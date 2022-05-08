using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MeteoApp.Models;
using Plugin.Geolocator;
using Xamarin.Essentials;
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
                Entries.Add(items[i]);
            }

            GetLocation();

            OnPropertyChanged();
        }

        async void GetLocation()
        {
            var city = await LocationRequest.GetLocationAsync();

            Entries.Insert(0, new Location { ID = 0, Name = city });
        }
    }
}