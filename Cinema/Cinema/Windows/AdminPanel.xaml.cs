using Cinema.Pages;
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
using static Cinema.Authorization;

namespace Cinema
{
    /// <summary>
    /// Логика взаимодействия для AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Window
    {
        public AdminPanel()
        {
            InitializeComponent();
        }

        public bool exitMode;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PageManager.Navigate(new EmployeeControls());
            CurrentPage.Text = "Сотрудники";
        }

        private void EmployeePage_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Navigate(new EmployeeControls());
            CurrentPage.Text = "Сотрудники";
        }

        private void SettingsPage_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Navigate(new SettingsControls());
            CurrentPage.Text = "Настройки";
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            exitMode = true;

            TransmittedData.idEmployee = 0;
            TransmittedData.idSelectedMovie = 0;
            TransmittedData.idSelectedGenre = 0;
            TransmittedData.idSelectedActor = 0;
            TransmittedData.idSelectedSession = 0;
            TransmittedData.idSelectedEmployee = 0;

            Authorization authorization = new Authorization();
            authorization.Show();

            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!exitMode)
            {
                Application.Current.Shutdown();
            }
        }
    }
}
