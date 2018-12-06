using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Catalog.CollClass;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


using System.Xml.Serialization;


namespace Catalog
{
    public partial class MainPage : ContentPage
    {

        private string fileConstellationsName;
        private string fileStarsName;
        private string filePlanetsName;

        private MyCollection<Constellation> AllConstellations;
        private MyCollection<Star> AllStars;
        private MyCollection<Planet> AllPlanets;

        public MainPage()
        {
            InitializeComponent();

            //
            AllConstellations = new MyCollection<Constellation>
            {
                new Constellation("Созвездие 1"),
                new Constellation("Созвездие 2"),
                new Constellation("Созвездие 3"),
                new Constellation("Созвездие 4"),
                new Constellation("Созвездие 5"),
                new Constellation("Созвездие 6"),
                new Constellation("Созвездие 7"),
                new Constellation("Созвездие 8"),
            };
            AllStars = new MyCollection<Star>
            {
                new Star("звезда 1"){ Weight=321312, Radius=321312, Luminosity=3123},
                new Star("звезда 2"){ Weight=321312, Radius=321312, Luminosity=3123},
                new Star("звезда 3"){ Weight=321312, Radius=321312, Luminosity=3123},
                new Star("звезда 4"){ Weight=321312, Radius=321312, Luminosity=3123},
                new Star("звезда 5"){ Weight=321312, Radius=321312, Luminosity=3123},
                new Star("звезда 6"){ Weight=321312, Radius=321312, Luminosity=3123},
            };            
            AllPlanets = new MyCollection<Planet>
            {
                new Planet("Меркурий"){ Weight=123, Radius=3213 },
                new Planet("Венера"){ Weight=123, Radius=3213 },
                new Planet("Земля"){ Weight=123, Radius=3213 },
                new Planet("Марс"){ Weight=123, Radius=3213 },
                new Planet("Юпитер"){ Weight=123, Radius=3213 },
                new Planet("Сатурн"){ Weight=123, Radius=3213 },
                new Planet("Уран"){ Weight=123, Radius=3213 },
                new Planet("Нептун"){ Weight=123, Radius=3213 },
            };
            //
            writeNameFile();
            //deserialize();
            serialize();
        }

        public void writeNameFile()
        {
            fileConstellationsName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Constellations.dat");
            fileStarsName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Stars.dat");
            filePlanetsName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Planets.dat");
        }
        /*/
        public void serialize()
        {
            //созвездия
            var formatter = new XmlSerializer(typeof(MyCollection<Constellation>));
            var fs = new FileStream(fileConstellationsName, FileMode.OpenOrCreate);
            formatter.Serialize(fs, AllConstellations);
            //звезды
            formatter = new XmlSerializer(typeof(MyCollection<Star>));
            fs = new FileStream(fileStarsName, FileMode.OpenOrCreate);
            formatter.Serialize(fs, AllStars);
            //планеты
            formatter = new XmlSerializer(typeof(MyCollection<Planet>));
            fs = new FileStream(filePlanetsName, FileMode.OpenOrCreate);
            formatter.Serialize(fs, AllPlanets);
        }

        public void deserialize()
        {
            //созвездия
            var formatter = new XmlSerializer(typeof(MyCollection<Constellation>));
            var fs = new FileStream(fileConstellationsName, FileMode.OpenOrCreate);      
            AllConstellations = (MyCollection<Constellation>)formatter.Deserialize(fs);
            //звезды
            formatter = new XmlSerializer(typeof(MyCollection<Star>));
            fs = new FileStream(fileStarsName, FileMode.OpenOrCreate);
            AllStars = (MyCollection<Star>)formatter.Deserialize(fs);
            //планету
            formatter = new XmlSerializer(typeof(MyCollection<Planet>));
            fs = new FileStream(filePlanetsName, FileMode.OpenOrCreate);
            AllPlanets = (MyCollection<Planet>)formatter.Deserialize(fs);
        }
        /*/

        public void serialize()
        {
            var formatter = new BinaryFormatter();
            using (Stream s = File.Create(fileConstellationsName))
                formatter.Serialize(s, AllConstellations);
            using (Stream s = File.Create(fileStarsName))
                formatter.Serialize(s, AllStars);
            using (Stream s = File.Create(filePlanetsName))
                formatter.Serialize(s, AllPlanets);
        }
        public void deserialize()
        {
            var formatter = new BinaryFormatter();
            using (Stream s = File.OpenRead(fileConstellationsName))
                AllConstellations = (MyCollection<Constellation>)formatter.Deserialize(s);
            using (Stream s = File.OpenRead(fileStarsName))
                AllStars = (MyCollection<Star>)formatter.Deserialize(s);
            using (Stream s = File.OpenRead(filePlanetsName))
                AllPlanets = (MyCollection<Planet>)formatter.Deserialize(s);
        }

        private async void ConstellationButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Pages.CollectionPage(AllConstellations, AllStars, AllPlanets, Pages.CollectionPage.TypeItemsOfColl.Constellation));
        }
        private async void StarButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Pages.CollectionPage(AllConstellations, AllStars, AllPlanets, Pages.CollectionPage.TypeItemsOfColl.Star));
        }
        private async void PlanetButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Pages.CollectionPage(AllConstellations, AllStars, AllPlanets, Pages.CollectionPage.TypeItemsOfColl.Planet));
        }
        
    }
}
