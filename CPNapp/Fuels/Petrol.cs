using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPNapp.Fuels
{

    public enum PetrolType
    {
        Benzyna95,
        Benzyna98
    }
    public class Petrol : Fuel
    {
        public PetrolType Type { get; set; }
    }
}
