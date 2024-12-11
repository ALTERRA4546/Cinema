using Cinema.Pages;
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                MoviePageOpen();

                currentSelectedButton = MainPage;
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
                    case "CurrentMoviesPage":
                        CurrentMoviesPageOpen();
                        break;

                    case "MainPage":
                        MoviePageOpen();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }         
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

        public void CurrentMoviesPageOpen()
        {
            try
            {
                PageManager.Navigate(new CurrentMovieCashierControls());

                GoBack.Style = (Style)Application.Current.Resources["TabOffButtonStyle"];
                CurrentMoviesPage.Style = (Style)Application.Current.Resources["TabOnButtonStyle"];
                currentSelectedButton = CurrentMoviesPage;
                GoBack.Content = "";
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

                GoBack.Style = (Style)Application.Current.Resources["TabOffButtonStyle"];
                MainPage.Style = (Style)Application.Current.Resources["TabOnButtonStyle"];
                currentSelectedButton = MainPage;
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

                MainPage.Style = (Style)Application.Current.Resources["TabOffButtonStyle"];
                CurrentMoviesPage.Style = (Style)Application.Current.Resources["TabOffButtonStyle"];
                GoBack.Style = (Style)Application.Current.Resources["TabOnButtonStyle"];
                currentSelectedButton = GoBack;
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
