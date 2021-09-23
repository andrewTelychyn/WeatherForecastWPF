using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Windows;
using LiveCharts;
using LiveCharts.Wpf;
using WeatherForecastV2.ViewModel;

namespace WeatherForecastV2
{
    public class HourlyViewModel
    {
        private List<string> hoursValues;

        private ChartValues<double> temps;

        private ChartValues<double> cloudness;

        private ChartValues<double> pop;

        private ChartValues<double> humidity;

        private ChartValues<double> wind;

        private ChartValues<double> feelLike;


        public Axis AxisXHours;

        public Axis AxisYTemp;

        public Axis AxisYPercent;

        public Axis AxisYWind;


        public SeriesCollection SeriesTemp { get; set; }

        public SeriesCollection SeriesCloudness { get; set; }

        public SeriesCollection SeriesPop { get; set; }

        public SeriesCollection SeriesHumidity { get; set; }

        public SeriesCollection SeriesWind { get; set; }

        public SeriesCollection SeriesFeelLike { get; set; }


        public HourlyViewModel(WeatherHourlyModel model, int day)
        {

            hoursValues = new List<string>();

            temps = new ChartValues<double>();

            cloudness = new ChartValues<double>();

            pop = new ChartValues<double>();

            humidity = new ChartValues<double>();

            wind = new ChartValues<double>();

            feelLike = new ChartValues<double>();

            for (int i = day; i < day + 8; i++)
            {
                hoursValues.Add(DateTime.Parse(model.list[i].dt_txt).ToString("HH"));

                temps.Add(Math.Round(model.list[i].main.temp_max, 1));

                cloudness.Add(model.list[i].clouds.all);

                pop.Add(model.list[i].pop);

                humidity.Add(model.list[i].main.humidity);

                wind.Add(Math.Round(model.list[i].wind.speed, 1));

                feelLike.Add(Math.Round(model.list[i].main.feels_like, 1));
            }

            SeriesTemp = new SeriesCollection()
            {
                new LineSeries
                {
                    Title = "Temp:",
                    Values = temps
                }
            };

            SeriesCloudness = new SeriesCollection()
            {
                new LineSeries
                {
                    Title = "Cloud:",
                    Values = cloudness
                }
            };

            SeriesPop = new SeriesCollection()
            {
                new LineSeries
                {
                    Title = "Pop:",
                    Values = pop
                }
            };

            SeriesWind = new SeriesCollection()
            {
                new LineSeries
                {
                    Title = "Wind:",
                    Values = wind
                }
            };

            SeriesFeelLike = new SeriesCollection()
            {
                new LineSeries
                {
                    Title = "Feel:",
                    Values = feelLike
                }
            };

            SeriesHumidity = new SeriesCollection()
            {
                new LineSeries
                {
                    Title = "Hum:",
                    Values = humidity
                }
            };

            AxisXHours = new Axis
            {
                Title = "Hours",
                Labels = hoursValues
            };

            AxisYTemp = new Axis
            {
                Title = "\u2103",
                LabelFormatter = value => value.ToString()
            };

            AxisYPercent = new Axis
            {
                Title = "%",
                LabelFormatter = value => value.ToString()
            };

            AxisYWind = new Axis
            {
                Title = "км/г",
                LabelFormatter = value => value.ToString()
            };
    }

        public HourlyViewModel()
        {
            
        }

    }
}
