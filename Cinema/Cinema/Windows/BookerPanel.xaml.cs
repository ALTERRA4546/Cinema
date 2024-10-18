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

        private void MoviePage_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Navigate(new MovieControls());
            CurrentPage.Text = "Фильмы";
        }

        private void SessionPage_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Navigate(new SessionControls());
            CurrentPage.Text = "Сеансы";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PageManager.Navigate(new MovieControls());
            CurrentPage.Text = "Фильмы";
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
            CurrentPage.Text = "Аналитика продаж";
        }

        public void MovieAnalysisOpen()
        {
            PageManager.Navigate(new MovieAnalysis());
            CurrentPage.Text = "Аналитика сеансов";
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
