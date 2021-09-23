using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecastV2
{
    public class WeatherHourlyModel
    {
        public List[] list { get; set; }
        public City city { get; set; }
    }

    public class City
    {
        public string name { get; set; }
        public int timezone { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
    }

    public class List
    {
        public int dt { get; set; }
        public Main main { get; set; }
        public Weather[] weather { get; set; }
        public Clouds clouds { get; set; }
        public Wind wind { get; set; }
        public int visibility { get; set; }
        public float pop { get; set; }//вероятность осадков
        public Sys sys { get; set; }// part of day
        public string dt_txt { get; set; }//time of data forecasted
        public Rain rain { get; set; }
    }

    public class Main
    {
        public float temp { get; set; }
        public float feels_like { get; set; }
        public float temp_min { get; set; }
        public float temp_max { get; set; }
        //public int pressure { get; set; }
        //public int sea_level { get; set; }
        //public int grnd_level { get; set; }
        public int humidity { get; set; }
        //public float temp_kf { get; set; }
    }

    public class Clouds
    {
        public int all { get; set; }
    }

    public class Wind
    {
        public float speed { get; set; }
    }

    public class Sys
    {
        public string pod { get; set; }
    }

    public class Rain
    {
        public float _3h { get; set; }
    }

    public class Weather
    {
        //public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
         public string icon { get; set; }
    }
}
