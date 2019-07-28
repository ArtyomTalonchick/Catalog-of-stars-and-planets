using Catalog.CollClass;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Catalog
{
    public static class Data
    {
        public static MyCollection<Constellation> AllConstellations { get; set; }
        public static MyCollection<Star> AllStars { get; set; }
        public static MyCollection<Planet> AllPlanets { get; set; }
        public static MyCollection<M_object> AllM_Ojects { get; set; }
    }

    public static class Setting
    {
        public static Style ButtonStyle { get; set; }

        private static Color _backColor;
        private static Color _textColor;
        private static Color _detailColor;
        private static Color _buttonColor;
        private static Color _barColor;
        public static Color BackColor
        {
            get => _backColor;
            set => SetColor("BackColor", out _backColor, value);
        }
        public static Color TextColor
        {
            get => _textColor;
            set
            {
                SetColor("TextColor", out _textColor, value);
                ButtonStyle.Setters.Add(new Setter { Property = Button.TextColorProperty, Value = TextColor });
            }
        }
        public static Color DetailColor
        {
            get => _detailColor;
            set => SetColor("DetailColor", out _detailColor, value);
        }
        public static Color ButtonColor
        {
            get => _buttonColor;
            set
            {
                SetColor("ButtonColor", out _buttonColor, value);
                ButtonStyle.Setters.Add(new Setter { Property = Button.BackgroundColorProperty, Value = ButtonColor });
            }
        }
        public static Color BarColor
        {
            get => _barColor;
            set => SetColor("BarColor", out _barColor, value);
        }
        private static void SetColor(string name, out Color clr, Color _clr)
        {
            clr = _clr;
            Preferences.Set(name + "R", _clr.R);
            Preferences.Set(name + "G", _clr.G);
            Preferences.Set(name + "B", _clr.B);
        }
        private static void RestoreColor(string name, out Color clr)
        {
           clr = new Color(Preferences.Get(name + "R", .0), Preferences.Get(name + "G", .0), Preferences.Get(name + "B", .0));
        }

        public static string Language
        {
            get => Preferences.Get("Language", "");
            set
            {
                if (value != "" && value != "ru")
                    return;
                Preferences.Set("Language", value);
            }
        }

        private static bool _animation;
        public static bool Animation
        {
            get => _animation;
            set
            {
                _animation = value;
                Preferences.Set("Animation", value);
            }
        }

        static Setting()
        {
            RestoreColor("BackColor", out _backColor);
            RestoreColor("TextColor", out _textColor);
            RestoreColor("DetailColor", out _detailColor);
            RestoreColor("ButtonColor", out _buttonColor);
            RestoreColor("BarColor", out _barColor);
            _animation = Preferences.Get("Animation", false);

            ButtonStyle = new Style(typeof(Button));
            ButtonStyle.Setters.Add(new Setter { Property = Button.BackgroundColorProperty, Value = ButtonColor });
            ButtonStyle.Setters.Add(new Setter { Property = Button.TextColorProperty, Value = TextColor });
        }



    }

}
