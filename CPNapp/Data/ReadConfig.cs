using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CPNapp.Data
{
    class ReadConfig
    {

        private int _maxVolume = 20000;
        public void CreateXml(string filename)
        {
            // Creates an instance of the XmlSerializer class;
            // specifies the type of object to serialize.
            XmlSerializer serializer = new XmlSerializer(typeof(Cpn));
            TextWriter writer = new StreamWriter(filename);


            List<Fuel> fuelList = new List<Fuel>()
            {
                new Fuel()
                {
                    Name = "B95",
                    Price = 445,
                    Volume = 20000,
                },
                new Fuel()
                {
                    Name = "B98",
                    Price = 475,
                    Volume = 20000,
                },
                new Fuel()
                {
                    Name = "ON",
                    Price = 515,
                    Volume = 20000,
                },
                new Fuel()
                {
                    Name = "LPG",
                    Price = 275,
                    Volume = 20000,
                }
            };

            List<Dispenser> dispensers = new List<Dispenser>()
            {
                new Dispenser()
                {
                    Number=1,
                    FuelList=fuelList,
                    MaxVolumePerType=_maxVolume
                },
                new Dispenser()
                {
                    Number=2,
                    FuelList=fuelList,
                    MaxVolumePerType=_maxVolume
                },
                new Dispenser()
                {
                    Number=3,
                    FuelList=fuelList,
                    MaxVolumePerType=_maxVolume
                }
            };

            Cpn cpn_settings = new Cpn()
            {
                Options = new Options(),
                Dispensers = dispensers
            };

            serializer.Serialize(writer, cpn_settings);
            writer.Close();
        }

        public Cpn ReadXml(string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Cpn));

            serializer.UnknownNode += new XmlNodeEventHandler(serializer_UnknownNode);
            serializer.UnknownAttribute += new XmlAttributeEventHandler(serializer_UnknownAttribute);

            FileStream fs = new FileStream(filename, FileMode.Open);
            Cpn cpn = (Cpn)serializer.Deserialize(fs);
            return cpn;
        }

        private void serializer_UnknownNode(object sender, XmlNodeEventArgs e)
        {
            Console.WriteLine("Unknown Node:" + e.Name + "\t" + e.Text);
        }

        private void serializer_UnknownAttribute(object sender, XmlAttributeEventArgs e)
        {
            System.Xml.XmlAttribute attr = e.Attr;
            Console.WriteLine("Unknown attribute " +
            attr.Name + "='" + attr.Value + "'");
        }
    }
}
