using CPNapp.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPNapp.Controller
{
    class PracownikController
    {
        public Cpn Config { get; private set; }
        private ReadConfig _rc;
        public PracownikController()
        {
            _rc = new ReadConfig();
            string settingsFile = "cpn_data.xml";
            _rc.SetFilename(settingsFile);
            if(!File.Exists(settingsFile))
            {
                _rc.CreateXml();
            }
            Config = _rc.ReadXml();
        }

        public void UpdateConfigFile(Cpn cpnSettings)
        {
            _rc.SaveXml(cpnSettings);
        } 
    }
}
