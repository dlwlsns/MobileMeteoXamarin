using Matcha.BackgroundService;
using MeteoApp.Models;
using MeteoApp.Views;
using Plugin.LocalNotifications;
using Xamarin.Forms;

namespace MeteoApp
{
    public partial class App : Application
    {
        private static LocationDatabase database;

        public App()
        {
            InitializeComponent();

            
            var nav = new NavigationPage(new MeteoListPage())
            {
                BarBackgroundColor = Color.LightGreen,
                BarTextColor = Color.White
            };

            //var nav = new MeteoListPage();

            MainPage = nav;

            //Register Periodic Tasks
            BackgroundAggregatorService.Add(() => new PeriodicTask(3600));

            //Start the background service
            BackgroundAggregatorService.StartBackgroundService();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        public static LocationDatabase Database
        {
            get
            {
                if (database == null) // se l'istanza è nulla, la creo
                    database = new LocationDatabase();
                return database; // ritorno l'istanza
            }
        }

    }
}
