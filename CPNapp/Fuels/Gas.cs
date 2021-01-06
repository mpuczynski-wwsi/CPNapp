using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPNapp.Fuels
{
    public enum GasType
    {
        LNG,
        LPG,
        CNG,
    }
    public class Gas: Fuel
    {
        public GasType Type { get; set; }

    }
}
