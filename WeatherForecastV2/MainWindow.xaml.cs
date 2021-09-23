using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts.Wpf;
using LiveCharts;
using WeatherForecastV2.ViewModel;

namespace WeatherForecastV2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public WeatherHourlyModel HourlyModel { get; set; }

        public WeatherDailyModel DailyModel { get; set; }

        private ChartData chartData;

        private int chosenDayIntForClass;


        public MainWindow()
        {
            InitializeComponent();

            setColor("#808B96", "#566573");

            ApiHelper.InitializeClient();

            initialDailyLoad();

            initialHourlyLoad();
        }

        private void loadChart()
        {
            var HourViewModel = new HourlyViewModel(HourlyModel, chosenDayIntForClass);

            Chart.AxisX.Clear();
            Chart.AxisX.Add(HourViewModel.AxisXHours);

            Chart.AxisY.Clear();

            switch (chartData)
            {
                case ChartData.Temp:
                    Chart.Series = HourViewModel.SeriesTemp;
                    Chart.AxisY.Add(HourViewModel.AxisYTemp);
                    Chart.AxisX[0].Title = "Температура";
                    break;
                case ChartData.Humidity:
                    Chart.Series = HourViewModel.SeriesHumidity;
                    Chart.AxisY.Add(HourViewModel.AxisYPercent);
                    Chart.AxisX[0].Title = "Вологість";

                    break;
                case ChartData.FeelLike:
                    Chart.Series = HourViewModel.SeriesFeelLike;
                    Chart.AxisY.Add(HourViewModel.AxisYTemp);
                    Chart.AxisX[0].Title = "Відчувається як";

                    break;
                case ChartData.Cloudness:
                    Chart.Series = HourViewModel.SeriesCloudness;
                    Chart.AxisY.Add(HourViewModel.AxisYPercent);
                    Chart.AxisX[0].Title = "Хмарність";

                    break;
                case ChartData.Pop:
                    Chart.Series = HourViewModel.SeriesPop;
                    Chart.AxisY.Add(HourViewModel.AxisYPercent);
                    Chart.AxisX[0].Title = "Ймовірність опадів";

                    break;
                case ChartData.Wind:
                    Chart.Series = HourViewModel.SeriesWind;
                    Chart.AxisY.Add(HourViewModel.AxisYWind);
                    Chart.AxisX[0].Title = "Вітер";

                    break;
            }
        }

        private void loadChosenDay(int chosenDay)
        {
            var day = HourlyModel.list[chosenDay];

            var uriSource = new Uri("http://openweathermap.org/img/wn/" + day.weather[0].icon + "@4x.png");

            ChosenWeatherImg.Source = new BitmapImage(uriSource);

            PopText.Text = $"Ймовірність опадів: {day.pop}%";

            HumidityText.Text = $"Вологість: {day.main.humidity}%";

            WindText.Text = $"Вітер: {Math.Round(day.wind.speed, 1)} км/г";

            CloudnessText.Text = $"Хмарність: {day.clouds.all}%";

            FeelLikeText.Text = $"Відчувається як: {Math.Round(day.main.feels_like, 1)}\u2103";


            DateTime dayDate = DateTime.Parse(day.dt_txt);

            if (dayDate.Date == DateTime.Now.Date)
                ChosenDayText.Text = "Сьогодні";
            else if(dayDate.Date == DateTime.Now.Date.AddDays(1))
                ChosenDayText.Text = "Завтра";
            else 
            {
                var dayString = dayDate.ToString("dddd");

                ChosenDayText.Text = char.ToUpper(dayString[0]).ToString() + dayString.Substring(1);
            }
            

            TimeText.Text = dayDate.ToString("t");

            TempText.Text = Math.Round(day.main.temp, 1).ToString() + "\u2103";

        }

        private async void initialHourlyLoad()
        {
            HourlyModel = await WeatherProcessor.LoadHourlyWeatherAsync();

            chosenDayIntForClass = 0;

            loadChosenDay(chosenDayIntForClass);

            chartData = ChartData.Temp;

            loadChart();
        }

        private async void initialDailyLoad()
        {
            try
            {
                DailyModel = await WeatherProcessor.LoadDailyWeatherAsync();
            }
            catch (Exception)
            {
                MessageBox.Show("Please, check your internet connection");
                //isInternet = false;
                return;
            }

            List<Image> images = new List<Image> { DailyWeather1, DailyWeather2, DailyWeather3, DailyWeather4, DailyWeather5};

            List<TextBlock> days = new List<TextBlock> { DayNameText1, DayNameText2, DayNameText3, DayNameText4, DayNameText5};

            List<TextBlock> temps = new List<TextBlock> { DailyText1, DailyText2, DailyText3, DailyText4, DailyText5 };

            for (int i = 0; i < 5; i++)
            {
                days[i].Text = DateTime.Parse(HourlyModel.list[i*8].dt_txt).ToString("ddd").ToUpper();

                temps[i].Text = $"{Math.Round(DailyModel.daily[i].temp.min, 1)}/{Math.Round(DailyModel.daily[i].temp.max)}\u2103";

                var uriSource = new Uri("http://openweathermap.org/img/wn/"+ DailyModel.daily[i].weather[0].icon +"@4x.png");

                images[i].Source = new BitmapImage(uriSource);
            }
        }

        private void setColor(string color, string secondColor)
        {
            var bc = (Brush)new BrushConverter().ConvertFrom(color);

            var bc2 = (Brush)new BrushConverter().ConvertFrom(secondColor);

            BackgroundData.Background = bc;

            BackgroundImg.Background = bc;

            var list = new List<Canvas> { BackgroundDay1, BackgroundDay2, BackgroundDay3, BackgroundDay4, BackgroundDay5 };

            foreach (var item in list)
            {
                item.Background = bc;
            }

            var canvasList = new List<Canvas> { BackgroundDayName1, BackgroundDayName2, BackgroundDayName3, BackgroundDayName4, BackgroundDayName5, 
                BackgroundTemp1, BackgroundTemp2, BackgroundTemp3, BackgroundTemp4, BackgroundTemp5, BackgroundChosenData };

            foreach (var item in canvasList)
            {
                item.Background = bc2;
            }


            Chart.SeriesColors = new ColorsCollection();
            Chart.SeriesColors.Add((Color)ColorConverter.ConvertFromString(secondColor));
        }

        #region MouseDownMethods
        private void DailyWeather1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            chosenDayIntForClass = 0;
            loadChosenDay(chosenDayIntForClass);
            loadChart();
        }

        private void DailyWeather2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            chosenDayIntForClass = 8;
            loadChosenDay(chosenDayIntForClass);
            loadChart();
        }

        private void DailyWeather3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            chosenDayIntForClass = 16;
            loadChosenDay(chosenDayIntForClass);
            loadChart();
        }

        private void DailyWeather4_MouseDown(object sender, MouseButtonEventArgs e)
        {
            chosenDayIntForClass = 24;
            loadChosenDay(chosenDayIntForClass);
            loadChart();
        }

        private void DailyWeather5_MouseDown(object sender, MouseButtonEventArgs e)
        {
            chosenDayIntForClass = 32;
            loadChosenDay(chosenDayIntForClass);
            loadChart();
        }

        private void PopText_MouseDown(object sender, MouseButtonEventArgs e)
        {
            chartData = ChartData.Pop;
            loadChart();
        }

        private void FeelLikeText_MouseDown(object sender, MouseButtonEventArgs e)
        {
            chartData = ChartData.FeelLike;
            loadChart();
        }

        private void WindText_MouseDown(object sender, MouseButtonEventArgs e)
        {
            chartData = ChartData.Wind;
            loadChart();
        }

        private void CloudnessText_MouseDown(object sender, MouseButtonEventArgs e)
        {
            chartData = ChartData.Cloudness;
            loadChart();
        }

        private void HumidityText_MouseDown(object sender, MouseButtonEventArgs e)
        {
            chartData = ChartData.Humidity;
            loadChart();
        }

        private void TempText_MouseDown(object sender, MouseButtonEventArgs e)
        {
            chartData = ChartData.Temp;
            loadChart();
        }

        private void Chart_DataClick(object sender, ChartPoint p)
        {
            //MessageBox.Show($"{chosenDayIntForClass} + {p.X}");
            loadChosenDay(chosenDayIntForClass + (int)p.X);
        }
        #endregion

    }
}
