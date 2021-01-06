using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CPNapp.Fuels
{
    [XmlInclude(typeof(Diesiel))]
    [XmlInclude(typeof(Petrol))]
    [XmlInclude(typeof(Gas))]
    public abstract class Fuel
    {
        public string Symbol { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
    }
}
