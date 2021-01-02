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
        private string _filename = "cpn_data.xml";


        public void CreateXml()
        {
            // Creates an instance of the XmlSerializer class;
            // specifies the type of object to serialize.



            List<Fuel> fuelList1 = new List<Fuel>()
            {
                new Fuel()
                {
                    Name = "B95",
                    Price = 445,
                    Volume = 6068,
                },
                new Fuel()
                {
                    Name = "B98",
                    Price = 475,
                    Volume = 12882,
                },
                new Fuel()
                {
                    Name = "ON",
                    Price = 515,
                    Volume = 9319,
                },
                new Fuel()
                {
                    Name = "LPG",
                    Price = 275,
                    Volume = 4913,
                }
            };

            List<Fuel> fuelList2 = new List<Fuel>()
            {
                new Fuel()
                {
                    Name = "B95",
                    Price = 445,
                    Volume = 9156,
                },
                new Fuel()
                {
                    Name = "B98",
                    Price = 475,
                    Volume = 12782,
                },
                new Fuel()
                {
                    Name = "ON",
                    Price = 515,
                    Volume = 15705,
                },
                new Fuel()
                {
                    Name = "LPG",
                    Price = 275,
                    Volume = 9321,
                }
            };

            List<Fuel> fuelList3 = new List<Fuel>()
            {
                new Fuel()
                {
                    Name = "B95",
                    Price = 445,
                    Volume = 4265,
                },
                new Fuel()
                {
                    Name = "B98",
                    Price = 475,
                    Volume = 18857,
                },
                new Fuel()
                {
                    Name = "ON",
                    Price = 515,
                    Volume = 12886,
                },
                new Fuel()
                {
                    Name = "LPG",
                    Price = 275,
                    Volume = 12136,
                }
            };

            List<Dispenser> dispensers = new List<Dispenser>()
            {
                new Dispenser()
                {
                    Number=1,
                    FuelList=fuelList1,
                    MaxVolumePerType=_maxVolume
                },
                new Dispenser()
                {
                    Number=2,
                    FuelList=fuelList2,
                    MaxVolumePerType=_maxVolume
                },
                new Dispenser()
                {
                    Number=3,
                    FuelList=fuelList3,
                    MaxVolumePerType=_maxVolume
                }
            };

            var options = new Options()
            {
                Users = new List<User>()
                {
                    new User()
                    {
                        Login= "pracownik",
                        Password= "admin123",
                        Role= Roles.Pracownik
                    },
                    new User()
                    {
                        Login= "klient1",
                        Password= "klient123",
                        Role= Roles.Klient
                    },
                    new User()
                    {
                        Login= "klient2",
                        Password= "klient123",
                        Role= Roles.Klient
                    },
                    new User()
                    {
                        Login= "klient3",
                        Password= "klient123",
                        Role= Roles.Klient
                    },
                }
            };

            Cpn cpnSettings = new Cpn()
            {
                Options = options,
                Dispensers = dispensers
            };
            SaveXml(cpnSettings);

        }

        public Cpn ReadXml()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Cpn));

            serializer.UnknownNode += new XmlNodeEventHandler(serializer_UnknownNode);
            serializer.UnknownAttribute += new XmlAttributeEventHandler(serializer_UnknownAttribute);

            FileStream fs = new FileStream(_filename, FileMode.Open);
            Cpn cpn = (Cpn)serializer.Deserialize(fs);
            fs.Close();
            return cpn;
        }

        public void SaveXml(Cpn cpnSettings)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Cpn));
            TextWriter writer = new StreamWriter(_filename);

            serializer.Serialize(writer, cpnSettings);
            writer.Close();
        }

        public void SetFilename(string filename)
        {
            _filename = filename;
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
