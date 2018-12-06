using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Catalog
{
	public partial class App : Application
    {
        private MainPage mainPage;
        public App ()
        {           
            InitializeComponent();
            mainPage = new Catalog.MainPage();
            MainPage = new NavigationPage(mainPage)
            {
                BarBackgroundColor = Color.FromHex("212121"),
                BackgroundColor=Color.FromHex("9E9E9E")                
            };      

        }



        protected override void OnStart ()
		{
			// Handle when your app starts
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
