using System.Collections.Generic;
using System.Collections.ObjectModel;
using MeteoApp.Models;
using Newtonsoft.Json.Linq;
using Location = MeteoApp.Models.Location;

namespace MeteoApp.ViewModels
{
    public class MeteoListViewModel : BaseViewModel
    {
        ObservableCollection<Location> _entries;

        public ObservableCollection<Location> Entries
        {
            get { return _entries; }
            set
            {
                _entries = value;
                OnPropertyChanged();
            }
        }

        public MeteoListViewModel()
        {
            Entries = new ObservableCollection<Location>();

            List<Location> items = App.Database.GetItems().Result;

            int count = items.Count;

            for (var i = 0; i < count; i++)
            {
                var weather = WeatherRequest.GetWeather(items[i].Name);
                var temperature = (string)JObject.Parse(weather)["main"]["temp"];
                items[i].Temperature = temperature + "°C";
                Entries.Add(items[i]);
            }

            GetLocation();

            OnPropertyChanged();
        }

        async void GetLocation()
        {
            var city = await LocationRequest.GetLocationAsync();
            var weather = WeatherRequest.GetWeather(city);
            var temperature = (string)JObject.Parse(weather)["main"]["temp"];

            Entries.Insert(0, new Location { ID = 0, Name = city, Temperature = temperature+"°C" });
        }
    }
}