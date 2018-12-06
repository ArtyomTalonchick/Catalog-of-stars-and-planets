using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.CollClass
{
    [Serializable]
    public class Star
    {
        //перечисление типо звезд
        public enum typeOfStar
        { Unknown, BrownDwarf, WhiteDwarf, RedGiant, VariableStar, TypeWolf_Rayet, TTS, New, Supernova, Hypernova, LBV, ULX, Neutron, Unique}

        //поля
        private string _name;
        private double _weight;
        private double _radius;
        private double _luminosity;
        public typeOfStar type { get; set; }
        private Constellation _constellation;
        private MyCollection<Planet> _planets;       //

        //свойства
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        public double Weight
        {
            get
            {
                return _weight;
            }
            set
            {
                if (value > 0)
                    _weight = value;
                else
                    _weight = 0;
            }
        }
        public double Radius
        {
            get
            {
                return _radius;
            }
            set
            {
                if (value > 0)
                    _radius = value;
                else
                    _radius = 0;
            }
        }
        public double Luminosity
        {
            get
            {
                return _luminosity;
            }
            set
            {
                if (value > 0)
                    _luminosity = value;
                else
                    _luminosity = 0;
            }
        }
        public Constellation Constellation
        {
            get
            {
                return _constellation;
            }
            set
            {
                if (_constellation != null)
                    _constellation.Stars.Remove(this);
                if (value != null) 
                {
                    _constellation = value;
                    (value as Constellation).Stars.Add(this);
                }
            }
        }
        public MyCollection<Planet> Planets
        {
            get
            {
                return _planets;
            }
            private set
            {
                _planets = value;
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
            type = typeOfStar.Unknown;
        }
        //деструктор
        ~Star()
        {
            Delete();
        }

        //методы
        public void Delete()
        {
            Constellation.Stars.Remove(this);
            foreach(var planet in Planets)
            {
                planet.Star = null;
            }
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
