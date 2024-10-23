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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cinema.Pages
{
    /// <summary>
    /// Логика взаимодействия для SettingsControls.xaml
    /// </summary>
    public partial class SettingsControls : Page
    {
        public SettingsControls()
        {
            InitializeComponent();
            LoadData();
        }

        public int selectedRowHall;
        public int selectedPlaceHall;

        private void LoadData()
        {
            List<int> numericRowList = new List<int>();
            List<int> numericPlaceList = new List<int>();

            for (int i = 1; i < 21; i++)
            {
                numericRowList.Add(i);
            }

            for (int i = 1; i < 31; i++)
            {
                numericPlaceList.Add(i);
            }

            SelectedRowHall.ItemsSource = numericRowList;
            SelectedPlaceHall.ItemsSource = numericPlaceList;

            using (var dataBase = new CinemaEntities())
            {
                var loadData = dataBase.Settings.OrderByDescending(o => o.DateTimeChange).FirstOrDefault();

                if (loadData != null)
                {
                    SelectedRowHall.SelectedItem = loadData.RowHall;
                    SelectedPlaceHall.SelectedItem = loadData.PlaceHall;
                }
            }
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ResizeWindows();
        }

        private void Hall_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ResizeWindows();
        }

        private void ResizeWindows()
        {
            double maxWidth = this.ActualWidth-10;
            double maxHeight = this.ActualHeight-10;

            double scale = this.ActualWidth / (Hall.ActualWidth * 1.28);
            double centerX = Hall.ActualWidth / 2;
            double centerY = Hall.ActualHeight / 2;

            double newWidth = Hall.ActualWidth * scale;
            double newHeight = Hall.ActualHeight * scale;

            if (newWidth > maxWidth)
            {
                scale = maxWidth / Hall.ActualWidth;
            }
            if (newHeight > maxHeight)
            {
                scale = Math.Min(scale, maxHeight / Hall.ActualHeight);
            }

            ScaleTransform scaleTransform = new ScaleTransform(scale, scale, centerX, centerY);
            Hall.RenderTransform = scaleTransform;
        }

        private void SelectedRowHall_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedRowHall = (int)SelectedRowHall.SelectedItem;
            CreateHall();
        }

        private void SelectedPlaceHall_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedPlaceHall = (int)SelectedPlaceHall.SelectedItem;
            CreateHall();
        }

        private void CreateHall()
        {
            if (selectedRowHall != 0 && selectedPlaceHall != 0)
            {
                Hall.Children.Clear();

                for (int row = 0; row < selectedRowHall; row++)
                {
                    WrapPanel wrapPanel = new WrapPanel();

                    TextBlock textBlockBegin = new TextBlock();
                    textBlockBegin.Text = $"Ряд {row+1}";
                    textBlockBegin.FontSize = 10;
                    textBlockBegin.Margin = new Thickness(0,0,5,0);
                    textBlockBegin.VerticalAlignment = VerticalAlignment.Center;

                    TextBlock textBlockEnd = new TextBlock();
                    textBlockEnd.Text = $"Ряд {row+1}";
                    textBlockEnd.FontSize = 10;
                    textBlockEnd.VerticalAlignment = VerticalAlignment.Center;

                    wrapPanel.Children.Add(textBlockBegin);
                    for (int place = 1; place <= selectedPlaceHall; place++)
                    {
                        Button button = new Button();
                        button.Content = place;
                        button.Name = $"Row{row}Place{place}";
                        button.Width = 18;
                        button.Height = 18;
                        button.FontSize = 10;
                        button.Margin = new Thickness(0, 0, 5, 0);

                        wrapPanel.Children.Add(button);
                    }
                    wrapPanel.Children.Add(textBlockEnd);

                    StackPanel stackPanel = new StackPanel();
                    stackPanel.Margin = new Thickness(0, 0, 0, 5);
                    stackPanel.HorizontalAlignment = HorizontalAlignment.Center;
                    stackPanel.Children.Add(wrapPanel);

                    Hall.Children.Add(stackPanel);
                }
            }
        }

        private void SaveHallSettings_Click(object sender, RoutedEventArgs e)
        {
            using (var dataBase = new CinemaEntities())
            {
                var newHall = new Settings();

                newHall.RowHall = (int)SelectedRowHall.SelectedItem;
                newHall.PlaceHall = (int)SelectedPlaceHall.SelectedItem;
                newHall.DateTimeChange = DateTime.Now;

                dataBase.Settings.Add(newHall);
                dataBase.SaveChanges();

                MessageBox.Show("Нстройка зала была сохранена", "Готово", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
