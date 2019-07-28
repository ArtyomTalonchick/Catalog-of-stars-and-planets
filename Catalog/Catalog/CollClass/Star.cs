using System;
using System.ComponentModel;


namespace Catalog.CollClass
{
    [Serializable]
    public class Star : INotifyPropertyChanged
    {
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        //перечисление типо звезд
        public enum typeOfStar
        { Unknown, O, B, A, F, G, K, M }

        //поля
        private string _name;
        private double _weight;
        private double _radius;
        private double _luminosity;
        private typeOfStar _type;
        private Constellation _constellation;
        private MyCollection<Planet> _planets;       //
        public string Uri { get; set; }

        //свойства
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        public double Weight
        {
            get => _weight;
            set
            {
                if (value > 0)
                    _weight = value;
                else
                    _weight = 0;
                OnPropertyChanged("Weight");
            }
        }
        public double Radius
        {
            get => _radius;
            set
            {
                if (value > 0)
                    _radius = value;
                else
                    _radius = 0;
                OnPropertyChanged("Radius");
            }
        }
        public double Luminosity
        {
            get => _luminosity;
            set
            {
                if (value > 0)
                    _luminosity = value;
                else
                    _luminosity = 0;
                OnPropertyChanged("Luminosity");
            }
        }
        public typeOfStar Type
        {
            get => _type;
            set
            {
                _type = value;
                OnPropertyChanged("Type");
            }
        }
        public Constellation Constellation
        {
            get
            {
                OnPropertyChanged("Constellation");
                return _constellation;
            }
            set
            {
                if (_constellation != null)
                    _constellation.Stars.Remove(this);
                _constellation = value;
                if (_constellation != null)
                    (_constellation as Constellation).Stars.Add(this);
                OnPropertyChanged("Constellation");
            }
        }
        public MyCollection<Planet> Planets
        {
            get
            {
                OnPropertyChanged("Planets");
                return _planets;
            }
            private set
            {
                _planets = value;
                OnPropertyChanged("Planets");
            }
        }

        //конструкторы
        private Star()
        {
            Planets = new MyCollection<Planet>();
        }
        public Star(string name_) : this()
        {
            Name = name_;
            Type = typeOfStar.Unknown;
        }
        //деструктор
        ~Star()
        {
            Delete();
        }

        //методы
        public void Delete()
        {
            if (Constellation != null)
                Constellation.Stars.Remove(this);
            foreach(var planet in Planets)
            {
                planet.Star = null;
            }
            Data.AllStars.Remove(this);
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        public override string ToString()
        {
            return Name;
        }
        public void SortPlanetsByName()
        {
            Planets.OrderByString();
        }       
    }
}
