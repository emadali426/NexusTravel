using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelNexus.Services.TboServiceReference;

namespace TravelNexus.Services
{
    public static class Country
    {
        public static Dictionary<string, string> CountryCodes { get; set; }
        public static void InitializeCountries()
        {
            CountryCodes = new Dictionary<string, string>();
            TboService service = new TboService();
            CountryList[] countries = service.Countries();
            if (countries == null)
                return;

            foreach (CountryList country in countries)
            {
                var exist = CountryCodes.Any(x => x.Key == country.CountryCode);
                if (!exist)
                    CountryCodes.Add(country.CountryCode, country.CountryName);
            }
        }

        public static string GetCountryNameByCode(string code)
        {
            return CountryCodes[code.ToUpper()];
        }

        public static string GetCountryCodeByName(string name)
        {
            var country = CountryCodes.Where(x => x.Value.ToLower() == name.ToLower()).FirstOrDefault();
            return country.Key;
        }
    }
}
