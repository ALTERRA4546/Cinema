using Cinema.Pages;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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

                PageManager.Navigate(new EmployeeControls());

                currentSelectedButton = EmployeePage;
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
                    case "EmployeePage":
                        PageManager.Navigate(new EmployeeControls());
                        break;

                    case "SettingsPage":
                        PageManager.Navigate(new SettingsControls());
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
    }
}
