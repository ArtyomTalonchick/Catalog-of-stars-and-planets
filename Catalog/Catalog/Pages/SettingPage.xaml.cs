using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Threading.Tasks;
using System.Globalization;

namespace Catalog
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingPage : ContentPage
	{
        private NavigationPage navPage;
        private List<(Color back, Color bar)> listColor;
        private Action ColorChange;
        private Action<string> LanguageChange;

        public SettingPage (NavigationPage _navPage, Action colorChange, Action<string> languageChange)
		{
            InitializeComponent();
            navPage = _navPage;
            ColorChange = colorChange;
            LanguageChange = languageChange;
            InitializeColor();

            thBt.BackgroundColor = Setting.BarColor;
            txBt.BackgroundColor = Setting.TextColor;
            dtBt.BackgroundColor = Setting.DetailColor;
            btBt.BackgroundColor = Setting.ButtonColor;
       //     lanBt.Style = Setting.ButtonStyle;
         //   lanBt.Text = Setting.Language == "" ? "English" : "Русский";
            
            anmSw.IsToggled = Setting.Animation;
        }

        private void InitializeColor ()
        {
            listColor = new List<(Color back, Color bar)>
            {
                (Color.FromHex("FFFFFF"), (Color.FromHex("FAFAFA"))),
                (Color.FromHex("FFCDD2"), (Color.FromHex("B71C1C"))),    //красный
                (Color.FromHex("FF80AB"), (Color.FromHex("880E4F"))),    //розовый
                (Color.FromHex("EA80FC"), (Color.FromHex("4A148C"))),    //фиолетовый темный
                (Color.FromHex("B388FF"), (Color.FromHex("311B92"))),    //фиолетовый светлый
                (Color.FromHex("8C9EFF"), (Color.FromHex("1A237E"))),    //синий темный
                (Color.FromHex("82B1FF"), (Color.FromHex("0D47A1"))),    //
                (Color.FromHex("80D8FF"), (Color.FromHex("01579B"))),    //
                (Color.FromHex("84FFFF"), (Color.FromHex("006064"))),    //
                (Color.FromHex("A7FFEB"), (Color.FromHex("004D40"))),    //
                (Color.FromHex("B9F6CA"), (Color.FromHex("1B5E20"))),    //
                (Color.FromHex("CCFF90"), (Color.FromHex("33691E"))),    //
                (Color.FromHex("F4FF81"), (Color.FromHex("827717"))),    //
                (Color.FromHex("FFFF8D"), (Color.FromHex("F57F17"))),    //
                (Color.FromHex("FFE57F"), (Color.FromHex("FF6F00"))),    //
                (Color.FromHex("FFD180"), (Color.FromHex("E65100"))),    //
                (Color.FromHex("FF9E80"), (Color.FromHex("BF360C"))),    //
                (Color.FromHex("8D6E63"), (Color.FromHex("3E2723"))),    //
                (Color.FromHex("BDBDBD"), (Color.FromHex("212121"))),    //
                (Color.FromHex("78909C"), (Color.FromHex("263238"))),    //   
                (Color.FromHex("212121"), (Color.FromHex("000000"))),    //               
            };
        }
        
        private async void ThemeColorSelection (object s, EventArgs e)
        {
            var stackOfColor = new StackLayout();
            foreach(var color in listColor)
            {
                var bt = new Button { BackgroundColor = color.back, TextColor = color.bar };
                stackOfColor.Children.Add(bt);
                bt.Clicked += (_s, _e) =>
                {
                    var clr = ((Button)_s).BackgroundColor;
                    Setting.BackColor = clr;
                    navPage.BackgroundColor = clr;

                    clr = ((Button)_s).TextColor;
                    Setting.BarColor = clr;
                    navPage.BarBackgroundColor = clr;

                    ((Button)s).BackgroundColor = clr;
                };
            }
            await Navigation.PushAsync(new ContentPage { Content = new ScrollView { Content = stackOfColor } });
        }

        private async void TextColorSelection(object s, EventArgs e)
        {
            var stackOfColor = new StackLayout();
            foreach (var color in listColor)
            {
                var bt = new Button { BackgroundColor = color.back };
                stackOfColor.Children.Add(bt);
                bt.Clicked += async (_s, _e) =>
                {
                    var clr = ((Button)_s).BackgroundColor;
                    Setting.TextColor = clr;
                    ((Button)s).BackgroundColor = clr;
                 //   lanBt.Style = Setting.ButtonStyle;
                    ColorChange();
                    await Navigation.PopAsync();
                };
            }
            await Navigation.PushAsync(new ContentPage { Content = new ScrollView { Content = stackOfColor } });
        }

        private async void DetailColorSelection(object s, EventArgs e)
        {
            var stackOfColor = new StackLayout();
            foreach (var color in listColor)
            {
                var bt = new Button { BackgroundColor = color.back };
                stackOfColor.Children.Add(bt);
                bt.Clicked += async (_s, _e) =>
                {
                    var clr = ((Button)_s).BackgroundColor;
                    Setting.DetailColor = clr;
                    ((Button)s).BackgroundColor = clr;
                    await Navigation.PopAsync();
                };
            }
            await Navigation.PushAsync(new ContentPage { Content = new ScrollView { Content = stackOfColor } });
        }

        private async void ButtonColorSelection(object s, EventArgs e)
        {
            var stackOfColor = new StackLayout();
            foreach (var color in listColor)
            {
                var bt = new Button { BackgroundColor = color.back };
                stackOfColor.Children.Add(bt);
                bt.Clicked += async (_s, _e) =>
                {
                    var clr = ((Button)_s).BackgroundColor;
                    Setting.ButtonColor = clr;
                    ((Button)s).BackgroundColor = clr;
                 //   lanBt.Style = Setting.ButtonStyle;
                    ColorChange();
                    await Navigation.PopAsync();
                };
            }
            await Navigation.PushAsync(new ContentPage { Content = new ScrollView { Content = stackOfColor } });
        }

        //private async void LanguageSelection(object s, EventArgs e)
        //{
        //    var str = await DisplayActionSheet(Language.Language_, Language.Cancel, null, "Русский", "English");
        //    //   LanguageChange(str);
        //    if (str == "Русский")
        //    {
        //        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ru");
        //        System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ru");
        //        Preferences.Set("Language", "ru");
        //    }
        //    else if (str == "English")
        //    {
        //        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("");
        //        System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("");
        //        Preferences.Set("Language", "");
        //    }
        ////    lanBt.Text = Setting.Language == "" ? "English" : "Русский";
        //}

        private void AnimationChange(object s, EventArgs e)
        {
            Setting.Animation = ((Switch)s).IsToggled;
        }
    }
}