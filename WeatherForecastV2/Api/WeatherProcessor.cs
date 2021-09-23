using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Resources;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecastV2
{
    public class WeatherProcessor
    {
        public static async Task<WeatherHourlyModel> LoadHourlyWeatherAsync()
        {
            string url = "http://api.openweathermap.org/data/2.5/forecast?lat=49.5564&lon=25.59689&units=metric&appid=9e8d169adca43f0202b47d5945771247";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    WeatherHourlyModel model = await response.Content.ReadAsAsync<WeatherHourlyModel>();
                    return model;
                }
                else 
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<WeatherDailyModel> LoadDailyWeatherAsync()
        {
            string url = "https://api.openweathermap.org/data/2.5/onecall?lat=49.5564&lon=25.59689&exclude=hourly,minutely,alerts,current&units=metric&appid=9e8d169adca43f0202b47d5945771247";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    WeatherDailyModel model  = await response.Content.ReadAsAsync<WeatherDailyModel>();
                    return model;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
