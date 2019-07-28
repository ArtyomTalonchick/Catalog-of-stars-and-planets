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

        public ConstellationPage(Constellation _constellation)
        {
            InitializeComponent();

            constellation = _constellation;
            if (constellation == null)
                return;

            BindingContext = constellation;
        }


        private async void ChangeConstellation(object s, EventArgs e) => await Navigation.PushAsync(new ConstellationChangePage(constellation));

        private async void DeleteConstellation(object s, EventArgs e)
        {
            constellation.delete();
            await Navigation.PopAsync();
        }

        private async void imageCell_Tapped(object s, EventArgs e)
        {
            await Navigation.PushAsync(new ContentPage()
            {
            Content = new StackLayout
            {
                Children = {
                    new PinchToZoomContainer { WidthRequest = App.ScreenWidth, HeightRequest = App.ScreenHeight,
                        Content = new Image { Source = ImageSource.FromUri(new Uri(constellation.Image)),
                         HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand }
                    }
                }
            }
            });
        }

        //вызывается, когда content является частью другой страницы с другим bindingContext
        public void InitializeContent()
        {
            if (constellation != null) 
            {
                ImageCell.ImageSource = constellation.Image;
                NameCell.Detail = constellation.Name;
            }
            else
            {
                Content = new Label { Text = Language.NotFound };
            }            
        }

        //открывает в браузере
        private void OpenUri (object s, EventArgs e)
        {
            try
            {
                Device.OpenUri(new Uri(constellation.Uri));
            }
            catch
            {
                DisplayAlert(Language.Error, Language.NotFound, Language.Cancel);
            }
        }
    }
}