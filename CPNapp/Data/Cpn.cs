using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace CPNapp.Data
{
    [XmlRoot("Cpn", IsNullable =false)]
    public class Cpn
    {
        public Options Options;
        public List<Dispenser> Dispensers = new List<Dispenser>();

    }

    public class Dispenser
    {
        public int Number;
        public List<Fuel> FuelList = new List<Fuel>();
        public int MaxVolumePerType;
    }

    public class Fuel
    {
        public string Name;
        public int Price;
        public int Volume;

    }

    public class Options
    {
    }
}
