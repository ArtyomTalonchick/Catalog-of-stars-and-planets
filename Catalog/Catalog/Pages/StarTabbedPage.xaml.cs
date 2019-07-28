using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Catalog.CollClass;
using Xamarin.Essentials;

namespace Catalog.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StarTabbedPage : TabbedPage
    {
        private Star star { get; set; }

        public StarTabbedPage (Star _star)
        {
            InitializeComponent();
            BackgroundColor = Setting.BackColor;
            BarBackgroundColor = Setting.BarColor;

            star = _star;

            Children.Add(new StarPage(star));
            InitializeConstellationPage();
            Children.Add(new CollectionPage(Data.AllConstellations, Data.AllStars, star.Planets, Data.AllM_Ojects, CollectionPage.TypeItemsOfColl.Planet));
        }

        private void InitializeConstellationPage()
        {

            var pageForConstellation = new ContentPage();
            var goButton = new Button();
            goButton.Style = Setting.ButtonStyle;
            if (star.Constellation == null)
            {
                pageForConstellation.Title = Language.Constellation;
                goButton.Text = Language.NotFound;
            }
            else
            {
                pageForConstellation.Title = star.Constellation.Name;
                goButton.Text = Language.GoToConstellation;
                goButton.Clicked += async (s, e) =>
                {
                //    await Navigation.PopAsync();
                    await Navigation.PushAsync(new ConstellationTabbedPage(star.Constellation));
                };
            }
            var constellationPage = new ConstellationPage(star.Constellation);
            constellationPage.InitializeContent();
            pageForConstellation.Content = new StackLayout
            { Children = { goButton, constellationPage.Content } };
            Children.Add(pageForConstellation);
        }

    }
}