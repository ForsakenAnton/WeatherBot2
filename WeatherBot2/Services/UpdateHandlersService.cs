using Shared.CountriesWithCitiesModels;
using Shared.VisualCrossingWeather;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace WeatherBot2.Services
{
    public class UpdateHandlersService
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly ILogger<UpdateHandlersService> _logger;

        private readonly VisualCrossingWeatherService _visualCrossingWeatherService;
        private readonly CountriesWithCitiesService _countriesWithCitiesService;

        public static bool IsUserInputText { get; private set; } = true;

        public UpdateHandlersService(
            ITelegramBotClient telegramBotClient, 
            ILogger<UpdateHandlersService> logger,
            VisualCrossingWeatherService visualCrossingWeatherService,
            CountriesWithCitiesService countriesWithCitiesService)
        {
            _telegramBotClient = telegramBotClient;
            _logger = logger;
            _visualCrossingWeatherService = visualCrossingWeatherService;
            _countriesWithCitiesService = countriesWithCitiesService;
        }


        public Task HandleErrorAsync(
            Exception ex,
            CancellationToken cancellationToken)
        {
            string error = ex switch
            {
                ApiRequestException apiRequestException =>
                $"Telegram Error:\n " +
                $"{apiRequestException.Message} " +
                $"{apiRequestException.ErrorCode}",

                _ => ex.ToString()
            };

            _logger.LogError("Error: " + error);
            return Task.CompletedTask;
        }


        public async Task HandleUpdateAsync(
            Update update, 
            CancellationToken cancellationToken)
        {
            Task handler = update switch
            {
                { Message: Message message } => BotOnMessageReceived(message, cancellationToken),
                { EditedMessage: Message message } => BotOnMessageReceived(message, cancellationToken),
                _ => UnknownUpdateHadlerAsync(update, cancellationToken)
            };

            await handler;
        }


        private async Task BotOnMessageReceived(
            Message message, 
            CancellationToken cancellationToken)
        {
            //await _telegramBotClient.SendTextMessageAsync(
            //    chatId: message.From!.Id,
            //    text: "HELLO: " + message.Text);

            if (message.Text is not { } messageText)
            {
                return;
            }

            var action = messageText.Split(' ')[0] switch
            {
                "/choose_country" => SendReplyKeyboard(_telegramBotClient, message, cancellationToken),
                "/close_keyboard" => RemoveKeyboard(_telegramBotClient, message, cancellationToken),
                "/help" => Help(_telegramBotClient, message, cancellationToken),
                _ => SendReplyKeyboard(_telegramBotClient, message, cancellationToken)
            };

            Message sentMessage = await action;
            _logger.LogDebug($"Message '{messageText}' was sent");
        }

        private async Task<Message> SendReplyKeyboard(
            ITelegramBotClient telegramBotClient,
            Message message,
            CancellationToken cancellationToken)
        {
            if (message.Text is "/choose_country")
            {
                IsUserInputText = false;
            }

            if (IsUserInputText == true && message.Text is not "/choose_country")
            {
                return await SendWeatherForecast(telegramBotClient, message, cancellationToken);
            }

            // TO DO...
            //return await SendWeatherForecast(_telegramBotClient, message, cancellationToken);

            CountriesAndCitiesData countriesAndCitiesData =
                await _countriesWithCitiesService.GetCountriesAndCitiesData();

            ReplyKeyboardMarkup keyboardWithCountries = GetReplyKeyboardMarkup();

            foreach (var item in countriesAndCitiesData.Data!)
            {
                if (item.Key.Equals(message.Text))
                {
                    ReplyKeyboardMarkup keyboardWithCities = GetReplyKeyboardMarkup(message.Text);

                    return await telegramBotClient.SendTextMessageAsync(
                        message.From!.Id,
                        text: "Choose the city of the country: " + message.Text,
                        replyMarkup: keyboardWithCities,
                        cancellationToken: cancellationToken);
                }

                foreach (var city in item.Value)
                {
                    if (city.Equals(message.Text))
                    {
                        return await SendWeatherForecast(telegramBotClient, message, cancellationToken);
                    }
                }
            }

            return await telegramBotClient.SendTextMessageAsync(
                message.From!.Id,
                text: "Choose the country: ",
                replyMarkup: keyboardWithCountries,
                cancellationToken: cancellationToken);


            // на выходе будет зубчатый массив из кнопок
            ReplyKeyboardMarkup GetReplyKeyboardMarkup(string? choosedCountry = null)
            {
                List<List<KeyboardButton>> listOfKeyboardButtonsList = new();
                List<KeyboardButton> keyboardButtons = new();
                if (choosedCountry is not null)
                {
                    var itemsWithChoosedCountry = new Dictionary<string, string[]>
                    {
                        [choosedCountry] = countriesAndCitiesData.Data!
                    .FirstOrDefault(c => c.Key == choosedCountry).Value


                    };
                    int index = 0;
                    foreach (var item in itemsWithChoosedCountry[choosedCountry])
                    {
                        keyboardButtons.Add(new KeyboardButton(item));
                        if (index == 1)
                        {
                            listOfKeyboardButtonsList.Add(keyboardButtons);
                            keyboardButtons = new List<KeyboardButton>();
                            index = 0;
                            continue;
                        }
                        index++;
                    }
                    if (index != 1)
                    {
                        listOfKeyboardButtonsList.Add(keyboardButtons);
                    }

                }
                else
                {

                    int index = 0;
                    foreach (var item in countriesAndCitiesData.Data!)
                    {
                        keyboardButtons.Add(new KeyboardButton(item.Key));
                        if (index == 1)
                        {
                            listOfKeyboardButtonsList.Add(keyboardButtons);
                            keyboardButtons = new List<KeyboardButton>();


                            index = 0;
                            continue;

                        }
                        index++;

                    }
                    if (index != 1)
                    {
                        listOfKeyboardButtonsList.Add(keyboardButtons);
                    }

                }
                ReplyKeyboardMarkup keyboard = new(listOfKeyboardButtonsList);
                return keyboard;

            }
        }

        private async Task<Message> SendWeatherForecast(
            ITelegramBotClient telegramBotClient, 
            Message message, 
            CancellationToken cancellationToken)
        {
            string query = _visualCrossingWeatherService
                .GetGeneralWeatherQuery(message.Text!);

            var weatherData = await _visualCrossingWeatherService
                .GetWeatherData(query);

            if (weatherData == null && IsUserInputText == true)
            {
                return await telegramBotClient.SendTextMessageAsync(
                    chatId: message.From!.Id,
                    text: "data not found.\n " +
                        "Can you try write the query with keyboard? /choose_country",
                    replyMarkup: new ReplyKeyboardRemove(),
                    cancellationToken: cancellationToken);
            }
            else if (weatherData == null)
            {
                IsUserInputText = true;

                return await telegramBotClient.SendTextMessageAsync(
                    chatId: message.From!.Id,
                    text: "Error while recieve the weather data...",
                    replyMarkup: new ReplyKeyboardRemove(),
                    cancellationToken: cancellationToken);
            }

            IsUserInputText = true;

            string weatherForecastString = GetWeatherForecast(weatherData);

            return await telegramBotClient.SendTextMessageAsync(
                message.From!.Id,
                weatherForecastString,
                replyMarkup: new ReplyKeyboardRemove(),
                parseMode: ParseMode.Markdown,
                cancellationToken: cancellationToken);
        }

        private string GetWeatherForecast(WeatherData weatherData)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("*Current Weather*");
            stringBuilder.AppendLine("Datatime: " + weatherData.CurrentConditions?.Datetime);
            stringBuilder.AppendLine("Temperature: " + weatherData.CurrentConditions?.Temp);
            stringBuilder.AppendLine("Longitude: " + weatherData.Longitude);
            stringBuilder.AppendLine("Latitude: " + weatherData.Latitude + "\n");

            if (weatherData.Days is not null)
            {
                foreach (var day in weatherData.Days)
                {
                    stringBuilder.AppendLine("*Weather on " + day.Datetime + "*");

                    stringBuilder.AppendLine("T: " + day.Temp);
                    stringBuilder.AppendLine("Wind speed: " + day.Windspeed);
                    stringBuilder.AppendLine("Solar Energy: " + day.Solarenergy);
                    stringBuilder.AppendLine("Solar Radiation: " + day.Solarradiation);
                    stringBuilder.AppendLine("Moonphase: " + day.Moonphase);
                }
            }

            return stringBuilder.ToString();
        }

        private async Task<Message> RemoveKeyboard(
            ITelegramBotClient telegramBotClient, 
            Message message,
            CancellationToken cancellationToken)
        {
            IsUserInputText = true;

            return await telegramBotClient.SendTextMessageAsync(
                   chatId: message.From!.Id,
                   text: "Keyboard removed",
                   replyMarkup: new ReplyKeyboardRemove(),
                   cancellationToken: cancellationToken);
        }


        private async Task<Message> Help(
            ITelegramBotClient telegramBotClient, 
            Message message,
            CancellationToken cancellationToken)
        {
            string menu = "Menu:\n" +
                "/choose_country\n" +
                "/close_keyboard\n" +
                "/help\n" +
                "Or search by input your text\n";

            return await telegramBotClient.SendTextMessageAsync(
                   chatId: message.From!.Id,
                   text: menu,
                   replyMarkup: new ReplyKeyboardRemove(),
                   cancellationToken: cancellationToken);
        }



        private Task UnknownUpdateHadlerAsync(
            Update update, 
            CancellationToken cancellationToken)
        {
            _logger.LogDebug("Update type is unknown: " + update.Type);
            return Task.CompletedTask;
        }
    }
}
