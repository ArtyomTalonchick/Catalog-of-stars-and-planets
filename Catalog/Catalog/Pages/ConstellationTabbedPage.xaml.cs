using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Catalog.CollClass;

using Xamarin.Essentials;

namespace Catalog.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConstellationTabbedPage : TabbedPage
    {
        public ConstellationTabbedPage (Constellation _constellation)
        {
            InitializeComponent();
            BackgroundColor = Setting.BackColor;
            BarBackgroundColor = Setting.BarColor;

            Children.Add(new ConstellationPage(_constellation));
            Children.Add(new CollectionPage(Data.AllConstellations, _constellation.Stars, Data.AllPlanets, Data.AllM_Ojects, CollectionPage.TypeItemsOfColl.Star));
            Children.Add(new CollectionPage(Data.AllConstellations, Data.AllStars, Data.AllPlanets, _constellation.M_objects, CollectionPage.TypeItemsOfColl.M_Object));
        }
    }
}