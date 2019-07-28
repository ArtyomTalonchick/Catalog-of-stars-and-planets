using Xamarin.Forms;
using Xamarin.Essentials;
using System.ComponentModel;

namespace Catalog
{
	public partial class App : Application, INotifyPropertyChanged
    {
        public static double ScreenWidth;
        public static double ScreenHeight;

        private MainPage mainPage;
        
        public App ()
        {
            Settings();
            InitializeComponent();
            mainPage = new MainPage();
            var navPage = new NavigationPage(mainPage)
            {
                BackgroundColor = Setting.BackColor,
                BarBackgroundColor = Setting.BarColor,
            };
            MainPage = navPage;
            mainPage.navPage = navPage;
        }

        private void Settings()
        {
            var lan = Setting.Language;
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(lan);
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(lan);            
        }

        protected override void OnStart ()
		{

            mainPage.writeNameFile();

            mainPage.addingItems();

       //     mainPage.deserialize();
        }

		protected override void OnSleep ()
		{
            mainPage.serialize();
        }

		protected override void OnResume ()
		{
            mainPage.serialize();
        }
    }
}
