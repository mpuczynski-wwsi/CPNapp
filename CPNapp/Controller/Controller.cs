using CPNapp.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPNapp.Controller
{
    class Controller
    {
        public Cpn Config { get; private set; }

        public User LoggedUser { get; set; }

        protected ReadConfig _rc;
        public Controller()
        {
            _rc = new ReadConfig();
            string settingsFile = "cpn_data.xml";
            _rc.SetFilename(settingsFile);
            if (!File.Exists(settingsFile))
            {
                _rc.CreateXml();
            }
            Config = _rc.ReadXml();
        }
    }
}
