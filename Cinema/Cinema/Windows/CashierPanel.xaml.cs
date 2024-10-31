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
    /// Логика взаимодействия для CashierPanel.xaml
    /// </summary>
    public partial class CashierPanel : Window
    {
        public CashierPanel()
        {
            InitializeComponent();
        }

        public bool exitMode;
        public Button currentSelectedButton;
        public string currentSelectedPage;
        public LinearGradientBrush linearGradientBrush = new LinearGradientBrush();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            linearGradientBrush.StartPoint = new Point(0.5, 0);
            linearGradientBrush.EndPoint = new Point(0.5, 1);
            linearGradientBrush.Opacity = 0.3;
            linearGradientBrush.GradientStops.Add(new GradientStop(Colors.Blue, 0.0));
            linearGradientBrush.GradientStops.Add(new GradientStop(Colors.Aqua, 0.35));

            MoviePageOpen();

            currentSelectedButton = MainPage;
            currentSelectedButton.Background = linearGradientBrush;
        }

        private void SelectedPage_Click(object sender, RoutedEventArgs e)
        {
            MoviePageOpen();
        }

        private void SelectedBackPage_Click(object sender, RoutedEventArgs e)
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

        public void MoviePageOpen()
        {
            PageManager.Navigate(new MovieCashierControls());

            GoBack.Background = Brushes.Transparent;
            MainPage.Background = linearGradientBrush;
            GoBack.Content = "";
        }

        public void SessionPageOpen()
        {
            PageManager.Navigate(new SessionCashierControls());

            MainPage.Background = Brushes.Transparent;
            GoBack.Background = linearGradientBrush;
            GoBack.Content = "Фильмы";
        }

        public void PlacesPageOpen()
        {
            PageManager.Navigate(new PlacesCashierControls());
            GoBack.Content = "Сеансы";
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
    }
}
