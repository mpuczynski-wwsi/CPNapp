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

            IShow loginView = new Login();
            IShow pracownikView = new Pracownik()
            {
                Controller = new PracownikController()
            };





            Application.Init();
			Top = Application.Top;
            if (LoggedUser == null)
            {
                pracownikView.Show(Top);
                //loginView.Show(Top);
            }
            else
            {

            }


			

			Application.Run();

		}
    }
}
