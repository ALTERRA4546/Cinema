using Microsoft.Win32;
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
    /// Логика взаимодействия для BackUpControls.xaml
    /// </summary>
    public partial class BackUpControls : Page
    {
        public BackUpControls()
        {
            InitializeComponent();
        }

        public string saveBackUpPath;
        public string restorePath;

        private void OpenSavePath_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "BackUp (*.BAK)|*.BAK";

                if (saveFileDialog.ShowDialog() == true)
                {
                    saveBackUpPath = saveFileDialog.FileName;
                    BackUpPath.Text = saveBackUpPath;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveBackUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (saveBackUpPath != null)
                {
                    using (var dataBase = new CinemaEntities())
                    {
                        var backUp = dataBase.Database.SqlQuery<CinemaEntities>($"BACKUP DATABASE [Cinema] TO DISK = '{saveBackUpPath}'").ToList();
                        MessageBox.Show("Резервное копирование выполненно", "Готово", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OpenRestorePath_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "BackUp (*.BAK)|*.BAK";

                if (openFileDialog.ShowDialog() == true)
                {
                    restorePath = openFileDialog.FileName;
                    RestorePath.Text = restorePath;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RestoreDataBase_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (restorePath != null)
                {
                    using (var dataBase = new CinemaEntities())
                    {
                        var backUp = dataBase.Database.SqlQuery<CinemaEntities>($"USE master; ALTER DATABASE [Cinema] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; RESTORE DATABASE [Cinema] FROM DISK = '{restorePath}' WITH REPLACE; ALTER DATABASE [Cinema] SET MULTI_USER;").ToList();
                        MessageBox.Show("Востановление выполнено", "Готово", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
