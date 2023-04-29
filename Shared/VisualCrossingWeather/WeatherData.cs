

using Newtonsoft.Json;

namespace Shared.VisualCrossingWeather
{
    public class WeatherData
    {
        [JsonProperty("queryCost")] // ... (?) стоимость запроса.
                                    // В моем случае 1000 запросов в сутки бесплатно!
        public int? QueryCost { get; set; }
        [JsonProperty("latitude")] // широта
        public double? Latitude { get; set; }
        [JsonProperty("longitude")] // долгота
        public double? Longitude { get; set; }
        [JsonProperty("resolvedAddress")]
        // адрес (ваш адрес, что вернул запрос при вводе address ниже)
        // напр. Україна Кривий Ріг, или по английски, или комбинируя несколько языков
        public string? ResolvedAddress { get; set; }
        [JsonProperty("address")] // адрес (ваш адрес, что вы ввели)
        public string? Address { get; set; }
        [JsonProperty("timezone")] // часовой пояс
        public string? Timezone { get; set; }
        [JsonProperty("tzoffset")] // сдвиг часового пояса
        public double? Tzoffset { get; set; }

        [JsonProperty("description")] // описание
        public string? Description { get; set; }
        [JsonProperty("days")] // дни
        public IList<Day>? Days { get; set; }
        [JsonProperty("alerts")] // оповещения
        public IList<object>? Alerts { get; set; }
        [JsonProperty("currentConditions")] // текущие (погодные) условия
        public CurrentConditions? CurrentConditions { get; set; }
    }


    // Все описания полей написаны в классах Day & Hour
    // Здесь на 99% все то же
    public class CurrentConditions
    {
        [JsonProperty("datetime")]
        public string? Datetime { get; set; }
        [JsonProperty("datetimeEpoch")]
        public int? DatetimeEpoch { get; set; }
        [JsonProperty("temp")]
        public double? Temp { get; set; }
        [JsonProperty("feelslike")]
        public double? Feelslike { get; set; }
        [JsonProperty("humidity")]
        public double? Humidity { get; set; }
        [JsonProperty("dew")]
        public double? Dew { get; set; }
        [JsonProperty("precip")]


        public double? Precip { get; set; }
        [JsonProperty("precipprob")]
        public double? Precipprob { get; set; }
        [JsonProperty("snow")]
        public double? Snow { get; set; }
        [JsonProperty("snowdepth")]
        public double? Snowdepth { get; set; }
        [JsonProperty("preciptype")]
        public IList<string>? Preciptype { get; set; }
        [JsonProperty("windgust")]
        public double? Windgust { get; set; }
        [JsonProperty("windspeed")]
        public double? Windspeed { get; set; }
        [JsonProperty("winddir")]
        public double? Winddir { get; set; }
        [JsonProperty("pressure")]
        public double? Pressure { get; set; }
        [JsonProperty("visibility")]
        public double? Visibility { get; set; }
        [JsonProperty("cloudcover")]
        public double? Cloudcover { get; set; }


        [JsonProperty("solarradiation")]
        public double? Solarradiation { get; set; }
        [JsonProperty("solarenergy")]
        public double? Solarenergy { get; set; }
        [JsonProperty("uvindex")]
        public double? Uvindex { get; set; }
        [JsonProperty("severerisk")]
        public double? Severerisk { get; set; }
        [JsonProperty("conditions")]
        public string? Conditions { get; set; }
        [JsonProperty("icon")]
        public string? Icon { get; set; }
        [JsonProperty("stations")]
        public IList<object>? Stations { get; set; }
        [JsonProperty("source")]
        public string? Source { get; set; }
        [JsonProperty("sunrise")]
        public string? Sunrise { get; set; }
        [JsonProperty("sunriseEpoch")]
        public double? SunriseEpoch { get; set; }
        [JsonProperty("sunset")]
        public string? Sunset { get; set; }


        [JsonProperty("sunsetEpoch")]
        public double? SunsetEpoch { get; set; }
        [JsonProperty("moonphase")]
        public double? Moonphase { get; set; }
    }


    public class Day
    {
        [JsonProperty("datetime")] // дата и время
        public string? Datetime { get; set; }
        [JsonProperty("datetimeEpoch")] // (?)
        public int? DatetimeEpoch { get; set; }
        [JsonProperty("tempmax")] // температура макс.
        public double? Tempmax { get; set; }
        [JsonProperty("tempmin")] // температура мин.
        public double? Tempmin { get; set; }
        [JsonProperty("temp")] // температура
        public double? Temp { get; set; }
        [JsonProperty("feelslikemax")] // ощущается как макс. ...
        public double? Feelslikemax { get; set; }
        [JsonProperty("feelslikemin")] // ощущается как мин. ...
        public double? Feelslikemin { get; set; }
        [JsonProperty("feelslike")] // ощущается как...


