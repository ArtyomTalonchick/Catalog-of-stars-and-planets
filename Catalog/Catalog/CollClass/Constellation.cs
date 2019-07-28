using System;
using System.ComponentModel;

namespace Catalog.CollClass
{
    [Serializable]
    public class Constellation : INotifyPropertyChanged
    {
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public string Image { get; set; }
        //положение на звездной карте

        //поля
        private string _name;
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
        public MyCollection<Star> Stars { get; private set; }
        public MyCollection<M_object> M_objects { get; private set; }


        //конструторы
        private Constellation()
        {
            Stars = new MyCollection<Star>();
            M_objects = new MyCollection<M_object>();
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
            foreach (var m_objects in M_objects)
            {
                m_objects.Constellation = null;
            }
            Data.AllConstellations.Remove(this);
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
