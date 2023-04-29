using Newtonsoft.Json;
using Shared.VisualCrossingWeather;

namespace WeatherBot2.Services
{
    public class VisualCrossingWeatherService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<VisualCrossingWeatherService> _logger;
        private readonly string _key;

        public VisualCrossingWeatherService(
            IHttpClientFactory httpClientFactory,
            ILogger<VisualCrossingWeatherService> logger,
            IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _key = configuration.GetSection("VisualCrossing").GetValue<string>("Key");
        }


        public string GetGeneralWeatherQuery(string address)
        {
            GeneralWeatherQueryModel queryModel = new(address, _key);
            return queryModel.GetGeneralWeatherQuery;
        }


        public async Task <WeatherData?> GetWeatherData(string query)
        {
            HttpClient httpClient = CreateNamedHttpClient();

            HttpRequestMessage httpRequestMessage = new(HttpMethod.Get, query);

            try
            {
                var response = await httpClient.SendAsync(httpRequestMessage);
                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync();
                    var weatherData = JsonConvert.DeserializeObject<WeatherData>(responseString);

                    return weatherData;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return null;
        }

        public HttpClient CreateNamedHttpClient()
        {
            return _httpClientFactory.CreateClient("WeatherHttpClient");
        }
    }
}
