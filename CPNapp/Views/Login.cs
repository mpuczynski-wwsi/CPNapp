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
    class Login : IShow
    {

		public LoginController Controller { get; set; }

		public void Show(Toplevel top, User user)
        {
			// Creates the top-level window to show
			var win = new Window("CPN app")
			{
				X = 0,
				Y = 1, // Leave one row for the toplevel menu

				// By using Dim.Fill(), it will automatically resize without manual intervention
				Width = Dim.Fill(),
				Height = Dim.Fill()
			};

			top.Add(win);


			var logo = new StringBuilder();


			logo.AppendLine("   ▒▓▓▓▓▓▓  ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▒   ");
			logo.AppendLine(" ▓▓▓▓▓▓▓▓▓▓ ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓");
			logo.AppendLine("░▓▓▓▓▓▓▓▓▓▓ ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓");
			logo.AppendLine("▓▓▓▓▓▓▓▓▓▓▓ ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓");
			logo.AppendLine("▓▓▓▓▓▓▒       ▓▓▓▓▓     ░▓▓▓▓▓");
			logo.AppendLine("▓▓▓▓▓▓▒         ▓▓▓     ░▓▓▓▓▓");
			logo.AppendLine("▓▓▓▓▓▓▒          ▓▓     ░▓▓▓▓▓");
			logo.AppendLine("▓▓▓▓▓▓▒     ▓           ░▓▓▓▓▓");
			logo.AppendLine("▓▓▓▓▓▓▒     ▓▓          ░▓▓▓▓▓");
			logo.AppendLine("▓▓▓▓▓▓▒     ▓▓▓▓        ░▓▓▓▓▓");
			logo.AppendLine("▓▓▓▓▓▓▒     ▓▓▓▓▓▓      ░▓▓▓▓▓");
			logo.AppendLine("▓▓▓▓▓▓▓▓▓▓▓ ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓");
			logo.AppendLine(" ▓▓▓▓▓▓▓▓▓▓ ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓");
			logo.AppendLine("  ▓▓▓▓▓▓▓▓▓ ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓ ");
			logo.AppendLine("            ▓▓▓▓▓▓▓ app       ");
			Label logoLabel = new Label() { X = 15, Y = 0, Height = 16, Width = 35 };

			logoLabel.Text = ustring.Make(logo.ToString()); // .Replace(" ", "\u00A0"); // \u00A0 is 'non-breaking space
			win.Add(logoLabel);

			var login = new Label("Login: ") { X = 3, Y = 18 };
			var password = new Label("Password: ")
			{
				X = Pos.Left(login),
				Y = Pos.Top(login) + 2
			};
			var loginText = new TextField("")
			{
				X = Pos.Right(password),
				Y = Pos.Top(login),
				Width = 30
			};
			var passText = new TextField("")
			{
				Secret = true,
				X = Pos.Left(loginText),
				Y = Pos.Top(password),
				Width = Dim.Width(loginText)
			};

			Button loginButton = new Button(6, 22, "Login");
			// Add some controls, 
			win.Add(
				// The ones with my favorite layout system, Computed
				login, password, loginText, passText,

				// The ones laid out like an australopithecus, with Absolute positions:
				// new CheckBox(3, 6, "Remember me"),
				//new RadioGroup(3, 8, (new[] { NStack.ustring.Make("_Personal"), "_Company" })),
				loginButton,
				new Button(18, 22, "Cancel")
			);


			loginButton.Clicked += () => {
				var btnText = loginButton.Text.ToString();
				var l = loginText.Text.ToString();
				var p = passText.Text.ToString();

				var (result, loggedUser) = Controller.Login(l,p);
				if (result)
                {
					top.Remove(win);
					win.Dispose();
					win.Clear();

					Controller.RedirectToWindow(loggedUser);
					

                } else
                {
					MessageBox.ErrorQuery("Exception", "Błędne dane logowania.", "Ok");

				}
			};
		}

		public void Show(Toplevel top)
		{
			Show(top, null);
		}
	}
}
