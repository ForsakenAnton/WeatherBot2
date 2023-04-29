namespace WeatherBot2.Configuration
{
    public class BotConfiguration
    {
        public static readonly string Configuration = "BotConfiguration";

        public string BotToken { get; set; } = default!;
        public string HostAddress { get; set; } = default!;
        public string Route { get; set; } = default!;
    }
}
