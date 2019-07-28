using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Catalog.CollClass;
using Xamarin.Essentials;

namespace Catalog.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlanetTabbedPage : TabbedPage
    {
        private Planet planet { get; set; }

        public PlanetTabbedPage (Planet _planet)
        {
            InitializeComponent();
            BackgroundColor = Setting.BackColor;
            BarBackgroundColor = Setting.BarColor;

            planet = _planet;
            Children.Add(new PlanetPage(planet));
            InitializeStarPage();
        }

        private void InitializeStarPage()
        {
            var pageForStar = new ContentPage();
            var goButton = new Button();
            goButton.Style = Setting.ButtonStyle;
            if (planet.Star == null)
            {
                pageForStar.Title = Language.Star;
                goButton.Text = Language.NotFound;
            }
            else
            {
                pageForStar.Title = planet.Star.Name;
                goButton.Text = Language.GoToStar;
                goButton.Clicked += async (s, e) =>
                {
                //    await Navigation.PopAsync();
                    await Navigation.PushAsync(new StarTabbedPage(planet.Star));
                };
            }
            var planetPage = new StarPage(planet.Star);
            planetPage.InitializeContent();
            pageForStar.Content = new StackLayout
            { Children = { goButton, planetPage.Content } };
            Children.Add(pageForStar);
        }

    }
}