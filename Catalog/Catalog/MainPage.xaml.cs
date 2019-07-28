using System;
using System.Linq;
using Xamarin.Forms;
using Catalog.CollClass;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Xamarin.Essentials;
using System.Threading.Tasks;

namespace Catalog
{
    public partial class MainPage : ContentPage
    {        
        private string fileConstellationsName;
        private string fileStarsName;
        private string filePlanetsName;
        private string fileM_objectsName;
        public NavigationPage navPage;


        public MainPage()
        {
            InitializeComponent();       
            
            Resources["buttonStyle"] = Setting.ButtonStyle;
            if (Preferences.Get("Language", "") == "")
            {
                LanButton.Text = "Рус";
            }
            else
            {
                LanButton.Text = "En";
            }
        }

        public void addingItems()
        {
            Data.AllConstellations = new MyCollection<Constellation>();
            Data.AllStars = new MyCollection<Star>();
            Data.AllPlanets = new MyCollection<Planet>();
            Data.AllM_Ojects = new MyCollection<M_object>();

            #region солнце
            //w=5,972e24kg  r=6378km    pA=24h  pS=365d     rO=150 000 000km
            var s1 = new Planet("Меркурий")
            {
                Uri = "https://ru.wikipedia.org/wiki/%D0%9C%D0%B5%D1%80%D0%BA%D1%83%D1%80%D0%B8%D0%B9",
                Weight = 0.06,
                Radius = 0.38,
                PeriodOfRotationOnItsAxis = 115,
                PeriodOfRotationAboutStar = 0.23,
                RadiusOfOrbit = 0.36
            };
            var s2 = new Planet("Венера")
            {
                Uri = "https://ru.wikipedia.org/wiki/%D0%92%D0%B5%D0%BD%D0%B5%D1%80%D0%B0",
                Weight = 0.81,
                Radius = 0.948,
                PeriodOfRotationOnItsAxis = 0.61,
                PeriodOfRotationAboutStar = 583.9,
                RadiusOfOrbit = 0.71
            };
            var s3 = new Planet("Земля")
            {
                Uri = "https://ru.wikipedia.org/wiki/%D0%97%D0%B5%D0%BC%D0%BB%D1%8F",
                Weight = 1,
                Radius = 1,
                PeriodOfRotationOnItsAxis = 1,
                PeriodOfRotationAboutStar = 1,
                RadiusOfOrbit = 1
            };
            var s4 = new Planet("Марс")
            {
                Uri = "https://ru.wikipedia.org/wiki/%D0%9C%D0%B0%D1%80%D1%81",
                Weight = 0.107,
                Radius = 0.53,
                PeriodOfRotationOnItsAxis = 779.94,
                PeriodOfRotationAboutStar = 1.88,
                RadiusOfOrbit = 1.37
            };
            var s5 = new Planet("Юпитер")
            {
                Uri = "https://ru.wikipedia.org/wiki/%D0%AE%D0%BF%D0%B8%D1%82%D0%B5%D1%80",
                Weight = 317.9,
                Radius = 10.66,
                PeriodOfRotationOnItsAxis = 398,
                PeriodOfRotationAboutStar = 11.8,
                RadiusOfOrbit = 5.13
            };
            var s6 = new Planet("Сатурн")
            {
                Uri = "https://ru.wikipedia.org/wiki/%D0%A1%D0%B0%D1%82%D1%83%D1%80%D0%BD",
                Weight = 94.6,
                Radius = 9.13,
                PeriodOfRotationOnItsAxis = 378,
                PeriodOfRotationAboutStar = 29.46,
                RadiusOfOrbit = 9.6
            };
            var s7 = new Planet("Уран")
            {
                Uri = "https://ru.wikipedia.org/wiki/%D0%A3%D1%80%D0%B0%D0%BD_(%D0%BF%D0%BB%D0%B0%D0%BD%D0%B5%D1%82%D0%B0)",
                Weight = 14.46,
                Radius = 3.97,
                PeriodOfRotationOnItsAxis = 369,
                PeriodOfRotationAboutStar = 84.01,
                RadiusOfOrbit = 19.1
            };
            var s8 = new Planet("Нептун")
            {
                Uri = "https://ru.wikipedia.org/wiki/%D0%9D%D0%B5%D0%BF%D1%82%D1%83%D0%BD",
                Weight = 17.15,
                Radius = 3.86,
                PeriodOfRotationOnItsAxis = 367,
                PeriodOfRotationAboutStar = 164.79,
                RadiusOfOrbit = 30.02
            };
            var sol = new Star("Солнце") //w=1,9885e30кг  r=6,9551e8м    l=1,9885e30кг   
            {
                Uri = "https://ru.wikipedia.org/wiki/%D0%A1%D0%BE%D0%BB%D0%BD%D1%86%D0%B5",
                Weight = 1,
                Radius = 1,
                Luminosity = 1,
                Type = Star.typeOfStar.G             
            };
            Data.AllStars.Add(sol);

            Data.AllPlanets.Add(s1);
            s1.Star = sol;
            Data.AllPlanets.Add(s2);
            s2.Star = sol;
            Data.AllPlanets.Add(s3);
            s3.Star = sol;
            Data.AllPlanets.Add(s4);
            s4.Star = sol;
            Data.AllPlanets.Add(s5);
            s5.Star = sol;
            Data.AllPlanets.Add(s6);
            s6.Star = sol;
            Data.AllPlanets.Add(s7);
            s7.Star = sol;
            Data.AllPlanets.Add(s8);
            s8.Star = sol;

            #endregion

            #region скорпион
            var sk1 = new Star("Антарес")
            {
                Uri = "https://ru.wikipedia.org/wiki/%D0%90%D0%BD%D1%82%D0%B0%D1%80%D0%B5%D1%81",
                Weight = 12.4,
                Radius = 400,
                Luminosity = 57500,
                Type = Star.typeOfStar.M
            };
            Data.AllStars.Add(sk1);
            Constellation Scorpio = new Constellation("Скорпион")
            {
                Uri = "https://ru.wikipedia.org/wiki/%D0%A1%D0%BA%D0%BE%D1%80%D0%BF%D0%B8%D0%BE%D0%BD_(%D1%81%D0%BE%D0%B7%D0%B2%D0%B5%D0%B7%D0%B4%D0%B8%D0%B5)",
                Image = "https://picua.org/images/2018/12/18/e077208ae41b3723b1195ce06beb06c4.md.png",                
            };
            sk1.Constellation = Scorpio;
            Data.AllConstellations.Add(Scorpio);
            addM_objects("4	NGC 6121	16h 23.6m -26° 32'	5,4	26		2200	Шаровое скопление	Кошачий Глаз", "https://picua.org/images/2018/12/18/09d861a101d4ee2091cd75852b0680b4.th.jpg", "https://ru.wikipedia.org/wiki/M_4_(%D1%88%D0%B0%D1%80%D0%BE%D0%B2%D0%BE%D0%B5_%D1%81%D0%BA%D0%BE%D0%BF%D0%BB%D0%B5%D0%BD%D0%B8%D0%B5)").Constellation = Scorpio;
            addM_objects("6	NGC 6405	17h 40.1m -32° 13'	4,2	25	80	491	Рассеянное скопление	Бабочка", "https://picua.org/images/2018/12/18/e2c759b895a6fc2e4e9457482de6f800.md.jpg", "https://ru.wikipedia.org/wiki/%D0%91%D0%B0%D0%B1%D0%BE%D1%87%D0%BA%D0%B0_(%D0%B7%D0%B2%D1%91%D0%B7%D0%B4%D0%BD%D0%BE%D0%B5_%D1%81%D0%BA%D0%BE%D0%BF%D0%BB%D0%B5%D0%BD%D0%B8%D0%B5)").Constellation = Scorpio;
            addM_objects("7	NGC 6475	17h 53.9m -34° 49'	3,3	80	100	245	Рассеянное скопление	Скопление Птолемея", "https://picua.org/images/2018/12/18/fc868b3dcc83ee6cecf247c0074ad68f.md.jpg", "https://ru.wikipedia.org/wiki/%D0%A1%D0%BA%D0%BE%D0%BF%D0%BB%D0%B5%D0%BD%D0%B8%D0%B5_%D0%9F%D1%82%D0%BE%D0%BB%D0%B5%D0%BC%D0%B5%D1%8F").Constellation = Scorpio;
            addM_objects("80	NGC 6093	16h 17.0m -22° 59'	7,3	9		9880	Шаровое скопление	", "https://picua.org/images/2018/12/18/976c30ff9c9460b0447d8b7f9022ae70.md.jpg", "https://ru.wikipedia.org/wiki/M_80_(%D1%88%D0%B0%D1%80%D0%BE%D0%B2%D0%BE%D0%B5_%D1%81%D0%BA%D0%BE%D0%BF%D0%BB%D0%B5%D0%BD%D0%B8%D0%B5)").Constellation = Scorpio;
            #endregion

            #region телец
            Constellation Telec = new Constellation("Tелец")
            {
                Uri = "https://ru.wikipedia.org/wiki/%D0%A2%D0%B5%D0%BB%D0%B5%D1%86_(%D1%81%D0%BE%D0%B7%D0%B2%D0%B5%D0%B7%D0%B4%D0%B8%D0%B5)",
                Image = "https://picua.org/images/2018/12/18/893b32ecbdebf1c2cca219828d78b62c.md.png"
            };
            Data.AllConstellations.Add(Telec);
            addM_objects("1	NGC 1952	05h 34.5m +22° 01'	8,4	6		2000	Сверхновая	Крабовидная туманность", "https://picua.org/images/2018/12/18/bbd2d39ed4f448d4b2a61313f217d7bb.md.jpg", "https://ru.wikipedia.org/wiki/%D0%9A%D1%80%D0%B0%D0%B1%D0%BE%D0%B2%D0%B8%D0%B4%D0%BD%D0%B0%D1%8F_%D1%82%D1%83%D0%BC%D0%B0%D0%BD%D0%BD%D0%BE%D1%81%D1%82%D1%8C").Constellation = Telec;
            addM_objects("45	    	03h 47.0m +24° 07'	1,2	110	160	135	Рассеянное скопление	Плеяды	Телец", "https://picua.org/images/2018/12/18/071fcb2294fee4e1d47f279a0956a634.md.jpg" , "https://ru.wikipedia.org/wiki/%D0%9F%D0%BB%D0%B5%D1%8F%D0%B4%D1%8B_(%D0%B7%D0%B2%D1%91%D0%B7%D0%B4%D0%BD%D0%BE%D0%B5_%D1%81%D0%BA%D0%BE%D0%BF%D0%BB%D0%B5%D0%BD%D0%B8%D0%B5)").Constellation = Telec;
            #endregion

            #region водолей
            var v1 = new Planet("Глизе 876 d")
            {
                Uri = "https://ru.wikipedia.org/wiki/%D0%93%D0%BB%D0%B8%D0%B7%D0%B5_876_d",
                Weight = 6.83,
                Radius = 3.83,
                PeriodOfRotationAboutStar = 0.005,
                RadiusOfOrbit = 0.02
            };
            var v2 = new Planet("Глизе 876 c")
            {
                Uri = "https://ru.wikipedia.org/wiki/%D0%93%D0%BB%D0%B8%D0%B7%D0%B5_876_c",
                Weight = 225,
                PeriodOfRotationOnItsAxis = 0.08,
                RadiusOfOrbit = 0.13
            };
            var v3 = new Planet("Глизе 876 b")
            {
                Uri = "https://ru.wikipedia.org/wiki/%D0%93%D0%BB%D0%B8%D0%B7%D0%B5_876_b",
                Weight = 700,
                PeriodOfRotationOnItsAxis = 0.16,
                RadiusOfOrbit = 0.208
            };
            var v4 = new Planet("Глизе 876 e")
            {
                Uri = "https://ru.wikipedia.org/wiki/%D0%93%D0%BB%D0%B8%D0%B7%D0%B5_876_e",
                Weight = 15,
                PeriodOfRotationOnItsAxis = 0.33,
                RadiusOfOrbit = 0.33
            };
            var v11 = new Star("Глизе 876")
            {
                Uri = "https://ru.wikipedia.org/wiki/%D0%93%D0%BB%D0%B8%D0%B7%D0%B5_876",
                Weight = 0.32,
                Radius = 0.36,
                Luminosity = 0.00124,
                Type = Star.typeOfStar.Unknown
            };
            Data.AllStars.Add(v11);

            Data.AllPlanets.Add(v1);
            v1.Star = v11;
            Data.AllPlanets.Add(v2);
            v2.Star = v11;
            Data.AllPlanets.Add(v3);
            v3.Star = v11;
            Data.AllPlanets.Add(v4);
            v4.Star = v11;

            Constellation Vodo = new Constellation("Водолей")
            {
                Uri = "https://ru.wikipedia.org/wiki/%D0%92%D0%BE%D0%B4%D0%BE%D0%BB%D0%B5%D0%B9_(%D1%81%D0%BE%D0%B7%D0%B2%D0%B5%D0%B7%D0%B4%D0%B8%D0%B5)",
                Image = "https://picua.org/images/2018/12/18/2cf0988675d7cda942a5ab623fe0eb3a.md.png",                
            };
            v11.Constellation = Vodo;
            Data.AllConstellations.Add(Vodo);
            addM_objects("2	NGC 7089	21h 33.5m -00° 49'	6,6	13		11500	Шаровое скопление		Водолей", "https://picua.org/images/2018/12/18/ab860c29db4e824ce96d78a2fac55b7e.md.jpg", "https://ru.wikipedia.org/wiki/M_2_(%D1%88%D0%B0%D1%80%D0%BE%D0%B2%D0%BE%D0%B5_%D1%81%D0%BA%D0%BE%D0%BF%D0%BB%D0%B5%D0%BD%D0%B8%D0%B5)").Constellation = Vodo;
            addM_objects("72	NGC 6981	20h 53.5m -12° 32'	9,2	6		17290	Шаровое скопление		Водолей", "https://picua.org/images/2018/12/18/ecc00bf7df3aa69b89263defca68b8f6.md.jpg", "https://ru.wikipedia.org/wiki/M_72_(%D1%88%D0%B0%D1%80%D0%BE%D0%B2%D0%BE%D0%B5_%D1%81%D0%BA%D0%BE%D0%BF%D0%BB%D0%B5%D0%BD%D0%B8%D0%B5)").Constellation = Vodo;
            //    addM_objects("73	NGC 6994	20h 58.9m -12° 38'	8.9	3			Астеризм		Водолей", "https://picua.org/images/2018/12/18/81d32c13b36010e24ce29f668ed10c90.md.jpg").Constellation = Vodo;
            #endregion

            #region большая медведица
            Constellation bigDipper = new Constellation("Большая Медведицы")
            {
                Uri = "https://ru.wikipedia.org/wiki/%D0%91%D0%BE%D0%BB%D1%8C%D1%88%D0%B0%D1%8F_%D0%9C%D0%B5%D0%B4%D0%B2%D0%B5%D0%B4%D0%B8%D1%86%D0%B0",
                Image = "https://picua.org/images/2018/12/18/015d35da78af7fd08a05654c51bae7f6.png"
            };
            Data.AllConstellations.Add(bigDipper);
            //   addM_objects("40		12h 22.2m +58° 05'	9	1			Звезда	", "").Constellation = bigDipper;
            addM_objects("81	NGC 3031	09h 55.5m +69° 04'	7	21		3040000	Галактика	Галактика Боде", "https://picua.org/images/2018/12/18/74eba2285abe037f43f3240765d42c38.md.jpg", "https://ru.wikipedia.org/wiki/%D0%93%D0%B0%D0%BB%D0%B0%D0%BA%D1%82%D0%B8%D0%BA%D0%B0_%D0%91%D0%BE%D0%B4%D0%B5").Constellation = bigDipper;
            addM_objects("82	NGC 3034	09h 55.9m +69° 41'	8,6	9		3800000	Галактика	Сигара", "https://picua.org/images/2018/12/18/e82a69059f1d56689079491412676e6f.md.gif", "https://ru.wikipedia.org/wiki/%D0%93%D0%B0%D0%BB%D0%B0%D0%BA%D1%82%D0%B8%D0%BA%D0%B0_%D0%A1%D0%B8%D0%B3%D0%B0%D1%80%D0%B0").Constellation = bigDipper;
            addM_objects("97	NGC 3587	11h 14.8m +55° 01'	9,9	3		790	Планетарная туманность	Сова", "https://picua.org/images/2018/12/18/7068e66d2f8b2efc1a2cc47d114f6959.th.jpg", "https://ru.wikipedia.org/wiki/%D0%A2%D1%83%D0%BC%D0%B0%D0%BD%D0%BD%D0%BE%D1%81%D1%82%D1%8C_%D0%A1%D0%BE%D0%B2%D0%B0").Constellation = bigDipper;
            addM_objects("101	NGC 5457	14h 03.2m +54° 21'	7,5	30		8250000	Галактика	Вертушка    ", "https://picua.org/images/2018/12/18/70661660bb0d1920bf35ab83a4f5a4d5.md.jpg", "https://ru.wikipedia.org/wiki/%D0%93%D0%B0%D0%BB%D0%B0%D0%BA%D1%82%D0%B8%D0%BA%D0%B0_%D0%92%D0%B5%D1%80%D1%82%D1%83%D1%88%D0%BA%D0%B0").Constellation = bigDipper;
          //  addM_objects("108	NGC 3556	11h 11.5m +55° 40'	9,9	8		4300000	Галактика   ", "https://picua.org/images/2018/12/18/0b2c6a6524c29ff756dace8c89e9db5c.md.jpg").Constellation = bigDipper;
            addM_objects("109	NGC 3992	11h 57.6m +53° 23'	9,8	7		16800000	Галактика	", "https://picua.org/images/2018/12/18/a63a64f5750a68e4b89bb52fd9c1b5bc.md.jpg", "https://ru.wikipedia.org/wiki/M_109_(%D0%B3%D0%B0%D0%BB%D0%B0%D0%BA%D1%82%D0%B8%D0%BA%D0%B0)").Constellation = bigDipper;
            #endregion

            //addM_objects("", "").Constellation = bigDipper;

            writeNameFile();
            serialize();
        }
        private M_object addM_objects(string data, string image, string uri)
        {           
            var newObj = new M_object();
            int i = 0;
            int j = 0;
            while (data[i] != '\t')
            {
                i++;
            }
            newObj.Number = Convert.ToInt32(data.Substring(j, i-j));
            j = ++i;
            while (data[i] != '\t')
            {
                i++;
            }
            newObj.NGC = data.Substring(j, i-j);
            j = ++i;
            while (data[i] != '\t')
            {
                i++;
            }
            newObj.EquatorialCoordinates = data.Substring(j, i-j);
            j = ++i;
            while (data[i] != '\t')
            {
                i++;
            }
            newObj.Glitter = Convert.ToDouble(data.Substring(j, i-j));
            j = ++i;
            while (data[i] != '\t')
            {
                i++;
            }
            newObj.Size = Convert.ToDouble(data.Substring(j, i-j));
            j = ++i;
            while (data[i] != '\t')
            {
                i++;
            }
            if (i != j)
                newObj.NumberOfStars = Convert.ToDouble(data.Substring(j, i-j));
            j = ++i;
            while (data[i] != '\t')
            {
                i++;
            }
            newObj.Distance = Convert.ToDouble(data.Substring(j, i-j));
            j = ++i;

            while (data[i] != '\t')
            {
                i++;
            }
           // newObj.Name = line.Substring(j, i - j);
            j = ++i;


            int count = data.Count();
            while (i < count && data[i] != '\t')
            {
                i++;
            }
            newObj.Name = data.Substring(j, i - j);
            j = ++i;
            
            newObj.Image = image;
            newObj.Uri = uri;

            Data.AllM_Ojects.Add(newObj);
            
            return newObj;
        }

