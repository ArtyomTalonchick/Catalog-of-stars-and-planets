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
	public partial class ConstellationChangePage : ContentPage
	{
        private Constellation constellation { get; set; }
        private bool isNewConstellation { get; set; }

		public ConstellationChangePage (Constellation _constellation)
		{
			InitializeComponent ();
            searcStarButton.Style = Setting.ButtonStyle;
            deleteStarButton.Style = Setting.ButtonStyle;


            if (_constellation != null)
            {
                constellation = _constellation;
                isNewConstellation = false;
            }
            else
            {
                constellation = new Constellation("");
                isNewConstellation = true;
            }

            //данные о созвездии
            this.BindingContext = constellation;
            
            NavigationPage.SetHasBackButton(this, false);
        }

        private async void Back(object s, EventArgs e) => await Navigation.PopAsync();

        private async void Save(object s, EventArgs e)
        {
            constellation.Name = nameEntry.Text;
            constellation.Image = imageEntry.Text;
            constellation.Uri = uriEntry.Text;
            if (isNewConstellation)
                Data.AllConstellations.Add(constellation);
            await Navigation.PopAsync();          
        }

        //звезды (добавление)
        private async void searchStarButton_Clicked(object s, EventArgs e)
        {
            var searchStarEntry = new Entry { Placeholder = Language.Star, Keyboard = Keyboard.Default };
            var searchStarListView = new ListView { ItemsSource = Data.AllStars.Where(stars => stars.Constellation == null) };
            searchStarEntry.TextChanged += (_s, _e) =>
            {
            //    if (constellation.Stars != null)
                    searchStarListView.ItemsSource = (Data.AllStars.Search(searchStarEntry.Text)).Where(stars => stars.Constellation == null);
              //  else
                //    searchStarListView.ItemsSource = Data.AllStars.Search(searchStarEntry.Text);
            };
            searchStarListView.ItemTapped += async (_s, _e) =>
            {
                Star star = (Star)searchStarListView.SelectedItem;
                if (await DisplayActionSheet(searchStarListView.SelectedItem.ToString(), Language.Cancel, null, Language.Add) == Language.Add)
                {
                    star.Constellation = constellation;
                    searchStarEntry.Text = "";
                    searchStarListView.ItemsSource = (Data.AllStars.Search(searchStarEntry.Text)).Where(stars => stars.Constellation != constellation);
                }
            };
            await Navigation.PushAsync(new ContentPage { Content = new StackLayout { Children = { searchStarEntry, searchStarListView } } });
        }

        //звезды (удалить)
        private async void deleteStarButton_Clicked(object s, EventArgs e)
        {
            var deleteStarEntry = new Entry { Placeholder = Language.Star, Keyboard = Keyboard.Default };
            var deleteStarListView = new ListView { ItemsSource = constellation.Stars };
            deleteStarEntry.TextChanged += (_s, _e) =>
            {
                deleteStarListView.ItemsSource = constellation.Stars.Search(deleteStarEntry.Text);
            };
            deleteStarListView.ItemTapped += async (_s, _e) =>
            {
                Star star = (Star)deleteStarListView.SelectedItem;
                if (await DisplayActionSheet(deleteStarListView.SelectedItem.ToString(), Language.Cancel, null, Language.Delete) == Language.Delete)
                {
                    star.Constellation = null;
                    deleteStarEntry.Text = "";
                    deleteStarListView.ItemsSource = constellation.Stars.Search(deleteStarEntry.Text);
                }
            };
            await Navigation.PushAsync(new ContentPage { Content = new StackLayout { Children = { deleteStarEntry, deleteStarListView } } });
        }


    }
}