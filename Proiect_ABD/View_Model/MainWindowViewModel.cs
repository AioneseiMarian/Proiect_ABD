using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Proiect_ABD.View_Model
{
    public class MainWindowViewModel : ViewModelBase
    {
        private object _currentView;

        public object CurrentView
        {
            get => _currentView;
            set => Set(ref _currentView, value);
        }

        public MainWindowViewModel()
        {
            // Setăm LoginView ca view inițial
            CurrentView = new View.LoginView();
            if (CurrentView is View.LoginView loginView)
            {
                if (loginView.DataContext is LoginViewModel loginViewModel)
                {
                    loginViewModel.RequestClose += (sender) => ShowDashboard();
                }
            }
        }

        // Metodă pentru a schimba vederea după autentificare
        public void ShowDashboard()
        {
            CurrentView = new View.DashboardView();
        }
    }
}