        public void writeNameFile()
        {
            fileConstellationsName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Constellations.dat");
            fileStarsName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Stars.dat");
            filePlanetsName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Planets.dat");
            fileM_objectsName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "M_objects.dat");
        }        
        public void serialize()
        {
            var formatter = new BinaryFormatter();
            using (Stream s = File.Create(fileConstellationsName))
                formatter.Serialize(s, Data.AllConstellations);
            using (Stream s = File.Create(fileStarsName))
                formatter.Serialize(s, Data.AllStars);
            using (Stream s = File.Create(filePlanetsName))
                formatter.Serialize(s, Data.AllPlanets);
            using (Stream s = File.Create(fileM_objectsName))
                formatter.Serialize(s, Data.AllM_Ojects);
        }
        public void deserialize()
        {
            var formatter = new BinaryFormatter();
            using (Stream s = File.OpenRead(fileConstellationsName))
                Data.AllConstellations = (MyCollection<Constellation>)formatter.Deserialize(s);
            using (Stream s = File.OpenRead(fileStarsName))
                Data.AllStars = (MyCollection<Star>)formatter.Deserialize(s);
            using (Stream s = File.OpenRead(filePlanetsName))
                Data.AllPlanets = (MyCollection<Planet>)formatter.Deserialize(s);
            using (Stream s = File.OpenRead(fileM_objectsName))
                Data.AllM_Ojects = (MyCollection<M_object>)formatter.Deserialize(s);
        }

