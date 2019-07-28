using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Catalog.CollClass;
using Microcharts;
using SkiaSharp;
using System.Collections.Generic;
using Entry = Microcharts.Entry;
using SkiaSharp.Views.Forms;

namespace Catalog.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlanetPage : ContentPage
    {
        private Planet planet { get; set; }

        public PlanetPage(Planet _planet)
        {
            InitializeComponent();

            planet = _planet;
            if (planet == null)
                return;

            BindingContext = planet;
            InitializeChart();           
        }

        private void InitializeChart()
        {
            List<Entry> entries = new List<Entry>
            {
                new Entry(Convert.ToSingle(planet.Weight))
                {
                    TextColor = SKColor.Parse("000000"),
                    Color=SKColor.Parse("D50000"),
                    Label =Language.Weight,
                },
                new Entry(Convert.ToSingle(planet.Radius))
                {
                    TextColor = SKColor.Parse("000000"),
                    Color = SKColor.Parse("4A148C"),
                    Label = Language.Radius,
                },
                new Entry(Convert.ToSingle(planet.PeriodOfRotationOnItsAxis))
                {
                    TextColor = SKColor.Parse("000000"),
                    Color =  SKColor.Parse("#01579B"),
                    Label = Language.PeriodOfRotationOnItsAxis,
                },
                new Entry(Convert.ToSingle(planet.PeriodOfRotationAboutStar))
                {
                    TextColor = SKColor.Parse("000000"),
                    Color =  SKColor.Parse("#004D40"),
                    Label = Language.PeriodOfRotationAboutStar,
                },
                new Entry(Convert.ToSingle(planet.RadiusOfOrbit))
                {
                    TextColor = SKColor.Parse("000000"),
                    Color =  SKColor.Parse("#E65100"),
                    Label = Language.RadiusOfOrbit,
                },
            };
            Chart.Chart = new RadarChart { Entries = entries, BackgroundColor = Setting.BackColor.ToSKColor() };
        }

        private async void ChangePlanet(object s, EventArgs e) => await Navigation.PushAsync(new PlanetChangePage(planet));

        private async void DeletePlanet(object s, EventArgs e)
        {
            planet.Delete();
            await Navigation.PopAsync();
        }
        
        //открывает в браузере
        private void OpenUri(object s, EventArgs e)
        {
            try
            {
                Device.OpenUri(new Uri(planet.Uri));
            }
            catch
            {
                DisplayAlert(Language.Error, Language.NotFound, Language.Cancel);
            }
        }

    }
}