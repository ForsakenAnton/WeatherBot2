using Telegram.Bot;
using WeatherBot2.Configuration;
using WeatherBot2.Services;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

IConfigurationSection botConfigSection = builder.Configuration
    .GetSection(BotConfiguration.Configuration);

builder.Services.Configure<BotConfiguration>(botConfigSection);

builder.Services
    .AddHttpClient("weatherBotClient")
    .AddTypedClient<ITelegramBotClient>((httpClient, services) =>
    {
        BotConfiguration botConfiguration = 
            botConfigSection.Get<BotConfiguration>();
        //TelegramBotClientExtensions options = new TelegramBotClientExtensions("")

        TelegramBotClient botClient = new TelegramBotClient(
            new TelegramBotClientOptions(botConfiguration.BotToken),
            httpClient);

        return botClient;
    });

builder.Services.AddHttpClient("WeatherHttpClient", client =>
{
    client.BaseAddress = new Uri(
        "https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/");
});

builder.Services.AddScoped<UpdateHandlersService>();
builder.Services.AddHostedService<ConfigureWebhookService>();

builder.Services.AddScoped<VisualCrossingWeatherService>();
builder.Services.AddSingleton<CountriesWithCitiesService>();

builder.Services
    .AddControllers()
    .AddNewtonsoftJson();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
