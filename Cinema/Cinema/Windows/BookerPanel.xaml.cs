using System;
using System.Diagnostics;
using System.IO;
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                PageManager.Navigate(new MovieControls());

                currentSelectedButton = MoviePage;
                currentSelectedButton.Style = (Style)Application.Current.Resources["TabOnButtonStyle"];
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
                    currentSelectedButton.Style = (Style)Application.Current.Resources["TabOffButtonStyle"];
                }

                currentSelectedButton = sender as Button;

                currentSelectedButton.Style = (Style)Application.Current.Resources["TabOnButtonStyle"];

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

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Uri resourceUri = new Uri("pack://application:,,,/Resource/HelpDocument.pdf", UriKind.Absolute);

                Stream resourceStream = Application.GetResourceStream(resourceUri)?.Stream;
                string tempFileName = Path.GetTempFileName() + ".pdf";
                using (FileStream fileStream = new FileStream(tempFileName, FileMode.Create, FileAccess.Write))
                {
                    resourceStream.CopyTo(fileStream);
                }

                Process.Start(new ProcessStartInfo(tempFileName) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
