using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPNapp
{
    enum RodzajGazu
    {
        LNG,
        LPG,
        CNG,
    }
    class Gaz: Paliwo
    {
        public RodzajGazu Rodzaj { get; set; }

    }
}
