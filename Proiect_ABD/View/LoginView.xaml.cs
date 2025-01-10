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
        public LoginView()
        {
            InitializeComponent();
            DataContext = new View_Model.LoginViewModel();
            ((View_Model.LoginViewModel)DataContext).RequestClose += ViewModel_RequestClose;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is View_Model.LoginViewModel viewModel)
            {
                viewModel.UpdatePassword(((PasswordBox)sender).Password);
            }
        }


        private void ViewModel_RequestClose(LoginViewModel sender)
        {
            MessageBox.Show("RequestClose a fost apelat\n");
            DialogResult = sender.IsAuthenticated;
            Close();
        }
    }
}
