using System;
using System.Linq;
using Plugin.Geolocator;
using Xamarin.Essentials;

namespace MeteoApp.Models
{
    public class LocationRequest
    {
        public static async System.Threading.Tasks.Task<string> GetLocationAsync()
        {
            var locator = CrossGeolocator.Current; // singleton
            var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));

            var placemarks = await Geocoding.GetPlacemarksAsync(position.Latitude, position.Longitude);
            var placemark = placemarks?.FirstOrDefault();

            return placemark.Locality;
        }
    }
}
