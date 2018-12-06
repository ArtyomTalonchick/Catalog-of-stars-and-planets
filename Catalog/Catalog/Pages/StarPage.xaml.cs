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
    public partial class StarPage : ContentPage
    {
        private Star star { get; set; }
        private MyCollection<Constellation> Constellations { get; set; }
        private MyCollection<Star> Stars { get; set; }
        private MyCollection<Planet> Planets { get; set; }
        private ToolbarItem changehTb { get; set; }

        private StarPage()
        {
            InitializeComponent();
        }
        public StarPage(Star _star, MyCollection<Constellation> _constellations, MyCollection<Star> _stars, MyCollection<Planet> _planets) : this()
        {
            star = _star;
            Title = "Звезда - " + star.Name;
            Constellations = _constellations;
            Stars = _stars;
            Planets = _planets;
                   
            data();
            change();          
        }

        private void data()
        {
            var ConstButton = new Button { TextColor = Color.Black, BackgroundColor = Color.FromHex("9E9E9E"), HorizontalOptions = LayoutOptions.Start };
            if (star.Constellation != null)
            {
                ConstButton.Text = star.Constellation.ToString();
                ConstButton.Clicked += async (s, e) => await Navigation.PushAsync(new Pages.ConstellationPage(star.Constellation, Constellations, Stars, Planets));
            }
            var PlanetList = new ListView { ItemsSource = star.Planets };
            PlanetList.ItemTapped += async (s, e) => await Navigation.PushAsync(new Pages.PlanetPage((Planet)PlanetList.SelectedItem, Constellations, Stars, Planets));
            var PlanetButton = new Button { Text = "Просмотерть", TextColor = Color.Black, BackgroundColor = Color.FromHex("9E9E9E"), HorizontalOptions = LayoutOptions.Start };
            var cancelButton = new Button { Text = "Назад", TextColor = Color.Black, BackgroundColor = Color.FromHex("9E9E9E"), HorizontalOptions = LayoutOptions.CenterAndExpand };
            cancelButton.Clicked += (s, e) =>
            {
                ToolbarItems.Add(changehTb);
                NavigationPage.SetHasBackButton(this, true);
                OurStackLayout.Children.Clear();
                OurStackLayout.Children.Add(OurTableView);
            };
            PlanetButton.Clicked += async (s, e) =>
            {
                ToolbarItems.Clear();
                NavigationPage.SetHasBackButton(this, false);
                OurStackLayout.Children.Clear();
                OurStackLayout.Children.Add(PlanetList);
                OurStackLayout.Children.Add(cancelButton);
            };

            var dataRoot = new TableRoot()
            {
                new TableSection("Название") { new TextCell{ Text=star.Name, TextColor=Color.Black } },
                new TableSection("Масса") { new TextCell{ Text=star.Weight.ToString(), TextColor=Color.Black } },
                new TableSection("Радиус") { new TextCell{ Text=star.Radius.ToString(), TextColor=Color.Black} },
                new TableSection("Сетимость") { new TextCell{ Text=star.Luminosity.ToString(), TextColor=Color.Black } },
                new TableSection("Тип звезды") { new TextCell{ Text=star.type.ToString(), TextColor=Color.Black } },                
                new TableSection("Звездная система") { new ViewCell { View = ConstButton } },
                new TableSection("Спутники"){new ViewCell { View = PlanetButton} }
            };
            OurTableView.Root = dataRoot;            
        }

        private void change()
        {            
            //данные о звезде
            var nameEntry = new EntryCell { Text = star.Name, Keyboard = Keyboard.Default };
            var weightEntry = new EntryCell { Text = star.Weight.ToString(), Keyboard = Keyboard.Numeric };
            var radiusEntry = new EntryCell { Text = star.Radius.ToString(), Keyboard = Keyboard.Numeric };
            var luminosityEntry = new EntryCell { Text = star.Luminosity.ToString(), Keyboard = Keyboard.Numeric };
            var typePicker = new Picker { Title = "Тип звезды", ItemsSource = Enum.GetNames(typeof(Star.typeOfStar)), SelectedIndex=(int)star.type };
            var changeRoot = new TableRoot()
            {
                new TableSection("Название") { nameEntry },
                new TableSection("Масса") { weightEntry },
                new TableSection("Радиус") { radiusEntry },
                new TableSection("Сетимость") { luminosityEntry },
                new TableSection("Тип звезды") { new ViewCell { View = typePicker } },
            };

            //кнопка "Назад" для возврата из выбора зв. системы или планет
            var cancelButton = new Button { Text = "Назад", TextColor = Color.Black, BackgroundColor = Color.FromHex("9E9E9E") };
            cancelButton.Clicked += (s, e) =>
            {
                ToolbarItems.Add(changehTb);
                NavigationPage.SetHasBackButton(this, true);
                OurStackLayout.Children.Clear();
                OurStackLayout.Children.Add(OurTableView);
            };

            //звездная система           
            var searchConstellButton = new Button { Text = "Звездная система", TextColor = Color.Black, BackgroundColor = Color.FromHex("9E9E9E"), HorizontalOptions = LayoutOptions.Start };
            var searchConstellEntry = new Entry { Placeholder = "Звездная система", Keyboard = Keyboard.Default };
            var searchConstelListView = new ListView() { ItemsSource = Constellations, };
            searchConstellButton.Clicked += (s, e) =>
            {
                NavigationPage.SetHasBackButton(this, false);
                OurStackLayout.Children.Clear();
                searchConstellEntry.TextChanged += (_s, _e) =>
                {
                    searchConstelListView.ItemsSource = Constellations.Search(searchConstellEntry.Text);
                };
                OurStackLayout.Children.Add(searchConstellEntry);
                OurStackLayout.Children.Add(searchConstelListView);
                OurStackLayout.Children.Add(cancelButton);
            };
            searchConstelListView.ItemTapped += (s, e) =>
            {
                searchConstellButton.Text = searchConstelListView.SelectedItem.ToString();
                OurStackLayout.Children.Clear();
                OurStackLayout.Children.Add(OurTableView);
            };
            changeRoot.Add(new TableSection("Звездная система") { new ViewCell { View = searchConstellButton } });

          

            //планеты (добавление)
            var searchPlanetButton = new Button { Text = "Спутники", TextColor = Color.Black, BackgroundColor = Color.FromHex("9E9E9E"), HorizontalOptions = LayoutOptions.Start };
            var searchPlanetEntry = new Entry { Placeholder = "Планета", Keyboard = Keyboard.Default };
            var searchPlanetListView = new ListView { ItemsSource = Planets.Where(planets => planets.Star != star) };
            searchPlanetButton.Clicked += (s, e) =>
            {
                ToolbarItems.Clear();
                NavigationPage.SetHasBackButton(this, false);
                searchPlanetEntry.TextChanged += (_s, _e) =>
                {
                    if (star.Planets != null)
                        searchPlanetListView.ItemsSource = (Planets.Search(searchPlanetEntry.Text)).Where(planets => planets.Star != star);
                    else
                        searchPlanetListView.ItemsSource = Planets.Search(searchPlanetEntry.Text);
                };
                OurStackLayout.Children.Clear();
                OurStackLayout.Children.Add(searchPlanetEntry);
                OurStackLayout.Children.Add(searchPlanetListView);
                OurStackLayout.Children.Add(cancelButton);
            };
            searchPlanetListView.ItemTapped += async (s, e) =>
            {
                NavigationPage.SetHasBackButton(this, false);
                Planet planet = (Planet)searchPlanetListView.SelectedItem;
                if (await DisplayActionSheet(searchPlanetListView.SelectedItem.ToString(), "Отмена", null, "Добавить планету") == "Добавить планету")
                {
                    planet.Star = star;
                    searchPlanetEntry.Text = "";
                    searchPlanetListView.ItemsSource = (Planets.Search(searchPlanetEntry.Text)).Where(planets => planets.Star != star);
                }
            };
           
            //планеты (удалить)
            var deletePlanetButton = new Button { Text = "Править", TextColor = Color.Black, BackgroundColor = Color.FromHex("9E9E9E"), HorizontalOptions = LayoutOptions.Start };
            var deletePlanetEntry = new Entry { Placeholder = "Планета", Keyboard = Keyboard.Default };
            var deletePlanetListView = new ListView { ItemsSource = star.Planets };
            deletePlanetButton.Clicked += (s, e) =>
            {
                ToolbarItems.Clear();
                NavigationPage.SetHasBackButton(this, false);
                deletePlanetEntry.TextChanged += (_s, _e) =>
                {
                    deletePlanetListView.ItemsSource = star.Planets;
                };
                OurStackLayout.Children.Clear();
                OurStackLayout.Children.Add(deletePlanetEntry);
                OurStackLayout.Children.Add(deletePlanetListView);
                OurStackLayout.Children.Add(cancelButton);
            };
            deletePlanetListView.ItemTapped += async (s, e) =>
            {
                NavigationPage.SetHasBackButton(this, false);
                Planet planet = (Planet)deletePlanetListView.SelectedItem;
                if (await DisplayActionSheet(deletePlanetListView.SelectedItem.ToString(), "Отмена", null, "Убрать планету") == "Убрать планету")
                {
                    planet.Star = null;
                    deletePlanetEntry.Text = "";
                    deletePlanetListView.ItemsSource = star.Planets;
                }
            };

            //добавления действий с планетами
            changeRoot.Add(new TableSection("Спутники") { new ViewCell { View = searchPlanetButton }, new ViewCell { View = deletePlanetButton } });
            

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
                        star.Name = nameEntry.Text;
                        star.Weight = Convert.ToDouble(weightEntry.Text);
                        star.Radius = Convert.ToDouble(radiusEntry.Text);
                        star.Luminosity = Convert.ToDouble(luminosityEntry.Text);
                        star.type = (Star.typeOfStar)typePicker.SelectedIndex;
                        if (searchConstelListView.SelectedItem != null)
                            star.Constellation = (Constellation)searchConstelListView.SelectedItem;
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