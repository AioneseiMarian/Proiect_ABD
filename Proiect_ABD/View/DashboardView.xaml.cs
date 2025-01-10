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
    public partial class DashboardView : Window
    {
        public DashboardView()
        {
            InitializeComponent();

            if (DataContext is View_Model.DashboardViewModel viewModel)
            {
                viewModel.RequestLogout += OnRequestLogout;
            }
        }

        private void OnRequestLogout()
        {
            MessageBox.Show("Deconectare reușită.");
            DialogResult = false; // Înapoi la LoginView
            Close();
        }
    }
}
