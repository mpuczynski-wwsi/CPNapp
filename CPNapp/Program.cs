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
        private static Toplevel Top { get; set; }


        static void Main(string[] args)
        {
            Application.Init();
            Top = Application.Top;

            Login loginView = new Login()
            {
                Controller = new LoginController(Top)
            };

            loginView.Show(Top);
           


			

			Application.Run();

		}
    }
}
