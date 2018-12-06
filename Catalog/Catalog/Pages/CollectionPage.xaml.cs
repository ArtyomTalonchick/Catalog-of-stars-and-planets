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
    public partial class CollectionPage : ContentPage
    {
        private class BindCollection<T> : BindableObject
        {
            public static readonly BindableProperty collectionProperty = BindableProperty.Create("collection", typeof(MyCollection<T>), typeof(ListView));
            public MyCollection<T> collection
            {
                get => (MyCollection<T>) GetValue(collectionProperty);
                set => SetValue(collectionProperty, value);
            }
        }

        public enum TypeItemsOfColl { Constellation, Star, Planet };
        private TypeItemsOfColl TypeItem { get; set; }
        private MyCollection<Constellation> Constellations { get; set; }
        private MyCollection<Star> Stars { get; set; }
        private MyCollection<Planet> Planets { get; set; }

        private Star newStar { get; set; }
        private Editor searchEditor { get; set; }
        private bool isSearch { get; set; }

        private CollectionPage()
        {
            InitializeComponent();
            ToolbarInitialize();
            searchEditor = new Editor();
            searchEditor.TextChanged += (_s, _e) => searchEditor_Changed(_s, _e);
            isSearch = false;
        }
        public CollectionPage(MyCollection<Constellation> constellations, MyCollection<Star> stars, MyCollection<Planet> planets, TypeItemsOfColl typeItem) : this()
        {
            Constellations = constellations;
            Stars = stars;
            Planets = planets;
            TypeItem = typeItem;

            if (TypeItem == TypeItemsOfColl.Constellation)
            {
                Title = "Звездные системы";
                OurListView.ItemsSource = Constellations;
            //    OurListView.BindingContext = constellations;
              //  OurListView.SetBinding(ListView.ItemsSourceProperty, "collectionProperty");
            }
            else if (TypeItem == TypeItemsOfColl.Star)
            {
                Title = "Звезды";
                BindCollection<Star> st = new BindCollection<Star> { collection = Stars };
                OurListView.ItemsSource = st.collection;
                OurListView.BindingContext = st;
                OurListView.SetBinding(ListView.ItemsSourceProperty, "collection");
            }
            else if (TypeItem == TypeItemsOfColl.Planet)
            {
                Title = "Планеты";
                OurListView.ItemsSource = Planets;
           //     OurListView.BindingContext = planets;
             //   OurListView.SetBinding(ListView.ItemsSourceProperty, "PropertyChanged");
            }
        }
       
        private void ToolbarInitialize()
        {
            ToolbarItem searchTb = new ToolbarItem()
            {
                Text = "Поиск",
                Order = ToolbarItemOrder.Secondary,
                Priority = 1,

            };
            ToolbarItem addTb = new ToolbarItem()
            {
                Text = "Добавить",
                Order = ToolbarItemOrder.Secondary,
                Priority = 1
            };
            searchTb.Clicked += (s, e) => searchTb_Clicked(s, e);
            addTb.Clicked += async (s, e) => await Navigation.PushAsync(new Pages.AddItemPage(Constellations, Stars, Planets, TypeItem));
            ToolbarItems.Add(searchTb);
            ToolbarItems.Add(addTb);
        }

        private void searchTb_Clicked (object sender, EventArgs e)
        {
            isSearch = !isSearch;
            if(isSearch)
            {
                OurStackLayout.Children.Clear();
                OurStackLayout.Children.Add(searchEditor);
                OurStackLayout.Children.Add(OurListView);
            }
            else
            {
                OurStackLayout.Children.Clear();
                OurStackLayout.Children.Add(OurListView);
            }
        }
        
        private void searchEditor_Changed(object sender, EventArgs e)
        {
            if (TypeItem == TypeItemsOfColl.Constellation)
                OurListView.ItemsSource = Constellations.Search(((Editor)sender).Text);
            else if (TypeItem == TypeItemsOfColl.Star)
                OurListView.ItemsSource = Stars.Search(((Editor)sender).Text);
            else if (TypeItem == TypeItemsOfColl.Planet)
                OurListView.ItemsSource = Planets.Search(((Editor)sender).Text);
        }

        private async void OurListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if(TypeItem == TypeItemsOfColl.Constellation)
                await Navigation.PushAsync(new Pages.ConstellationPage((Constellation)e.Item, Constellations, Stars, Planets));
            else if (TypeItem == TypeItemsOfColl.Star)
                await Navigation.PushAsync(new Pages.StarPage((Star)e.Item, Constellations, Stars, Planets));
            else if (TypeItem == TypeItemsOfColl.Planet)
                await Navigation.PushAsync(new Pages.PlanetPage((Planet)e.Item, Constellations, Stars, Planets));
        }
    }
}