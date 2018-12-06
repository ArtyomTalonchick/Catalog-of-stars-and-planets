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
    public partial class ConstellationPage : ContentPage
    {
        private Constellation constellation { get; set; }
        private MyCollection<Constellation> Constellations { get; set; }
        private MyCollection<Star> Stars { get; set; }
        private MyCollection<Planet> Planets { get; set; }
        private ToolbarItem changehTb { get; set; }


        private ConstellationPage ()
        {
            InitializeComponent();
        }
        public ConstellationPage (Constellation _constellation, MyCollection<Constellation> _constellations, MyCollection<Star> _stars, MyCollection<Planet> _planets) : this()
		{
            constellation = _constellation;
            Title = "Звездная система - " + constellation.Name;
            Constellations = _constellations;
            Stars = _stars;
            Planets = _planets;

            data();
            change();
        }

        private void data()
        {           
            var starList = new ListView { ItemsSource = constellation.Stars};
            starList.ItemTapped += async (s, e) => await Navigation.PushAsync(new Pages.StarPage((Star)starList.SelectedItem, Constellations, Stars, Planets));
            var starButton = new Button { Text = "Просмотерть", TextColor = Color.Black, BackgroundColor = Color.FromHex("9E9E9E"), HorizontalOptions = LayoutOptions.Start };
            var cancelButton = new Button { Text = "Назад", TextColor = Color.Black, BackgroundColor = Color.FromHex("9E9E9E"), HorizontalOptions = LayoutOptions.CenterAndExpand };
            cancelButton.Clicked += (s, e) =>
            {
                ToolbarItems.Add(changehTb);
                NavigationPage.SetHasBackButton(this, true);
                OurStackLayout.Children.Clear();
                OurStackLayout.Children.Add(OurTableView);
            };
            starButton.Clicked += async (s, e) =>
            {
                ToolbarItems.Clear();
                NavigationPage.SetHasBackButton(this, false);
                OurStackLayout.Children.Clear();
                OurStackLayout.Children.Add(starList);
                OurStackLayout.Children.Add(cancelButton);
            };

            var dataRoot = new TableRoot()
            {
                new TableSection("Название") { new TextCell{ Text=constellation.Name, TextColor=Color.Black } },
                new TableSection("Звезды"){new ViewCell { View = starButton} }
            };
            OurTableView.Root = dataRoot;
        }
        
        private void change()
        {
            //данные о зв. системе
            var nameEntry = new EntryCell { Text = constellation.Name, Keyboard = Keyboard.Default };
            var changeRoot = new TableRoot() { new TableSection("Название") { nameEntry }, };

            //кнопка "назад" для возврата из выбора звезд
            var cancelButton = new Button { Text = "Назад", TextColor = Color.Black, BackgroundColor = Color.FromHex("9E9E9E") };
            cancelButton.Clicked += (s, e) =>
            {
                ToolbarItems.Add(changehTb);
                NavigationPage.SetHasBackButton(this, true);
                OurStackLayout.Children.Clear();
                OurStackLayout.Children.Add(OurTableView);
            };

            //звезды (добавление)
            var searchStarButton = new Button { Text = "Звезды", TextColor = Color.Black, BackgroundColor = Color.FromHex("9E9E9E"), HorizontalOptions = LayoutOptions.Start };
            var searchStarEntry = new Entry { Placeholder = "Звезда", Keyboard = Keyboard.Default };
            var searchStarListView = new ListView { ItemsSource = Stars.Where(stars => stars.Constellation!= constellation) };
            searchStarButton.Clicked += (s, e) =>
            {
                ToolbarItems.Clear();
                NavigationPage.SetHasBackButton(this, false);
                searchStarEntry.TextChanged += (_s, _e) =>
                {
                    if (constellation.Stars!= null)
                        searchStarListView.ItemsSource = (Stars.Search(searchStarEntry.Text)).Where(stars => stars.Constellation != constellation);
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
                NavigationPage.SetHasBackButton(this, false);
                Star star = (Star)searchStarListView.SelectedItem;
                if (await DisplayActionSheet(searchStarListView.SelectedItem.ToString(), "Отмена", null, "Добавить звезду") == "Добавить звезду")
                {
                    star.Constellation = constellation;
                    searchStarEntry.Text = "";
                    searchStarListView.ItemsSource = (Stars.Search(searchStarEntry.Text)).Where(stars => stars.Constellation != constellation);
                }
            };
        
            //звезды (удаление)
            var deleteStarButton = new Button { Text = "Править", TextColor = Color.Black, BackgroundColor = Color.FromHex("9E9E9E"), HorizontalOptions = LayoutOptions.Start };
            var deleteStarEntry = new Entry { Placeholder = "Звезда", Keyboard = Keyboard.Default };
            var deleteStarListView = new ListView { ItemsSource = constellation.Stars };
            deleteStarButton.Clicked += (s, e) =>
            {
                ToolbarItems.Clear();
                NavigationPage.SetHasBackButton(this, false);
                deleteStarEntry.TextChanged += (_s, _e) =>
                {
                    deleteStarListView.ItemsSource = constellation.Stars;
                };
                OurStackLayout.Children.Clear();
                OurStackLayout.Children.Add(deleteStarEntry);
                OurStackLayout.Children.Add(deleteStarListView);
                OurStackLayout.Children.Add(cancelButton);
            };
            deleteStarListView.ItemTapped += async (s, e) =>
            {
                NavigationPage.SetHasBackButton(this, false);
                Star star = (Star)deleteStarListView.SelectedItem;
                if (await DisplayActionSheet(deleteStarListView.SelectedItem.ToString(), "Отмена", null, "Убрать звезду") == "Убрать звезду")
                {
                    star.Constellation = null;
                    deleteStarEntry.Text = "";
                    deleteStarListView.ItemsSource = constellation.Stars;
                }
            };
            changeRoot.Add(new TableSection("Звезды") { new ViewCell { View = searchStarButton }, new ViewCell { View = deleteStarButton } });
            
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
                        constellation.Name = nameEntry.Text;
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