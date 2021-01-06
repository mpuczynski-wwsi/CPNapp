using CPNapp.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPNapp
{
    public enum Roles
    {
        Klient,
        Pracownik,
    }
    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Roles Role { get; set; }

        public Vehicle UsingVehicle { get; set; }

    }
}
