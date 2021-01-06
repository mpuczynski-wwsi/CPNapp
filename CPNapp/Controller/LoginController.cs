using CPNapp.Data;
using CPNapp.Views;
using NStack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace CPNapp.Controller
{
    class LoginController : Controller
    {

        private IShow _pracownikView;
        private IShow _klientView;
        private Toplevel _top;
        public LoginController(Toplevel top)
        {
            _top = top;
            _pracownikView = new Pracownik()
            {
                Controller = new PracownikController()
            };
            _klientView = new Klient()
            {
                Controller = new KlientController()
            };
        }

        public (bool,User) Login(string l, string p)
        {
            var user = Config.Options.Users.Where(x => x.Login == l);
            if (user.Count() > 0)
            {
                var u = user.Single();
                if (u.Password == p)
                {
                    return (true, u);
                }
            }

            return (false, null);
        }

        public void RedirectToWindow(User user)
        {
            switch (user.Role)
            {
                case Roles.Klient:
                    _klientView.Show(_top);
                    break;

                case Roles.Pracownik:
                    _pracownikView.Show(_top);
                    break;

            }
            Application.Run();
        }
    }
}
