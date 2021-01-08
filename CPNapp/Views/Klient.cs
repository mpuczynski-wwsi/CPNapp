using CPNapp.Controller;
using CPNapp.Vehicles;
using NStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Terminal.Gui;

namespace CPNapp.Views
{


	class Klient : IShow
    {
		private int _selectedDispenser = 1; 
		private int _selectedFuel = 0;

		public ListView _listView;

		public KlientController Controller { get; set; }

        public void Show(Toplevel top, User user)
        {
			Controller.LoggedUser = user;
			Vehicle vehicle = Controller.LoggedUser.UsingVehicle;

			int margin = 2;
			int padding = 1;
			int contentHeight = 8;

			Window win = new Window($"Tankowanie")
			{
				X = margin,
				Y = margin,
				Width = Dim.Fill(margin),
				Height = 30,
			};
			win.ColorScheme = Colors.Dialog;




			var window1 = new FrameView("Dystrybutory")
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
			var dispensersList = new List<Dystrybutor>();
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
					PracownikEdit=false,
					FuelList = dispenser.FuelList,
					SelectedFuel = _selectedDispenser == dispenser.Number ? _selectedFuel : -1
				};

				dispensersList.Add(dystrybutor);
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




			var window2 = new FrameView("Napełnij bak")
			{
				X = Pos.Percent(70),
				Y = 0,
				Width = Dim.Percent(100),
				Height = Dim.Percent(50),
				ColorScheme = Colors.Base,

			};

			var labelRodzaj = new Label()
			{

				TextAlignment = TextAlignment.Left,
				X = 0,
				Width = Dim.Fill(),
				Height = 2,
				ColorScheme = Colors.Base,
				Text = $"Wybierz rodzaj paliwa który\nchesz zatankować z Dystrybutora {_selectedDispenser}"
				//Text = "a Newline\nin the Label"
			};
			window2.Add(labelRodzaj);



			var rodzaje = Controller.Config.Dispensers.Where(
				(dispenser) => dispenser.Number == _selectedDispenser
			).Select(d => d.FuelList.Select(d1 => (ustring)d1.Symbol)).SelectMany(d => d).ToArray();

            var rodzajRadioGroup = new RadioGroup(rodzaje)
            {
                X = 3,
                Y = 3,
                SelectedItem = 0,
            };

			window2.Add(rodzajRadioGroup);


			var fillButton = new Button("Tankuj")
			{
				ColorScheme = Colors.Base,
				//X = Pos.Right (prev) + 2,
				X = 6,
				Y = Pos.Bottom(rodzajRadioGroup) + 2,
			};
			window2.Add(fillButton);


			win.Add(window2);


			rodzajRadioGroup.SelectedItemChanged += (args) =>
            {
                _selectedFuel = args.SelectedItem;
                _applyNewPrice(dispensersList[_selectedDispenser - 1]);
            };


			fillButton.KeyDown += (args) =>
			{
				if (args.KeyEvent.Key == Terminal.Gui.Key.ControlJ)
                {

					double delta;
					var slowDownSpeedAt = vehicle.TankCapacity * 0.97;

					if (dispensersList[_selectedDispenser - 1].ActualQuantity >= slowDownSpeedAt)
                    {
						delta = 0.03;

					}
					else
                    {
						delta = 0.23;
					}

					if (dispensersList[_selectedDispenser - 1].FuelList[_selectedFuel].Quantity <= 0)
                    {
						dispensersList[_selectedDispenser - 1].FuelList[_selectedFuel].Quantity = 0;
						Controller.UpdateConfigFile(Controller.Config);
						MessageBox.ErrorQuery("Exception", "Brak paliwa w dystrybutorze", "Ok");
					}

					if (dispensersList[_selectedDispenser - 1].ActualQuantity < vehicle.TankCapacity)
                    {
						var price = dispensersList[_selectedDispenser - 1].FuelList[_selectedFuel].Price;
						dispensersList[_selectedDispenser - 1].ActualQuantity = dispensersList[_selectedDispenser - 1].ActualQuantity + delta;
						dispensersList[_selectedDispenser - 1].DispenserScreens["quantity"].Text = dispensersList[_selectedDispenser - 1].getActualQuantity();

						var actualPrice = (dispensersList[_selectedDispenser - 1].ActualQuantity * (float)dispensersList[_selectedDispenser - 1].PricePerUnit);
						dispensersList[_selectedDispenser - 1].ActualPrice = (int)actualPrice;
						dispensersList[_selectedDispenser - 1].DispenserScreens["price"].Text = dispensersList[_selectedDispenser - 1].getActualPrice();
					} else
                    {
						MessageBox.ErrorQuery("Exception", "Zbiornik jest pełny", "Ok");
					}

					

					dispensersList[_selectedDispenser - 1].FuelList[_selectedFuel].Quantity -= delta;
					Controller.UpdateConfigFile(Controller.Config);


					Thread.Sleep(50);
				}
			};



			for (int i = 0; i < btnList.Count; i++)
			{
				var b = btnList[i];
				var sel = i + 1;
				b.Clicked += () => {
					_unselectBtn(btnList);
					_resetPrice(dispensersList);
					b.Text = $"• {b.Text} •";

					_selectedDispenser = sel;
					_selectedFuel = 0;
					_applyNewPrice(dispensersList[_selectedDispenser - 1]);

					labelRodzaj.Text = $"Wybierz rodzaj paliwa który\nchesz zatankować z Dystrybutora {sel}";

				};
			}





			var window3 = new FrameView("Dane pojazdu")
			{
				X = Pos.Percent(70),
				Y = Pos.Percent(50),
				Width = Dim.Percent(100),
				Height = Dim.Percent(50),
				ColorScheme = Colors.Base,

			};

			var label = new Label($"Marka: {vehicle.Make}")
			{
				X = 0,
				Y = 0,
				Width = 15,
				Height = 1,
			};
			window3.Add(label);

			label = new Label($"Model: {vehicle.Model}")
			{
				X = 0,
				Y = Pos.Bottom(label)+1,
				Width = 15,
				Height = 1,
			};
			window3.Add(label);





			win.Add(window3);



			top.Add(win);


		}

        private void _applyNewPrice(Dystrybutor d)
        {
            d.ActualPrice = d.FuelList[_selectedFuel].Price;
            d.SelectedFuel = _selectedFuel;
            d.DispenserScreens["price_per_quantity"].Text = d.getPricePerUnit();
        }

        private void _unselectBtn(List<Button> btns)
		{
			foreach (var b in btns)
            {
				b.Text = "Zaznacz";
			}
		}

		private void _resetPrice(List<Dystrybutor> dispensersList)
		{
			foreach (var d in dispensersList)
			{
				_selectedFuel = 0;
				d.PricePerUnit = 0;
				d.SelectedFuel = _selectedFuel;
				d.DispenserScreens["price_per_quantity"].Text = d.getPricePerUnit(false);
			}
		}

        public void Show(Toplevel top)
        {
			Show(top, null);
        }
    }


}
