using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using WeatherBot2.Configuration;

namespace WeatherBot2.Services
{
    public class ConfigureWebhookService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly BotConfiguration _botConfiguration;
        private readonly ILogger<ConfigureWebhookService> _logger;

        public ConfigureWebhookService(
            IServiceProvider serviceProvider, 
            IOptions<BotConfiguration> botConfigurationOptions,
            ILogger<ConfigureWebhookService> logger)
        {
            _serviceProvider = serviceProvider;
            _botConfiguration = botConfigurationOptions.Value;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using IServiceScope scope = _serviceProvider.CreateScope();
            var botClient = scope.ServiceProvider
                .GetRequiredService<ITelegramBotClient>();

            string webhookUrl = _botConfiguration.HostAddress +
                _botConfiguration.Route;

            await botClient.SetWebhookAsync(
                url: webhookUrl,
                allowedUpdates: Array.Empty<UpdateType>(),
                cancellationToken: cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            using IServiceScope scope = _serviceProvider.CreateScope();
            var botClient = scope.ServiceProvider
                .GetRequiredService<ITelegramBotClient>();

            await botClient
                .DeleteWebhookAsync(cancellationToken: cancellationToken);
        }
    }
}
