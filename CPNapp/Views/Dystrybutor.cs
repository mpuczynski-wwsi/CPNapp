using CPNapp.Data;
using CPNapp.Fuels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;
using Attribute = Terminal.Gui.Attribute;

namespace CPNapp.Views
{
    class Dystrybutor 
    {
        public Pos X { get; set; }
        public Pos Y { get; set; }
        public Dim Width { get; set; }
        public Dim Height { get; set; }

        public string Title { get; set; }


        public int PricePerUnit { get; set; }
        public int ActualPrice { get; set; }
        public int ActualQuantity { get; set; }


        public int SelectedFuel { get; set; }
        public List<Fuel> FuelList { get; set; }


        public bool PracownikEdit { get; set; }
        public int MaxVolume { get; set; }
        public List<ProgressBar> ProgressBarList { get; set; }


        public Window GetWindow(Toplevel top)
        {
            Window win = new Window(Title)
            {
                X = X,
                Y = Y,
                Width = Width,
                Height = Height,
                ColorScheme = Colors.Base,
            };

            int LabelWidth = 12;



            var labelPrice = new Label("Cena:")
            {
                X = 0,
                Y = 0,
                Width = LabelWidth,
                Height = 1,
                TextAlignment = Terminal.Gui.TextAlignment.Right,
            };
            win.Add(labelPrice);
            var pricehEdit = new TextField(getActualPrice())
            {
                X = Pos.Right(labelPrice) + 1,
                Y = Pos.Top(labelPrice),
                Width = 5,
                Height = 1,
                CanFocus = false
            };
            win.Add(pricehEdit);

            var labelQuantity = new Label("Ilość:")
            {
                X = 0,
                Y = 2,
                Width = LabelWidth,
                Height = 1,
                TextAlignment = Terminal.Gui.TextAlignment.Right,
            };
            win.Add(labelQuantity);
            var quantityhEdit = new TextField(getActualQuantity())
            {
                X = Pos.Right(labelQuantity) + 1,
                Y = Pos.Top(labelQuantity),
                Width = 5,
                Height = 1,
                CanFocus = false
            };
            win.Add(quantityhEdit);

            var labelPricePerQuantity = new Label("Cena / Ilość:")
            {
                X = 0,
                Y = 4,
                Width = LabelWidth,
                Height = 1,
                TextAlignment = Terminal.Gui.TextAlignment.Right,
            };
            win.Add(labelPricePerQuantity);
            var pricePerQuantityEdit = new TextField(getPricePerUnit())
            {
                X = Pos.Right(labelPricePerQuantity) + 1,
                Y = Pos.Top(labelPricePerQuantity),
                Width = 5,
                Height = 1,
                CanFocus=false
            };
            win.Add(pricePerQuantityEdit);

            if (PracownikEdit)
            {
                var labelDispenserVolume = new Label("Ilość w dystrybutorze:")
                {
                    X = 0,
                    Y = 7,
                    Width = Dim.Fill(0),
                    Height = 1,
                    TextAlignment = Terminal.Gui.TextAlignment.Right,
                };
                win.Add(labelDispenserVolume);

                var progressColors = new ColorScheme();

                progressColors.Normal = MakeColor(ConsoleColor.Green, ConsoleColor.Green);
                progressColors.Focus = MakeColor(ConsoleColor.Green, ConsoleColor.Green);
                progressColors.HotNormal = MakeColor(ConsoleColor.Yellow, ConsoleColor.Blue);
                progressColors.HotFocus = MakeColor(ConsoleColor.Yellow, ConsoleColor.Cyan);

                Pos y = Pos.Bottom(labelDispenserVolume) + 1;
                ProgressBarList = new List<ProgressBar>();
                foreach (Fuel f in FuelList)
                {
                    float fraction =  f.Quantity / (float)MaxVolume; 

                    var label = new Label($"{f.Symbol} ")
                    {
                        X = 0,
                        Y = y,
                        Width = 4,
                        Height = 1,
                        TextAlignment = Terminal.Gui.TextAlignment.Right,
                    };
                    win.Add(label);

                    var pb = new ProgressBar()
                    {
                        X = 4,
                        Y = y,
                        Width = Dim.Fill(),
                        Height = 1,
                        Fraction = fraction,
                        ColorScheme = progressColors
                    };
                    win.Add(pb);
                    ProgressBarList.Add(pb);
                    y += 2;
                }


            }



            return win;
        }

        public void updateProgressBar()
        {
            int i = 0;
            foreach (Fuel f in FuelList)
            {
                float fraction = f.Quantity / (float)MaxVolume;
                ProgressBarList[i].Fraction = fraction;
                ProgressBarList[i].Redraw(ProgressBarList[i].Bounds);
                i++;
            }
        }

        private string getPricePerUnit()
        {
            SetPricePerUnit();
            var ppu = PricePerUnit / 100f;
            return String.Format("{0:0.00}", ppu);
        }


        private string getActualPrice()
        {
            var ap = ActualPrice / 100f;
            return String.Format("{0:0.00}", ap);
        }

        private string getActualQuantity()
        {
            return String.Format("{0:D}", ActualQuantity);
        }

        private void SetPricePerUnit()
        {
            if (SelectedFuel != -1)
            {
                PricePerUnit = FuelList[SelectedFuel].Price;
            }
            else
            {
                PricePerUnit = 0;
            }
        }

        private Attribute MakeColor(ConsoleColor f, ConsoleColor b)
        {
            return new Attribute(
                value: ((((int)f) & 0xffff) << 16) | (((int)b) & 0xffff),
                foreground: (Color)f,
                background: (Color)b
                );
        }

    }
}
