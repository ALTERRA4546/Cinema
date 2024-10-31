using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static Cinema.Authorization;

namespace Cinema
{
    /// <summary>
    /// Логика взаимодействия для BookerPanel.xaml
    /// </summary>
    public partial class BookerPanel : Window
    {
        public BookerPanel()
        {
            InitializeComponent();
        }

        public bool exitMode;
        public Button currentSelectedButton;
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

                PageManager.Navigate(new MovieControls());

                currentSelectedButton = MoviePage;
                currentSelectedButton.Background = linearGradientBrush;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SelectedPage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (currentSelectedButton != null)
                {
                    currentSelectedButton.Background = Brushes.Transparent;
                }

                currentSelectedButton = sender as Button;

                currentSelectedButton.Background = linearGradientBrush;

                switch (currentSelectedButton.Name)
                {
                    case "MoviePage":
                        PageManager.Navigate(new MovieControls());
                        break;

                    case "SessionPage":
                        PageManager.Navigate(new SessionControls());
                        break;

                    case "TicketPage":
                        PageManager.Navigate(new TicketAnalysis());
                        break;
                }
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void TicketPage_Click(object sender, RoutedEventArgs e)
        {
            TicketAnalysisOpen();
        }

        public void TicketAnalysisOpen()
        {
            try
            {
                PageManager.Navigate(new TicketAnalysis());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void MovieAnalysisOpen()
        {
            try
            {
                PageManager.Navigate(new MovieAnalysis());
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
