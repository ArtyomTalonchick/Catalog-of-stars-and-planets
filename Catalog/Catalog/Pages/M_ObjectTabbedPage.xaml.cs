using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Catalog.CollClass;
using Xamarin.Essentials;

namespace Catalog.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class M_ObjectTabbedPage : TabbedPage
    {
        private M_object m_Object { get; set; }

        public M_ObjectTabbedPage (M_object _m_Object)
        {
            InitializeComponent();
            BackgroundColor = Setting.BackColor;
            BarBackgroundColor = Setting.BarColor;

            m_Object = _m_Object;
            Children.Add(new M_ObjectPage(m_Object));
            InitializeConstellationPage();
        }


        private void InitializeConstellationPage()
        {
            var pageForConstellation = new ContentPage();
            var goButton = new Button();
            goButton.Style = Setting.ButtonStyle;
            if (m_Object.Constellation == null)
            {
                pageForConstellation.Title = Language.Constellation;
                goButton.Text = Language.NotFound;
            }
            else
            {
                pageForConstellation.Title = m_Object.Constellation.Name;
                goButton.Text = Language.GoToConstellation;
                goButton.Clicked += async (s, e) =>
                {
                //    await Navigation.PopAsync();
                    await Navigation.PushAsync(new ConstellationTabbedPage(m_Object.Constellation));
                };
            }
            var constellationPage = new ConstellationPage(m_Object.Constellation);
            constellationPage.InitializeContent();
            pageForConstellation.Content = new StackLayout
            { Children = { goButton, constellationPage.Content } };

            Children.Add(pageForConstellation);
        }

    }
}