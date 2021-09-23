using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecastV2
{

    public class WeatherDailyModel
    {
        public string timezone { get; set; }
        public int timezone_offset { get; set; }
        public Daily[] daily { get; set; }
    }

    public class Daily
    {
        public int dt { get; set; }
        public Temp temp { get; set; }
        public Weather[] weather { get; set; }
    }

    public class Temp
    {
        //public float day { get; set; }
        public float min { get; set; }
        public float max { get; set; }
        //public float night { get; set; }
        //public float eve { get; set; }
        //public float morn { get; set; }
    }


}
