using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SelectedPage_Click(object sender, RoutedEventArgs e)
        {
            MoviePageOpen();
        }

        private void SelectedBackPage_Click(object sender, RoutedEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void MoviePageOpen()
        {
            try
            {
                PageManager.Navigate(new MovieCashierControls());

                GoBack.Background = Brushes.Transparent;
                MainPage.Background = linearGradientBrush;
                GoBack.Content = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void SessionPageOpen()
        {
            try
            {
                PageManager.Navigate(new SessionCashierControls());

                MainPage.Background = Brushes.Transparent;
                GoBack.Background = linearGradientBrush;
                GoBack.Content = "Фильмы";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void PlacesPageOpen()
        {
            try
            {
                PageManager.Navigate(new PlacesCashierControls());
                GoBack.Content = "Сеансы";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                exitMode = true;

                TransmittedData.idSelectedCashierMovie = 0;
                TransmittedData.idSelectedCashierSession = 0;

                Authorization authorization = new Authorization();
                authorization.Show();

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (!exitMode)
                {
                    Application.Current.Shutdown();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
