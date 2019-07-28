using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Catalog.CollClass;
using Entry = Microcharts.Entry;
using SkiaSharp.Views.Forms;
using SkiaSharp;
using Microcharts;

namespace Catalog.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StarPage : ContentPage
    {                
        private Star star { get; set; }

        public StarPage(Star _star)
        {
            InitializeComponent();

            star = _star;
            if (star == null)
                return;

            BindingContext = star;
            InitializeChart();
        }

        private void InitializeChart()
        {
            List<Entry> entries = new List<Entry>
            {
                new Entry(Convert.ToSingle(star.Weight))
                {
                    TextColor = SKColor.Parse("000000"),
                    Color=SKColor.Parse("D50000"),
                    Label =Language.Weight,
                },
                new Entry(Convert.ToSingle(star.Radius))
                {
                    TextColor = SKColor.Parse("000000"),
                    Color = SKColor.Parse("4A148C"),
                    Label = Language.Radius,
                },
                new Entry(Convert.ToSingle(star.Luminosity))
                {
                    TextColor = SKColor.Parse("000000"),
                    Color =  SKColor.Parse("#01579B"),
                    Label = Language.Luminosity,
                },
            };
            Chart.Chart = new RadarChart { Entries = entries, BackgroundColor = Setting.BackColor.ToSKColor() };
        }

        private async void ChangeStar(object s, EventArgs e) => await Navigation.PushAsync(new StarChangePage(star));

        private async void DeleteStar(object s, EventArgs e)
        {
            star.Delete();
            await Navigation.PopAsync();
        } 
        
        //вызывается, когда content является частью другой страницы с другим bindingContext
        public void InitializeContent()
        {
            if (star != null)
            {
                NameCell.Detail = star.Name;
                WeightCell.Detail = Convert.ToString(star.Weight);
                RadiusCell.Detail = Convert.ToString(star.Radius);
                LuminosityCell.Detail = Convert.ToString(star.Luminosity);
                TypeCell.Detail = Convert.ToString(star.Type);
            }
            else
            {
                Content = new Label { Text = Language.NotFound };
            }
        }


        //открывает в браузере
        private void OpenUri(object s, EventArgs e)
        {
            try
            {
                Device.OpenUri(new Uri(star.Uri));
            }
            catch
            {
                DisplayAlert(Language.Error, Language.NotFound, Language.Cancel);
            }
        }

    }
}