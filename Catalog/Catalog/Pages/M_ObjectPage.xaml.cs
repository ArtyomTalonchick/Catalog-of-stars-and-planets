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
    public partial class M_ObjectPage : ContentPage
    {
        private M_object m_object { get; set; }


        public M_ObjectPage(M_object _m_object)
        {
            InitializeComponent();
            m_object = _m_object;

            if (m_object == null)
                return;

            BindingContext = m_object;

            GlitterCell.Detail += " (m)";
            SizeCell.Detail += " (ps)";
            DistanceCell.Detail += " (ps)";
        }

        private async void imageCell_Tapped(object s, EventArgs e)
        {
            await Navigation.PushAsync(new ContentPage()
            {
                Content = new StackLayout
                {
                    Children = {
                    new PinchToZoomContainer { WidthRequest = App.ScreenWidth, HeightRequest = App.ScreenHeight,
                        Content = new Image { Source = ImageSource.FromUri(new Uri(m_object.Image)),
                         HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand }
                    }
                }
                }
            });
        }

        //открывает в браузере
        private void OpenUri(object s, EventArgs e)
        {
            try
            {
                Device.OpenUri(new Uri(m_object.Uri));
            }
            catch
            {
                DisplayAlert(Language.Error, Language.NotFound, Language.Cancel);
            }
        }

    }
}