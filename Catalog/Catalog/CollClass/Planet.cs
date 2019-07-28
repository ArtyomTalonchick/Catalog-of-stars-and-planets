using System;
using System.ComponentModel;

namespace Catalog.CollClass
{
    [Serializable]
    public class Planet : INotifyPropertyChanged
    {
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        //поля  //автосвойства
        private string _name;
        private double _weight;
        private double _radius;
        private double _periodOfRotationOnItsAxis;
        private Star _star;
        private double _periodOfRotationAboutStars { get; set; }
        private double _radiusOfOrbit { get; set; }
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
        public double PeriodOfRotationOnItsAxis
        {
            get => _periodOfRotationOnItsAxis;
            set
            {
                if (value > 0)
                    _periodOfRotationOnItsAxis = value;
                else
                    _periodOfRotationOnItsAxis = 0;
                OnPropertyChanged("PeriodOfRotationOnItsAxis");
            }
        }
        public Star Star
        {
            get
            {
                return _star;
            }
            set
            {
                if (_star != null) 
                    _star.Planets.Remove(this);
                _star = value;
                if (_star != null)
                    (_star as Star).Planets.Add(this);
            }
        }
        public double PeriodOfRotationAboutStar
        {
            get => _periodOfRotationAboutStars;
            set
            {
                if (value > 0)
                    _periodOfRotationAboutStars = value;
                else
                    _periodOfRotationAboutStars = 0;
                OnPropertyChanged("PeriodOfRotationAboutStar");
            }
        }
        public double RadiusOfOrbit
        {
            get => _radiusOfOrbit;
            set
            {
                if (value > 0)
                    _radiusOfOrbit = value;
                else
                    _radiusOfOrbit = 0;
                OnPropertyChanged("RadiusOfOrbit");
            }
        }

        //конструкторы 
        private Planet()
        {

        }
        public Planet(string name_) : this()
        {
            Name = name_;
        }
        //деструктор
        ~Planet()
        {
            Delete();
        }

        //методы
        public void Delete()
        {
            if (Star != null) 
                Star.Planets.Remove(this);
            Data.AllPlanets.Remove(this);
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        public override string ToString()
        {
            return Name;
        }

    }
}
