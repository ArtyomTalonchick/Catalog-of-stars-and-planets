using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.CollClass
{
    [Serializable]
    public class Planet
    {
        //поля  //автосвойства
        private string _name;
        private double _weight;
        private double _radius;
        private double _periodOfRevolutionAroundItsAxis { get; set; }
        private Star _star;
        private int _periodOfRevolutionAroundStar { get; set; }
        private int _radiusOfOrbit { get; set; }

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
        public Star Star
        {
            get
            {
                return _star;
            }
            set
            {
                if (_star != null) 
                {
                    _star.Planets.Remove(this);
                }
                if (value != null)
                {
                    _star = value;
                    (value as Star).Planets.Add(this);
                }
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
            Star.Planets.Remove(this);
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        public override string ToString()
        {
            return Name;
        }

    }
}
