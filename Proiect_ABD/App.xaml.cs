using Proiect_ABD.View;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Proiect_ABD
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Afiseaza LoginView la pornirea aplicatiei
            var loginView = new View.LoginView();
            bool? result = loginView.ShowDialog();
            if (result == true)
            {
                MessageBox.Show("Entered to DashboardView");

                var dashboard_view = new View.DashboardView();
                dashboard_view.Show();
            }
            else
            {
                // Daca autentificarea esueaza, inchide aplicatia
                Shutdown();
            }
        }
    }
}
