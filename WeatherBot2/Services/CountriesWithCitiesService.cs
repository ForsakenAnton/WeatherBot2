using Shared.CountriesWithCitiesModels;
using System.Text.Json;

namespace WeatherBot2.Services
{
    public class CountriesWithCitiesService
    {
        public async Task<CountriesAndCitiesData> GetCountriesAndCitiesData()
        {
            using FileStream fs =
                new FileStream("countriesWithCities.json", FileMode.Open);

            JsonElement jsonElement = await JsonSerializer.DeserializeAsync<JsonElement>(fs);
            string json = jsonElement.GetRawText();

            var countriesAndCitiesData = new CountriesAndCitiesData();
            countriesAndCitiesData.Data = 
                JsonSerializer.Deserialize<Dictionary<string, string[]>>(json);

            return countriesAndCitiesData;
        }
    }
}
