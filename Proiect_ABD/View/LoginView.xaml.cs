using Proiect_ABD.Utils;
using Proiect_ABD.View_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Proiect_ABD.View
{
    public partial class LoginView : Window
    {
        static bool IsAppStart = true;
        public LoginView()
        {
            InitializeComponent();

            if (IsAppStart)
            {
                NavigationClass.ActiveWindow = this;
                IsAppStart = false;
            }

            DataContext = new LoginViewModel();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is View_Model.LoginViewModel viewModel)
            {
                var passwordBox = sender as PasswordBox;
                if (passwordBox != null)
                {
                    // Actualizarea proprietății Password din ViewModel
                    viewModel.Password = passwordBox.Password;
                }
            }
        }
    }
}