        private async void Settings_Clicked(object s, EventArgs e)
        {
            var colorChange = new Action(() =>
            {
                ConstellationButton.BackgroundColor = Setting.ButtonColor;
                StarButton.BackgroundColor = Setting.ButtonColor;
                PlanetButton.BackgroundColor = Setting.ButtonColor;
                M_ObjectButton.BackgroundColor = Setting.ButtonColor;

                ConstellationButton.TextColor = Setting.TextColor;
                StarButton.TextColor = Setting.TextColor;
                PlanetButton.TextColor = Setting.TextColor;
                M_ObjectButton.TextColor = Setting.TextColor;
            });
            var languageChange = new Action<string>((string str) =>
            {
                if (str == "Русский")
                {
                    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ru");
                    System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ru");
                    Preferences.Set("Language", "ru");
                }
                else if (str == "English")
                {
                    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("");
                    System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("");
                    Preferences.Set("Language", "");
                }
            });
            await Navigation.PushAsync(new SettingPage(navPage, colorChange, languageChange));
        }
        private void Lan_Clicked(object s, EventArgs e)
        {
            if (Preferences.Get("Language", "") == "")
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ru");
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ru");
                Preferences.Set("Language", "ru");
                LanButton.Text = "En";
            }
            else
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("");
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("");
                Preferences.Set("Language", "");
                LanButton.Text = "Рус";
            }

            ConstellationButton.Text = Language.Constellation;
            StarButton.Text = Language.Star;
            PlanetButton.Text = Language.Planet;
            M_ObjectButton.Text = Language.M_Object;
            Title = Language.Menu;
        }

        private async void ConstellationButton_Clicked(object sender, EventArgs e)
        {
            await Animation(sender);
            GC.Collect();
            await Navigation.PushAsync(new Pages.CollectionPage(Data.AllConstellations, Data.AllStars, Data.AllPlanets, Data.AllM_Ojects, Pages.CollectionPage.TypeItemsOfColl.Constellation));
            GC.Collect();
        }
        private async void StarButton_Clicked(object sender, EventArgs e)
        {
            await Animation(sender);
            GC.Collect();
            await Navigation.PushAsync(new Pages.CollectionPage(Data.AllConstellations, Data.AllStars, Data.AllPlanets, Data.AllM_Ojects, Pages.CollectionPage.TypeItemsOfColl.Star));
            GC.Collect();
        }
        private async void PlanetButton_Clicked(object sender, EventArgs e)
        {
            await Animation(sender);
            GC.Collect();
            await Navigation.PushAsync(new Pages.CollectionPage(Data.AllConstellations, Data.AllStars, Data.AllPlanets, Data.AllM_Ojects, Pages.CollectionPage.TypeItemsOfColl.Planet));
            GC.Collect();
        }
        private async void M_ObjectButton_Clicked(object sender, EventArgs e)
        {
            await Animation(sender);
            GC.Collect();
            await Navigation.PushAsync(new Pages.CollectionPage(Data.AllConstellations, Data.AllStars, Data.AllPlanets, Data.AllM_Ojects, Pages.CollectionPage.TypeItemsOfColl.M_Object));
            GC.Collect();
        }

        private async void serialize_Clicked(object s, EventArgs e)
        {
            //  GC.Collect();
            //serialize();            
            Device.OpenUri(new Uri("https://ru.wikipedia.org/wiki/%D0%93%D0%BB%D0%B8%D0%B7%D0%B5_581"));
        }

        private async Task Animation (object sender)
        {
            if (!Setting.Animation)
                return;
            View view = (View)sender;
            await view.TranslateTo(-view.Width * 0.7, 0, 500);
            var task = new Task(() => view.TranslateTo(0, 0, 1).Wait(100));
            task.Start();
        }
    }
}
