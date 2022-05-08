using Xamarin.Forms;
using Newtonsoft.Json.Linq;
using System;
using MeteoApp.Models;

namespace MeteoApp.Views
{
    public partial class MeteoItemPage : ContentPage
    {
        Location _location;
        public Location Location
        {
            get { return _location; }
            set
            {
                _location = value;
                OnPropertyChanged();
            }
        }

        public MeteoItemPage(Location location)
        {
            Location = location;
            InitializeComponent();
            
            GetWeatherAsync();
        }

        private void GetWeatherAsync()
        {
            var content = WeatherRequest.GetWeather(Location.Name);

            if (content == null)
                return;

            var weather = (string)JObject.Parse(content)["weather"][0]["description"];
            var image = (string)JObject.Parse(content)["weather"][0]["icon"];
            var temperature = (string)JObject.Parse(content)["main"]["temp"];

            Weather.Text = weather;
            Temperature.Text = temperature + " °C";
            WeatherImage.Source = ImageSource.FromUri(new Uri("https://openweathermap.org/img/wn/" + image + "@4x.png"));
        }
    }

}