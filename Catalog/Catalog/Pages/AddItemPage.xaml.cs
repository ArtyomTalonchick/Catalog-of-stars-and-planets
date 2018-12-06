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
    public partial class AddItemPage : ContentPage
    {
        private CollectionPage.TypeItemsOfColl TypeItem { get; set; }
        private MyCollection<Constellation> Constellations { get; set; }
        private MyCollection<Star> Stars { get; set; }
        private MyCollection<Planet> Planets { get; set; }

        private Constellation newConstellation { get; set; }
        private Star newStar { get; set; }
        private Planet newPlanet { get; set; }

        private TableRoot root;
        private TableView tableview;
        private EntryCell nameEntry;
        private EntryCell weightEntry;
        private EntryCell radiusEntry;
        private EntryCell luminosityEntry;
        private Picker typePicker;
        private ListView searchParentListView;
        private Button SaveButton;
        private Button cancelButton;

        private AddItemPage()
        {
            InitializeComponent();
        }
        public AddItemPage(MyCollection<Constellation> constellations, MyCollection<Star> stars, MyCollection<Planet> planets, CollectionPage.TypeItemsOfColl typeItem) : this()
        {
            Constellations = constellations;
            Stars = stars;
            Planets = planets;
            TypeItem = typeItem;

            cancelButton = new Button { Text = "Назад", TextColor = Color.Black, BackgroundColor = Color.FromHex("9E9E9E") };
            cancelButton.Clicked += (s, e) =>
            {
                NavigationPage.SetHasBackButton(this, true);
                OurStackLayout.Children.Clear();
                OurStackLayout.Children.Add(tableview);
                OurStackLayout.Children.Add(SaveButton);
            };

            AddItem();
        }

        private void AddItem()
        {
            root = new TableRoot();
            tableview = new TableView() { Root = root };

            if (TypeItem == CollectionPage.TypeItemsOfColl.Constellation)
                AddConstellation();
            else if (TypeItem == CollectionPage.TypeItemsOfColl.Star)
                AddStar();
            else if (TypeItem == CollectionPage.TypeItemsOfColl.Planet)
                AddPlanet();

            SaveButton = new Button() { Text = "Добавить" };
            SaveButton.Clicked += (s, e_) => SaveButton_Clicked(s, e_);

            OurStackLayout.Children.Add(tableview);
            OurStackLayout.Children.Add(SaveButton);
        }

        private void AddConstellation()
        {
            newConstellation = new Constellation("");
            Title = "Добавление зв. системы";

            //данные о звездной системе
            nameEntry = new EntryCell { Label = "Название:", Placeholder = "Введите название", Keyboard = Keyboard.Default };
            root.Add(new TableSection("Общие данные") { nameEntry});
            
            //звезды
            var searchStarButton = new Button { Text = "Звезды", TextColor = Color.Black, BackgroundColor = Color.FromHex("9E9E9E"), HorizontalOptions = LayoutOptions.Start };
            var searchStarEntry = new Entry { Placeholder = "Звезда", Keyboard = Keyboard.Default };
            var searchStarListView = new ListView { ItemsSource = Stars};
            searchStarButton.Clicked += (s, e) =>
            {
                NavigationPage.SetHasBackButton(this, false);
                searchStarEntry.TextChanged += (_s, _e) =>
                {
                    if (newConstellation.Stars != null)
                        searchStarListView.ItemsSource = (Stars.Search(searchStarEntry.Text)).Where(stars => stars.Constellation != newConstellation);
                    else
                        searchStarListView.ItemsSource = Stars.Search(searchStarEntry.Text);
                };
                OurStackLayout.Children.Clear();
                OurStackLayout.Children.Add(searchStarEntry);
                OurStackLayout.Children.Add(searchStarListView);
                OurStackLayout.Children.Add(cancelButton);
            };
            searchStarListView.ItemTapped += async (s, e) =>
            {
                Planet planet = (Planet)searchStarListView.SelectedItem;
                if (await DisplayActionSheet(searchStarListView.SelectedItem.ToString(), "Отмена", null, "Добавить звезду") == "Добавить звезду")
                {
                    planet.Star = newStar;
                    searchStarEntry.Text = "";
                    searchStarListView.ItemsSource = (Stars.Search(searchStarEntry.Text)).Where(stars => stars.Constellation != newConstellation);
                }
            };
            root.Add(new TableSection("Звезды") { new ViewCell { View = searchStarButton } });
        }

        private void AddStar()
        {
            newStar = new Star("");
            Title = "Добавление звезды";

            //данные о звезде
            nameEntry = new EntryCell { Label = "Название:", Placeholder = "Введите название", Keyboard = Keyboard.Default };
            weightEntry = new EntryCell { Label = "Масса:", Placeholder = "Введите массу в килограммах", Keyboard = Keyboard.Numeric, };
            radiusEntry = new EntryCell { Label = "Радиус:", Placeholder = "Введите радиус в метрах", Keyboard = Keyboard.Numeric };
            luminosityEntry = new EntryCell { Label = "Светимость:", Placeholder = "Введите светимость в лм", Keyboard = Keyboard.Numeric };
            root.Add(new TableSection("Общие данные") { nameEntry, weightEntry, radiusEntry, luminosityEntry });
            typePicker = new Picker { Title = "Тип звезды", ItemsSource = Enum.GetNames(typeof(Star.typeOfStar)) };
            root.Add(new TableSection("Тип звезды") { new ViewCell { View = typePicker } });
                       
            //звездная система
            var searchConstellButton = new Button { Text = "Звездная система", TextColor = Color.Black, BackgroundColor = Color.FromHex("9E9E9E"), HorizontalOptions = LayoutOptions.Start };
            var searchConstellEntry = new Entry { Placeholder = "Звездная система", Keyboard = Keyboard.Default };
            searchParentListView = new ListView() { ItemsSource = Constellations, };
            searchConstellButton.Clicked += (s, e) =>
            {
                NavigationPage.SetHasBackButton(this, false);
                OurStackLayout.Children.Clear();
                searchConstellEntry.TextChanged += (_s, _e) =>
                {
                    searchParentListView.ItemsSource = Constellations.Search(searchConstellEntry.Text);
                };
                OurStackLayout.Children.Add(searchConstellEntry);
                OurStackLayout.Children.Add(searchParentListView);
                OurStackLayout.Children.Add(cancelButton);
            };
            searchParentListView.ItemTapped += (s, e) =>
            {
                searchConstellButton.Text = searchParentListView.SelectedItem.ToString();
                OurStackLayout.Children.Clear();
                OurStackLayout.Children.Add(tableview);
                OurStackLayout.Children.Add(SaveButton);
            };
            root.Add(new TableSection("Звездная система") { new ViewCell { View = searchConstellButton } });

            //планеты
            var searchPlanetButton = new Button { Text = "Спутники", TextColor = Color.Black, BackgroundColor = Color.FromHex("9E9E9E"), HorizontalOptions = LayoutOptions.Start };
            var searchPlanetEntry = new Entry { Placeholder = "Планета", Keyboard = Keyboard.Default };
            var searchPlanetListView = new ListView { ItemsSource = Planets, };
            searchPlanetButton.Clicked += (s, e) =>
            {
                NavigationPage.SetHasBackButton(this, false);
                OurStackLayout.Children.Clear();
                searchPlanetEntry.TextChanged += (_s, _e) =>
                {
                    if (newStar.Planets != null)
                        searchPlanetListView.ItemsSource = (Planets.Search(searchPlanetEntry.Text)).Where(planets => planets.Star != newStar);
                    else
                        searchPlanetListView.ItemsSource = Planets.Search(searchPlanetEntry.Text);
                };
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
                    planet.Star = newStar;
                    searchPlanetEntry.Text = "";
                    searchPlanetListView.ItemsSource = (Planets.Search(searchPlanetEntry.Text)).Where(planets => planets.Star != newStar);
                }
            };

            root.Add(new TableSection("Спутники") { new ViewCell { View = searchPlanetButton } });
        }

        private void AddPlanet()
        {
            newPlanet = new Planet("");
            Title = "Добавление планеты";

            //данные о планете
            nameEntry = new EntryCell { Label = "Название:", Placeholder = "Введите название", Keyboard = Keyboard.Default };
            weightEntry = new EntryCell { Label = "Масса:", Placeholder = "Введите массу в килограммах", Keyboard = Keyboard.Numeric, };
            radiusEntry = new EntryCell { Label = "Радиус:", Placeholder = "Введите радиус в метрах", Keyboard = Keyboard.Numeric };
            root.Add(new TableSection("Общие данные") { nameEntry, weightEntry, radiusEntry });

            //звезда
            var searchStarButton = new Button { Text = "Звезда", TextColor = Color.Black, BackgroundColor = Color.FromHex("9E9E9E"), HorizontalOptions = LayoutOptions.Start };
            var searchStarEntry = new Entry { Placeholder = "Звезда", Keyboard = Keyboard.Default };
            searchParentListView = new ListView() { ItemsSource = Stars };
            searchStarButton.Clicked += (s, e) =>
            {
                NavigationPage.SetHasBackButton(this, false);
                OurStackLayout.Children.Clear();
                searchStarEntry.TextChanged += (_s, _e) =>
                {
                    searchParentListView.ItemsSource = Stars.Search(searchStarEntry.Text);
                };
                OurStackLayout.Children.Add(searchStarEntry);
                OurStackLayout.Children.Add(searchParentListView);
                OurStackLayout.Children.Add(cancelButton);
            };
            searchParentListView.ItemTapped += (s, e) =>
            {
                searchStarButton.Text = searchParentListView.SelectedItem.ToString();
                OurStackLayout.Children.Clear();
                OurStackLayout.Children.Add(tableview);
                OurStackLayout.Children.Add(SaveButton);
            };
            root.Add(new TableSection("Звезда") { new ViewCell { View = searchStarButton } });
        }
        

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            if (TypeItem == CollectionPage.TypeItemsOfColl.Constellation)
            {
                Constellations.Add(newConstellation);
                newConstellation.Name = nameEntry.Text;              
            }
            else if (TypeItem == CollectionPage.TypeItemsOfColl.Star)
            {
                Stars.Add(newStar);
                newStar.Name = nameEntry.Text;
                newStar.Weight = Convert.ToDouble(weightEntry.Text);
                newStar.Radius = Convert.ToDouble(radiusEntry.Text);
                newStar.Luminosity = Convert.ToDouble(luminosityEntry.Text);
                newStar.type = (Star.typeOfStar)typePicker.SelectedIndex;
                if (searchParentListView.SelectedItem != null)
                    newStar.Constellation = (Constellation)searchParentListView.SelectedItem;
            }
            else if (TypeItem == CollectionPage.TypeItemsOfColl.Planet)
            {
                Planets.Add(newPlanet);
                newPlanet.Name = nameEntry.Text;
                newPlanet.Weight = Convert.ToDouble(weightEntry.Text);
                newPlanet.Radius = Convert.ToDouble(radiusEntry.Text);
                if (searchParentListView.SelectedItem != null)
                    newPlanet.Star = (Star)searchParentListView.SelectedItem;
            }
            
            await Navigation.PopAsync();
        }
    }
}