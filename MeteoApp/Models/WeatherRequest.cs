using System.Net.Http;
using System.Text;

namespace MeteoApp.Models
{
    public class WeatherRequest
    {
        public static string GetWeather(string city)
        {
            var httpClient = new HttpClient();
            StringBuilder sb = new StringBuilder("https://api.openweathermap.org/data/2.5/weather?q=", 200);
            sb.Append(city);
            sb.Append("&appid=1ae729fe4329fe7bb3784f5931d6643b&units=metric");
            var response = httpClient.GetAsync(sb.ToString());

            if (response.Result.IsSuccessStatusCode)
            {
                response.Result.EnsureSuccessStatusCode();
                return response.Result.Content.ReadAsStringAsync().Result;
            } 

            return null;
        }
    }
}
