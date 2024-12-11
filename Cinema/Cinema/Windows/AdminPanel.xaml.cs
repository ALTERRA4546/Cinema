using Cinema.Pages;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
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
        public Button currentSelectedButton;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                PageManager.Navigate(new EmployeeControls());

                currentSelectedButton = EmployeePage;
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
                    case "EmployeePage":
                        PageManager.Navigate(new EmployeeControls());
                        break;

                    case "SettingsPage":
                        PageManager.Navigate(new SettingsControls());
                        break;

                    case "BackUpPage":
                        PageManager.Navigate(new BackUpControls());
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
