using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;
using static System.ConsoleColor;
using CPNapp.Views;
using System.Xml;
using System.IO;
using System.Data;
using System.Xml.Linq;
using System.Xml.Serialization;
using CPNapp.Data;
using CPNapp.Controller;

namespace CPNapp
{
    class Program
    {
        public static User LoggedUser { get; private set; }
        private static Toplevel Top { get; set; }


        static void Main(string[] args)
        {
            Application.Init();
            Top = Application.Top;

            IShow loginView = new Login()
            {
                Controller = new LoginController(Top)
            };

            if (LoggedUser == null)
            {
                loginView.Show(Top);
            }


			

			Application.Run();

		}
    }
}
