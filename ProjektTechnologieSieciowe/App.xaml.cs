using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using TechnologieSiecioweLibrary;

namespace ProjektTechnologieSieciowe
{
    public partial class App : Application
    {
        private DispatcherTimer _sessionTimer;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            SessionManager.UpdateActivity();

            _sessionTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(200)
            };
            _sessionTimer.Tick += CheckSessionValidity;
            _sessionTimer.Start();
        }

        private void CheckSessionValidity(object sender, EventArgs e)
        {
            if (!SessionManager.IsSessionValid())
            {
                LogoutUser();
            }

            foreach(Window window in Current.Windows)
            {
                if(window is GlowneOkno)
                {
                    GlowneOkno okno = (GlowneOkno)window;
                    okno.DaneMenuItem2.Header = $"Sesja wygaśnie: {DateTimeHelper.FormatDateTime2(Config.token.ExpirationDate)}" +
                        $"({DateTimeHelper.FormatDateTime3(SessionManager.GetRemainingTime())})";
                }
            }
        }

        private void LogoutUser()
        {
            _sessionTimer.Stop();

            var loginWindow = new MainWindow();
            loginWindow.Title = "Sesja wygasła - zaloguj się ponownie";
            loginWindow.Show();
            Config.token = null;
            SessionManager.SessionExpiration = null;

            foreach (Window window in Current.Windows)
            {
                if (!(window is MainWindow))
                {
                    window.Close();
                }
            }

            _sessionTimer.Start();
        }
    }
}
