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
    public partial class PlanetPage : ContentPage
    {
        private Planet planet { get; set; }
        private MyCollection<Constellation> Constellations { get; set; }
        private MyCollection<Star> Stars { get; set; }
        private MyCollection<Planet> Planets { get; set; }
        private ToolbarItem changehTb { get; set; }

        private PlanetPage()
        {
            InitializeComponent();
        }
        public PlanetPage(Planet _planet, MyCollection<Constellation> _constellations, MyCollection<Star> _stars, MyCollection<Planet> _planets) : this()
        {
            planet = _planet;
            Title = "Планета - " + planet.Name;
            Constellations = _constellations;
            Stars = _stars;
            Planets = _planets;

            data();
            change();
        }

        private void data()
        {
            var starButton = new Button { TextColor = Color.Black, BackgroundColor = Color.FromHex("9E9E9E"), HorizontalOptions = LayoutOptions.Start };
            if (planet.Star != null)
            {
                starButton.Text = planet.Star.ToString();
                starButton.Clicked += async (s, e) => await Navigation.PushAsync(new Pages.StarPage(planet.Star, Constellations, Stars, Planets));
            }

            var dataRoot = new TableRoot()
            {
                new TableSection("Название") { new TextCell{ Text=planet.Name, TextColor=Color.Black } },
                new TableSection("Масса") { new TextCell{ Text= planet.Weight.ToString(), TextColor=Color.Black } },
                new TableSection("Радиус") { new TextCell{ Text= planet.Radius.ToString(), TextColor=Color.Black} },
                new TableSection("Звезда") { new ViewCell { View = starButton } },
            };
            OurTableView.Root = dataRoot;
        }

        private void change()
        {
            //данные о плнете
            var nameEntry = new EntryCell { Text = planet.Name, Keyboard = Keyboard.Default };
            var weightEntry = new EntryCell { Text = planet.Weight.ToString(), Keyboard = Keyboard.Numeric };
            var radiusEntry = new EntryCell { Text = planet.Radius.ToString(), Keyboard = Keyboard.Numeric };         
            var changeRoot = new TableRoot()
            {
                new TableSection("Название") { nameEntry },
                new TableSection("Масса") { weightEntry },
                new TableSection("Радиус") { radiusEntry },
            };

            //кнопка "Назад" для возврата из выбора звезды
            var cancelButton = new Button { Text = "Назад", TextColor = Color.Black, BackgroundColor = Color.FromHex("9E9E9E") };
            cancelButton.Clicked += (s, e) =>
            {
                ToolbarItems.Add(changehTb);
                NavigationPage.SetHasBackButton(this, true);
                OurStackLayout.Children.Clear();
                OurStackLayout.Children.Add(OurTableView);
            };
           
            //звезда
            var searchStarButton = new Button { Text = "Звезда", TextColor = Color.Black, BackgroundColor = Color.FromHex("9E9E9E"), HorizontalOptions = LayoutOptions.Start };
            var searchStarEntry = new Entry { Placeholder = "Звезда", Keyboard = Keyboard.Default };
            var searchStarListView = new ListView() { ItemsSource = Constellations, };
            searchStarButton.Clicked += (s, e) =>
            {
                ToolbarItems.Clear();
                NavigationPage.SetHasBackButton(this, false);
                searchStarEntry.TextChanged += (_s, _e) =>
                {
                    searchStarListView.ItemsSource = Stars.Search(searchStarEntry.Text);
                };
                OurStackLayout.Children.Clear();
                OurStackLayout.Children.Add(searchStarEntry);
                OurStackLayout.Children.Add(searchStarListView);
            };
            searchStarListView.ItemTapped += (s, e) =>
            {
                searchStarButton.Text = searchStarListView.SelectedItem.ToString();
                OurStackLayout.Children.Clear();
                OurStackLayout.Children.Add(OurTableView);
            };
            changeRoot.Add(new TableSection("Звезда") { new ViewCell { View = searchStarButton } });
            
            {
                changehTb = new ToolbarItem()
                {
                    Text = "Изменить",
                    Order = ToolbarItemOrder.Secondary,
                    Priority = 1,

                };
                changehTb.Clicked += (s, e) =>
                {
                    if (OurTableView.Root == changeRoot)
                    {
                        NavigationPage.SetHasBackButton(this, true);
                        changehTb.Text = "Изменить";
                        planet.Name = nameEntry.Text;
                        planet.Weight = Convert.ToDouble(weightEntry.Text);
                        planet.Radius = Convert.ToDouble(radiusEntry.Text);
                        if (searchStarListView.SelectedItem != null)
                            planet.Star= (Star)searchStarListView.SelectedItem;
                        data();
                    }
                    else
                    {
                        NavigationPage.SetHasBackButton(this, false);
                        OurTableView.Root = changeRoot;
                        changehTb.Text = "Сохранить";
                    }
                };
                ToolbarItems.Add(changehTb);
            }
        }

    }
}