﻿using Proiect_ABD.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Proiect_ABD.Model;
using Proiect_ABD.Utils;
using Proiect_ABD.View_Model;
using Proiect_ABD.Data;

namespace Proiect_ABD.Utils
{
    public class NavigationClass
    {
        public static Users CurrentUser { get; set; }
        public static Window ActiveWindow { get; set; }
        public static Window PrevWindow { get; set; }


        public static void NavigateTo(string windowName)
        {

            if (ActiveWindow != null)
            {

                Window newWindow = CreateWindow(windowName);

                if (newWindow != null)
                {
                    PrevWindow = ActiveWindow;
                    ActiveWindow = newWindow;
                    ActiveWindow.Show();
                    ActiveWindow.Activate();
                    PrevWindow.Close();
                }
            }


        }
        public static void OpenWindow(string windowName, string errorMessage = null)
        {
            Window newWindow = CreateWindow(windowName, errorMessage);

            if (newWindow != null)
            {
                newWindow.Owner = ActiveWindow;
                newWindow.Show();
                newWindow.Activate();
            }
        }

        private static Window CreateWindow(string windowName, string errorMessage = null)
        {
            switch (windowName)
            {
                case "LoginView":
                    return new LoginView();
                case "DashboardView":
                    var userRepository = new UsersRepository();
                    var rolesRepository = new RolesRepository();
                    var notificationService = new NotificationService();
                    var viewModelFactory = new ViewModelFactory(userRepository, rolesRepository, notificationService); // Declare before using
                    var dashboardView = new DashboardView();
                    dashboardView.DataContext = new DashboardViewModel(CurrentUser, notificationService, viewModelFactory);
                    return dashboardView;
                default:
                    return null;
            }
        }
    }
}
