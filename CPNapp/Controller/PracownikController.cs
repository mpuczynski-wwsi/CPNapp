using CPNapp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPNapp.Controller
{
    class PracownikController
    {
        public Cpn Config { get; private set; }
        public PracownikController()
        {
            ReadConfig rc = new ReadConfig();

            rc.CreateXml("cpn.xml");
            Config = rc.ReadXml("cpn.xml");
        }
    }
}
