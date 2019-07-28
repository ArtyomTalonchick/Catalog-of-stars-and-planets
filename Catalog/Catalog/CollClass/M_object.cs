using System;
using Xamarin.Forms;

namespace Catalog.CollClass
{
    [Serializable]
    public class M_object
    {
        public enum typeOfObject
        { Unknown, BrownDwarf, WhiteDwarf, RedGiant, VariableStar, TypeWolf_Rayet, TTS, New, Supernova, Hypernova, LBV, ULX, Neutron, Unique }

        public int Number { get; set; }
        public string NGC { get; set; }
        public string EquatorialCoordinates { get; set; }
        public double Glitter { get; set; }
        public double Size { get; set; }
        public double NumberOfStars { get; set; }
        public double Distance { get; set; }
        public typeOfObject Type { get; set; }
        public string Name { get; set; }
        private Constellation _constellation;
        public string Image { get; set; }
        public string Uri { get; set; }

        public Constellation Constellation
        {
            get
            {
                return _constellation;
            }
            set
            {
                if (_constellation != null)
                {
                    _constellation.M_objects.Remove(this);
                    _constellation = null;
                }
                if (value != null)
                {
                    _constellation = value;
                    (value as Constellation).M_objects.Add(this);
                }
            }
        }

        public override string ToString() => Name;
        

    }
}
