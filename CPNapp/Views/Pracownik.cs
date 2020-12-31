using CPNapp.Controller;
using NStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace CPNapp.Views
{


	class Pracownik : IShow
    {
		private int _selectedDispenser = 1; 

		public ListView _listView;

		public PracownikController Controller { get; set; }

        public void Show(Toplevel top)
        {
			int margin = 2;
			int padding = 1;
			int contentHeight = 20;

			Window win = new Window($"Panel Administracyjny")
			{
				X = margin,
				Y = margin,
				Width = Dim.Fill(margin),
				Height = 30,
			};
			win.ColorScheme = Colors.Dialog;




			var window1 = new Window()
			{
				X = Pos.Percent(0),
				Y = 0,
				Width = Dim.Percent(100),
				Height = Dim.Percent(100),
				ColorScheme = Colors.Base,

			};
			win.Add(window1);


			var listWin = new List<View>();
			var btnList = new List<Button>();
			var dystrybutorNames = new List<ustring>();
			foreach (var dispenser in Controller.Config.Dispensers)
            {
				var dystrybutorName = $"Dystrybutor {dispenser.Number}";
				dystrybutorNames.Add(dystrybutorName);
				Dystrybutor dystrybutor = new Dystrybutor()
				{
					Title = dystrybutorName,
					X = dispenser.Number > 1 ? Pos.Right(listWin[dispenser.Number - 2]) + margin : margin,
					Y = (margin),
					Width = Dim.Percent(65 / 3),
					Height = contentHeight,
					PracownikEdit=true,
					FuelList=dispenser.FuelList,
					MaxVolume= dispenser.MaxVolumePerType
				};


				Window subWin = dystrybutor.GetWindow(top);
				window1.Add(subWin);
				listWin.Add(subWin);

				string btnText = dispenser.Number == 1 ? $"• Zaznacz •" : "Zaznacz";
				var selectButton = new Button(btnText)
				{
					ColorScheme = Colors.Base,
					//X = Pos.Right (prev) + 2,
					X = dispenser.Number > 1 ? Pos.Right(listWin[dispenser.Number - 2]) + margin + 6 : margin + 6,
					Y = Pos.Bottom(subWin) + 1,
				};
				window1.Add(selectButton);
				btnList.Add(selectButton);


			}




			var window2 = new Window("Doładuj zbiornik")
			{
				X = Pos.Percent(70),
				Y = 0,
				Width = Dim.Percent(100),
				Height = Dim.Percent(100),
				ColorScheme = Colors.Base,

			};

			var labelRodzaj = new Label()
			{

				TextAlignment = TextAlignment.Left,
				X = 0,
				Width = Dim.Fill(),
				Height = 2,
				ColorScheme = Colors.Base,
				Text = $"Wybierz rodzaj paliwa który\nchesz doładować do Dystrybutora {_selectedDispenser}"
				//Text = "a Newline\nin the Label"
			};
			window2.Add(labelRodzaj);



			var rodzaje = Controller.Config.Dispensers.Where(
				(dispenser) => dispenser.Number == _selectedDispenser
			).Select(d => d.FuelList.Select(d1 => (ustring)d1.Name)).SelectMany(d => d).ToArray();

            var radioGroup = new RadioGroup(rodzaje)
            {
                X = 3,
                Y = 3,
                SelectedItem = 2,
            };

			window2.Add(radioGroup);



			win.Add(window2);






			for (int i = 0; i < btnList.Count; i++)
			{
				var b = btnList[i];
				var sel = i + 1;
				b.Clicked += () => {
					_unselectBtn(btnList);
					b.Text = $"• {b.Text} •";
					_selectedDispenser = sel;
					labelRodzaj.Text = $"Wybierz rodzaj paliwa który\nchesz doładować do Dystrybutora {sel}";

				};
			}













			top.Add(win);










		}

		private void _unselectBtn(List<Button> btns)
		{
			foreach (var b in btns)
            {
				b.Text = "Zaznacz";

			}
		}
	}


}
