using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.CollClass
{
    [Serializable]
    public class Constellation
    {
        //поля
        private string _name;
        private object _images;
        private MyCollection<Star> _stars;
        //положение на звездной карте

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
        public MyCollection<Star> Stars
        {
            get
            {
                return _stars;
            }
            private set
            {
                _stars = value;
            }
        }

        //конструторы
        private Constellation()
        {
            Stars = new MyCollection<Star>();
        }
        public Constellation(string name_) : this()
        {
            Name = name_;
        }
        //деструкторы
        ~Constellation()
        {
            delete();
        }

        //методы
        public void delete()
        {
            foreach(var star in Stars)
            {
                star.Constellation = null;
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        public override string ToString()
        {
            return Name;
        }
        public void SortStarsByName()
        {
            Stars.OrderByString();
        }

    }
}
