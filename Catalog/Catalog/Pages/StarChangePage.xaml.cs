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
    public partial class StarChangePage : ContentPage
    {
        private Star star { get; set; }
        private bool isNewStar { get; set; }

        public StarChangePage(Star _star)
        {
            InitializeComponent();
            searchPlanetButton.Style = Setting.ButtonStyle;
            deletePlanetButton.Style = Setting.ButtonStyle;
            searchConstellButton.Style = Setting.ButtonStyle;

            if (_star != null)
            {
                star = _star;
                isNewStar = false;
            }
            else
            {
                star = new Star("");
                isNewStar = true;
            }
            //данные о звезде
            this.BindingContext = star;
            nameEntry.Text = star.Name;
            weightEntry.Text = star.Weight.ToString();
            radiusEntry.Text = star.Radius.ToString();
            luminosityEntry.Text = star.Luminosity.ToString();
            typePicker.ItemsSource = Enum.GetNames(typeof(Star.typeOfStar));
            typePicker.SelectedIndex = (int)star.Type;

            NavigationPage.SetHasBackButton(this, false);
        }
        
        private async void Back(object s, EventArgs e) => await Navigation.PopAsync();

        private async void Save(object s, EventArgs e)
        {
            star.Name = nameEntry.Text;
            star.Weight = Convert.ToDouble(weightEntry.Text);
            star.Radius = Convert.ToDouble(radiusEntry.Text);
            star.Luminosity = Convert.ToDouble(luminosityEntry.Text);
            star.Type = (Star.typeOfStar)typePicker.SelectedIndex;
            star.Uri = uriEntry.Text;
            var constellations = Data.AllConstellations.Search(searchConstellButton.Text);
            if (constellations.Count() > 0)
                star.Constellation = constellations[0];
            if (isNewStar)
                Data.AllStars.Add(star);
            await Navigation.PopAsync();
        }

        //выбор звездной системы  
        private async void searchConstellButton_Clicked(object s, EventArgs e)
        {
            var searchConstellEntry = new Entry { Placeholder = Language.Constellation, Keyboard = Keyboard.Default };
            var searchConstelListView = new ListView() { ItemsSource = Data.AllConstellations, };
            searchConstellEntry.TextChanged += (_s, _e) => searchConstelListView.ItemsSource = Data.AllConstellations.Search(searchConstellEntry.Text);
            searchConstelListView.ItemTapped += async (_s, _e) =>
            {
                searchConstellButton.Text = searchConstelListView.SelectedItem.ToString();
                await Navigation.PopAsync();
            };
            await Navigation.PushAsync(new ContentPage { Content = new StackLayout { Children = { searchConstellEntry, searchConstelListView } } });
        }

        //планеты (добавление)
        private async void searchPlanetButton_Clicked(object s, EventArgs e)
        {
            var searchPlanetEntry = new Entry { Placeholder = Language.Planet, Keyboard = Keyboard.Default };
            var searchPlanetListView = new ListView { ItemsSource = Data.AllPlanets.Where(planets => planets.Star == null) };
            searchPlanetEntry.TextChanged += (_s, _e) =>
            {
             //   if (star.Planets != null)
                    searchPlanetListView.ItemsSource = (Data.AllPlanets.Search(searchPlanetEntry.Text)).Where(planets => planets.Star == null);
               // else
                 //   searchPlanetListView.ItemsSource = Data.AllPlanets.Search(searchPlanetEntry.Text);
            };
            searchPlanetListView.ItemTapped += async (_s, _e) =>
            {
                Planet planet = (Planet)searchPlanetListView.SelectedItem;
                if (await DisplayActionSheet(searchPlanetListView.SelectedItem.ToString(), Language.Cancel, null, Language.Add) == Language.Add)
                {
                    planet.Star = star;
                    searchPlanetEntry.Text = "";
                    searchPlanetListView.ItemsSource = (Data.AllPlanets.Search(searchPlanetEntry.Text)).Where(planets => planets.Star != star);
                }
            };
            await Navigation.PushAsync(new ContentPage { Content = new StackLayout { Children = { searchPlanetEntry, searchPlanetListView } } });
        }

        //планеты (удалить)
        private async void deletePlanetButton_Clicked(object s, EventArgs e)
        {
            var deletePlanetEntry = new Entry { Placeholder = Language.Planet, Keyboard = Keyboard.Default };
            var deletePlanetListView = new ListView { ItemsSource = star.Planets };
            deletePlanetEntry.TextChanged += (_s, _e) =>
            {
                deletePlanetListView.ItemsSource = star.Planets.Search(deletePlanetEntry.Text);
            };
            deletePlanetListView.ItemTapped += async (_s, _e) =>
            {
                Planet planet = (Planet)deletePlanetListView.SelectedItem;
                if (await DisplayActionSheet(deletePlanetListView.SelectedItem.ToString(), Language.Cancel, null, Language.Delete) == Language.Delete)
                {
                    planet.Star = null;
                    deletePlanetEntry.Text = "";
                    deletePlanetListView.ItemsSource = star.Planets.Search(deletePlanetEntry.Text);
                }
            };
            await Navigation.PushAsync(new ContentPage { Content = new StackLayout { Children = { deletePlanetEntry, deletePlanetListView } } });
        }
    }
}