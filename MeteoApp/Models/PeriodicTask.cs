using System;
using System.Threading.Tasks;
using Matcha.BackgroundService;
using Newtonsoft.Json.Linq;
using Plugin.LocalNotifications;

namespace MeteoApp.Models
{
    public class PeriodicTask : IPeriodicTask
    {
        public TimeSpan Interval { get; set; }

        public PeriodicTask(int seconds)
        {
            Interval = TimeSpan.FromSeconds(seconds);
        }

        public async Task<bool> StartJob()
        {
            var gpsLocation = await LocationRequest.GetLocationAsync();

            var content = WeatherRequest.GetWeather(gpsLocation);

            if (content != null)
                return false;

            float temperature = float.Parse((string)JObject.Parse(content)["main"]["temp"]);

            string title = "Weather warning";
            if (temperature <= 0)
                CrossLocalNotifications.Current.Show(title, "On your location is very cold (" + temperature + "°C)");
            else if(temperature >= 35)
                CrossLocalNotifications.Current.Show(title, "On your location is very hot (" + temperature + "°C)");

            return true;
        }
    }
}
