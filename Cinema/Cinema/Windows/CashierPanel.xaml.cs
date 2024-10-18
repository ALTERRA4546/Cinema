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
    /// Логика взаимодействия для CashierPanel.xaml
    /// </summary>
    public partial class CashierPanel : Window
    {
        public CashierPanel()
        {
            InitializeComponent();
        }

        public bool exitMode;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MoviePageOpen();
        }

        private void MainPage_Click(object sender, RoutedEventArgs e)
        {
            MoviePageOpen();
        }

        public void MoviePageOpen()
        {
            PageManager.Navigate(new MovieCashierControls());
            CurrentPage.Text = "Фильмы";
        }

        public void SessionPageOpen()
        {
            PageManager.Navigate(new SessionCashierControls());
            CurrentPage.Text = "Сеансы";
        }

        public void PlacesPageOpen()
        {
            PageManager.Navigate(new PlacesCashierControls());
            CurrentPage.Text = "Места в кино";
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            exitMode = true;

            TransmittedData.idSelectedCashierMovie = 0;
            TransmittedData.idSelectedCashierSession = 0;

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

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            Page currentPage = PageManager.Content as Page;

            if (currentPage.GetType().Name == "PlacesCashierControls")
            {
                SessionPageOpen();
            }
            else
                if (currentPage.GetType().Name == "SessionCashierControls")
            {
                MoviePageOpen();
            }
        }
    }
}
