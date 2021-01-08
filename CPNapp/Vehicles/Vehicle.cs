using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CPNapp.Vehicles
{
    [XmlInclude(typeof(Car))]
    [XmlInclude(typeof(Scooter))]
    [XmlInclude(typeof(Truck))]
    public class Vehicle
    {
        public int TankCapacity { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int VehicleProductionDate { get; set; }
    }
}
