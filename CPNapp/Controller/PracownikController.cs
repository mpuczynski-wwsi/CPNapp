using CPNapp.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPNapp.Controller
{
    class PracownikController : Controller
    {

        public void UpdateConfigFile(Cpn cpnSettings)
        {
            _rc.SaveXml(cpnSettings);
        } 
    }
}
