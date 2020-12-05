using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPNapp
{

    enum RodzajBenzyny
    {
        Benzyna95,
        Benzyna98
    }
    class Benzyna : Paliwo
    {
        public RodzajBenzyny Rodzaj { get; set; }
    }
}
