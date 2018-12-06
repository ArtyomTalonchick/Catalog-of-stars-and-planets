using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using Xamarin.Forms;

using System.ComponentModel;

namespace Catalog.CollClass
{
    [Serializable]
    public class MyCollection<T> :  IEnumerable<T>   //, INotifyPropertyChanged
    {
        /*/
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
        /*/

        //поля
        private List<T> list { get; set; }
        //конструктор
        public MyCollection(params T[] _array)
        {
            list = new List<T>();
            foreach (var item in _array)
            {
                list.Add(item);
            }
        }

        //индексатор
        public T this[int i]
        {
            get
            {
                return list[i];
            }
            set
            {
                list[i] = value;
            }
        }

        //методы
        public void Add(T item) => list.Add(item);
        public void Clear() => list.Clear();
        public void CopyTo(T[] array, int arrayIndex) => list.CopyTo(array, arrayIndex);
        public bool Remove(T item) => list.Remove(item);
        public int Count() => list.Count();
        public void OrderByString()
        {
            var tempRes = this.OrderBy(T => T.ToString());
            List<T> result = new List<T>();
            foreach(var item in tempRes)
            {
                result.Add(item);
            }
            list = result;
        }
        public MyCollection<T> Search (string name)
        {
            var tempRes = this.Where(T => T.ToString().IndexOf(name) > -1);
            MyCollection<T> result = new MyCollection<T>();
            if (tempRes.Count() == 0)
                return result;
            foreach (var item in tempRes)
            {
                result.Add(item);
            }
            return result;
        }


        //привязка данных
/*/
        public static readonly BindableProperty collectionProperty = BindableProperty.Create("collection", typeof(T), typeof(ListView));
        public T collection
        {
            get => (T) GetValue(collectionProperty);
            set => SetValue(collectionProperty, value);
        }
  /*/      

        //реализация интерфейса IEnumerable<T>
        public IEnumerator<T> GetEnumerator() => (new CollectionEnum(this));
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        
        //класс - перечислитель
        private class CollectionEnum : IEnumerator<T>
        {
            //поля
            private readonly MyCollection<T> _this;
            private int position = -1;

            //конструктор
            public CollectionEnum(MyCollection<T> coll)
            {
                _this = coll;
            }

            //реализация интерфейса IEnumerator<T>
            public T Current => _this.list[position];
            object IEnumerator.Current => Current;

            void IDisposable.Dispose()  //??
            {
                ((IEnumerator)this).Reset();
            }
            bool IEnumerator.MoveNext()
            {
                position++;
                return (position < _this.list.Count());
            }
            void IEnumerator.Reset() => position = -1;

        }

    }   
}