        public double? Feelslike { get; set; }
        [JsonProperty("dew")] // роса
        public double? Dew { get; set; }
        [JsonProperty("humidity")] // влажность
        public double? Humidity { get; set; }
        [JsonProperty("precip")] // precipitation - осадки
        public double? Precip { get; set; }
        [JsonProperty("precipprob")] // precipitation probably - возможные осадки
        public double? Precipprob { get; set; }
        [JsonProperty("precipcover")] // изменение осадков
        public double? Precipcover { get; set; }
        [JsonProperty("preciptype")] // тип осадков
        public IList<string>? Preciptype { get; set; }
        [JsonProperty("snow")] // снег
        public double? Snow { get; set; }
        [JsonProperty("snowdepth")] // толщина снега
        public double? Snowdepth { get; set; }
        [JsonProperty("windgust")] // порывы ветра
        public double? Windgust { get; set; }
        [JsonProperty("windspeed")] // скорость ветра
        public double? Windspeed { get; set; }


        [JsonProperty("winddir")] // направление ветра
        public double? Winddir { get; set; }
        [JsonProperty("pressure")] // давление
        public double? Pressure { get; set; }
        [JsonProperty("cloudcover")] // облачность
        public double? Cloudcover { get; set; }
        [JsonProperty("visibility")] // видимость (фактическая)
        public double? Visibility { get; set; }
        [JsonProperty("solarradiation")] // солнечная радиация
        public double? Solarradiation { get; set; }
        [JsonProperty("solarenergy")] // солн. энергия
        public double? Solarenergy { get; set; }
        [JsonProperty("uvindex")] // ультрафиолетовый индекс (uv - ultra-violet)
        public double? Uvindex { get; set; }
        [JsonProperty("severerisk")] // ??? дословно - тяжесть риска
        public double? Severerisk { get; set; }
        [JsonProperty("sunrise")] // восход солнца
        public string? Sunrise { get; set; }
        [JsonProperty("sunriseEpoch")] // (?)
        public double? SunriseEpoch { get; set; }
        [JsonProperty("sunset")] // заход солнца
        public string? Sunset { get; set; }


        [JsonProperty("sunsetEpoch")] // (?)
        public double? SunsetEpoch { get; set; }
        [JsonProperty("moonphase")] // фаза луны
        public double? Moonphase { get; set; }
        [JsonProperty("conditions")] // условия (?)
        public string? Conditions { get; set; }
        [JsonProperty("description")] // описание
        public string? Description { get; set; }
        [JsonProperty("icon")]
        public string? Icon { get; set; }
        [JsonProperty("stations")]
        public IList<string>? Stations { get; set; }
        [JsonProperty("source")] // источник (чего ?)
        public string? Source { get; set; }

        [JsonProperty("hours")]
        public IList<Hour>? Hours { get; set; }
    }


    public class Hour
    {
        [JsonProperty("datetime")]
        public string? Datetime { get; set; }
        [JsonProperty("datetimeEpoch")]
        public int? DatetimeEpoch { get; set; }
        [JsonProperty("temp")] // температура
        public double? Temp { get; set; }
        [JsonProperty("feelslike")] // ощущается как...
        public double? Feelslike { get; set; }
        [JsonProperty("humidity")] // влажность
        public double? Humidity { get; set; }
        [JsonProperty("dew")] // роса
        public double? Dew { get; set; }
        [JsonProperty("precip")] // precipitation - осадки
        public double? Precip { get; set; }
        [JsonProperty("precipprob")] // precipitation probably - возможные осадки


        public double? Precipprob { get; set; }
        [JsonProperty("snow")] // снег
        public double? Snow { get; set; }
        [JsonProperty("snowdepth")] // толщина снега
        public double? Snowdepth { get; set; }
        [JsonProperty("preciptype")] // тип осадков
        public IList<string>? Preciptype { get; set; }
        [JsonProperty("windgust")] // порывы ветра
        public double? Windgust { get; set; }
        [JsonProperty("windspeed")] // скорость ветра
        public double? Windspeed { get; set; }
        [JsonProperty("winddir")] // направление ветра
        public double? Winddir { get; set; }
        [JsonProperty("pressure")] // давление
        public double? Pressure { get; set; }
        [JsonProperty("visibility")] // видимость (фактическая)
        public double? Visibility { get; set; }
        [JsonProperty("cloudcover")] // облачность
        public double? Cloudcover { get; set; }
        [JsonProperty("solarradiation")] // солнечная радиация
        public double? Solarradiation { get; set; }


        [JsonProperty("solarenergy")] // солн. энергия
        public double? Solarenergy { get; set; }
        [JsonProperty("uvindex")] // ультрафиолетовый индекс (uv - ultra-violet)
        public double? Uvindex { get; set; }
        [JsonProperty("severerisk")] // ??? дословно - тяжесть риска
        public double? Severerisk { get; set; }
        [JsonProperty("conditions")] // условия (?)
        public string? Conditions { get; set; }
        [JsonProperty("icon")]
        public string? Icon { get; set; }
        [JsonProperty("stations")]
        public IList<object>? Stations { get; set; }
        [JsonProperty("source")] // источник (чего ?)
        public string? Source { get; set; }
        [JsonProperty("sunrise")] // восход солнца
        public string? Sunrise { get; set; }
        [JsonProperty("sunriseEpoch")] // (?)
        public double? SunriseEpoch { get; set; }
        [JsonProperty("sunset")] // заход солнца
        public string? Sunset { get; set; }
        [JsonProperty("sunsetEpoch")] // (?)
        public double? SunsetEpoch { get; set; }


        [JsonProperty("moonphase")] // фаза луны
        public double? Moonphase { get; set; }

    }
}
