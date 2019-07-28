using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Catalog.CollClass;

namespace Catalog.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PlanetChangePage : ContentPage
	{
        private Planet planet { get; set; }
        private bool isNewPlanet { get; set; }

        public PlanetChangePage (Planet _planet)
		{
			InitializeComponent ();
            searchStarButton.Style = Setting.ButtonStyle;

            if (_planet != null)
            {
                planet = _planet;
                isNewPlanet = false;
            }
            else
            {
                planet = new Planet("");
                isNewPlanet = true;
            }

            //данные о планете
            this.BindingContext = planet;

            NavigationPage.SetHasBackButton(this, false);
        }

        //настройка элементов toolbar'а
        private async void Back(object s, EventArgs e) => await Navigation.PopAsync();
        
        private async void Save(object s, EventArgs e)
        {
            planet.Name = nameEntry.Text;
            planet.Weight = Convert.ToDouble(weightEntry.Text);
            planet.Radius = Convert.ToDouble(radiusEntry.Text);
            planet.PeriodOfRotationOnItsAxis = Convert.ToDouble(PeriodOfRotationOnItsAxisCell.Text);
            planet.PeriodOfRotationAboutStar = Convert.ToDouble(PeriodOfRotationAboutStarCell.Text);
            planet.RadiusOfOrbit = Convert.ToDouble(RadiusOfOrbitCell.Text);
            planet.Uri = uriEntry.Text;
            var stars = Data.AllStars.Search(searchStarButton.Text);
            if (stars.Count() > 0)
                planet.Star = stars[0];
            if (isNewPlanet)
                Data.AllPlanets.Add(planet);
            await Navigation.PopAsync();
        }

        //выбор звезды  
        private async void searchStarButton_Clicked(object s, EventArgs e)
        {
            var searchStarEntry = new Entry { Placeholder = Language.Star, Keyboard = Keyboard.Default };
            var searchStarListView = new ListView() { ItemsSource = Data.AllStars, };
            searchStarEntry.TextChanged += (_s, _e) => searchStarListView.ItemsSource = Data.AllStars.Search(searchStarEntry.Text);
            searchStarListView.ItemTapped += async (_s, _e) =>
            {
                searchStarButton.Text = searchStarListView.SelectedItem.ToString();
                await Navigation.PopAsync();
            };
            await Navigation.PushAsync(new ContentPage { Content = new StackLayout { Children = { searchStarEntry, searchStarListView } } });
        }


    }
}