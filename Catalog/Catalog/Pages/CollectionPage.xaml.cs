using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Catalog.CollClass;
using Xamarin.Essentials;

namespace Catalog.Pages
{
    
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CollectionPage : ContentPage
    {      
        public enum TypeItemsOfColl { Constellation, Star, Planet, M_Object };
        private TypeItemsOfColl TypeItem { get; set; }

        private MyCollection<Constellation> Constellations { get; set; }
        private MyCollection<Star> Stars { get; set; }
        private MyCollection<Planet> Planets { get; set; }
        private MyCollection<M_object> M_objects { get; set; }

        
        public CollectionPage(MyCollection<Constellation> _constellations, MyCollection<Star> _stars, MyCollection<Planet> _planets, MyCollection<M_object> _m_Objects, TypeItemsOfColl _typeItem)
        {
            InitializeComponent();

            Constellations = _constellations;
            Stars = _stars;
            Planets = _planets;
            M_objects = _m_Objects;

            TypeItem = _typeItem;
            
            if (TypeItem == TypeItemsOfColl.Constellation)
            {
                Title = Language.Constellation;
                OurListView.ItemsSource = Constellations;
                OurListView.ItemTemplate = new DataTemplate(() =>
                {
                    ImageCell imageCell = new ImageCell { TextColor = Setting.TextColor, DetailColor = Setting.DetailColor };
                    imageCell.SetBinding(ImageCell.TextProperty, "Name");
                    imageCell.SetBinding(ImageCell.ImageSourceProperty, "Image");
                    return imageCell;
                });                
            }
            else if (TypeItem == TypeItemsOfColl.Star)
            {
                Title = Language.Star;
                OurListView.ItemsSource = Stars;
                OurListView.ItemTemplate = new DataTemplate(() =>
                {
                    ImageCell imageCell = new ImageCell { TextColor = Setting.TextColor, DetailColor = Setting.DetailColor };
                    imageCell.SetBinding(ImageCell.TextProperty, "Name");
                    Binding typeBinding = new Binding { Path = "Constellation", StringFormat = Language.Constellation + ": {0}" };
                    imageCell.SetBinding(ImageCell.DetailProperty, typeBinding);
                    imageCell.SetBinding(ImageCell.ImageSourceProperty, "Image.Source");
                    return imageCell;
                });                
            }
            else if (TypeItem == TypeItemsOfColl.Planet)
            {
                Title = Language.Planet;
                OurListView.ItemsSource = Planets;
                OurListView.ItemTemplate = new DataTemplate(() =>
                {
                    ImageCell imageCell = new ImageCell { TextColor = Setting.TextColor, DetailColor = Setting.DetailColor };
                    imageCell.SetBinding(ImageCell.TextProperty, "Name");
                    Binding typeBinding = new Binding { Path = "Star", StringFormat = Language.Star + ": {0}" };
                    imageCell.SetBinding(ImageCell.DetailProperty, typeBinding);
                    imageCell.SetBinding(ImageCell.ImageSourceProperty, "Image.Source");
                    return imageCell;
                });               
            }
            else if (TypeItem == TypeItemsOfColl.M_Object)
            {
                Title = Language.M_Object;
                OurListView.ItemsSource = M_objects;
                OurListView.ItemTemplate = new DataTemplate(() =>
                {
                    ImageCell imageCell = new ImageCell { TextColor = Setting.TextColor, DetailColor = Setting.DetailColor };
                    imageCell.SetBinding(ImageCell.TextProperty, "Name");
                    Binding typeBinding = new Binding { Path = "Number", StringFormat = "№ {0}" };
                    imageCell.SetBinding(ImageCell.DetailProperty, typeBinding);
                    imageCell.SetBinding(ImageCell.ImageSourceProperty, "Image");                    
                    return imageCell;
                });
                ToolbarItems.Clear();
            }
        }

        private async void AddItem(object s, EventArgs e)
        {
            if (TypeItem == TypeItemsOfColl.Constellation)
            {
                await Navigation.PushAsync(new ConstellationChangePage(null));
            }
            else if (TypeItem == TypeItemsOfColl.Star)
            {
                await Navigation.PushAsync(new StarChangePage(null));
            }
            else if (TypeItem == TypeItemsOfColl.Planet)
            {
                await Navigation.PushAsync(new PlanetChangePage(null));
            }
            else if (TypeItem == TypeItemsOfColl.M_Object)
            {

            };
        }

        private void searchBar_Changed(object sender, EventArgs e)
        {
            if (TypeItem == TypeItemsOfColl.Constellation)
                OurListView.ItemsSource = Constellations.Search(searchBar.Text);
            else if (TypeItem == TypeItemsOfColl.Star)
                OurListView.ItemsSource = Stars.Search(searchBar.Text);
            else if (TypeItem == TypeItemsOfColl.Planet)
                OurListView.ItemsSource = Planets.Search(searchBar.Text);
        }

        private async void OurListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if(TypeItem == TypeItemsOfColl.Constellation)
                await Navigation.PushAsync(new ConstellationTabbedPage((Constellation)e.Item));
            else if (TypeItem == TypeItemsOfColl.Star)
                await Navigation.PushAsync(new StarTabbedPage((Star)e.Item));
            else if (TypeItem == TypeItemsOfColl.Planet)
                await Navigation.PushAsync(new PlanetTabbedPage((Planet)e.Item));
            else if (TypeItem == TypeItemsOfColl.M_Object)
                await Navigation.PushAsync(new M_ObjectTabbedPage((M_object)e.Item));
        }

        private void OurListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}