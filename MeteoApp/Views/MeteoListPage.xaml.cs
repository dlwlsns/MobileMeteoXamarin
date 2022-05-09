using System;
using MeteoApp.Models;
using MeteoApp.ViewModels;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace MeteoApp.Views
{
    public partial class MeteoListPage : ContentPage
    {

        public MeteoListPage()
        {
            InitializeComponent();
            BindingContext = new MeteoListViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        async void OnItemAdded(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("New Location", "City:");

            if (result.Equals(""))
                return;

            var weather = WeatherRequest.GetWeather(result);

            if (weather == null)
                return;

            Random rng = new Random();
            int i = rng.Next();
            var temperature = (string)JObject.Parse(weather)["main"]["temp"];
            Location l = new Location { ID = i, Name = result , Temperature = temperature + "°C" };

            ((MeteoListViewModel)BindingContext).Entries.Add(l);

            await App.Database.SaveItemAsync(l);
            OnPropertyChanged();
        }

        void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                Navigation.PushAsync(new MeteoItemPage(e.SelectedItem as Models.Location)
                {
                    BindingContext = e.SelectedItem as Models.Location
                });
            }
        }

    }
}