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
            linearGradientBrush.StartPoint = new Point(0.5, 0);
            linearGradientBrush.EndPoint = new Point(0.5, 1);
            linearGradientBrush.Opacity = 0.3;
            linearGradientBrush.GradientStops.Add(new GradientStop(Colors.Blue, 0.0));
            linearGradientBrush.GradientStops.Add(new GradientStop(Colors.Aqua, 0.35));

            PageManager.Navigate(new MovieControls());

            currentSelectedButton = MoviePage;
            currentSelectedButton.Background = linearGradientBrush;
        }

        private void SelectedPage_Click(object sender, RoutedEventArgs e)
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

        public void TicketPage_Click(object sender, RoutedEventArgs e)
        {
            TicketAnalysisOpen();
        }

        public void TicketAnalysisOpen()
        {
            PageManager.Navigate(new TicketAnalysis());
        }

        public void MovieAnalysisOpen()
        {
            PageManager.Navigate(new MovieAnalysis());
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
