using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.VisualCrossingWeather
{
    public class GeneralWeatherQueryModel
    {
        public string Address { get; set; } = "Миколаїв";
        public bool Today { get; set; } = true;
        public string UnitGroup { get; set; } = "metric";
        public string Lang { get; set; } = "ua";

        public string Key { get; init; } = default!;

        public GeneralWeatherQueryModel(string address, string key)
        {
            Address = address;
            Key = key;
        }


        public string GetGeneralWeatherQuery
        {
            get =>
                $"{Address}" +
                $"{(Today == true ? "/" + ToCamalCase(nameof(Today)) + "?" : "?")}" +
                $"{ToCamalCase(nameof(Lang))}={Lang}&" +
                $"{ToCamalCase(nameof(Key))}={Key}";
        }

        private string ToCamalCase(string param)
        {
            if (string.IsNullOrEmpty(param))
            {
                return "";
            }
            else if (param.Length < 2)
            {
                return param.ToLower();
            }

            return param[0]
                .ToString()
                .ToLower() + param[1..]; //param.Substring(1);
        }
    }
}
