using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Configuration;
using System.IO;

namespace Cinema
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();
        }

        // Передоваемые данные
        public static class TransmittedData
        { 
            public static int idEmployee { get; set; }
            public static int idSelectedMovie { get; set; }
            public static int idSelectedGenre { get; set; }
            public static int idSelectedActor { get; set; }
            public static int idSelectedSession { get; set; }
            public static int idSelectedEmployee { get; set; }
            public static int idSelectedCashierMovie { get; set; }
            public static int idSelectedCashierSession { get; set; }
            public static int idSelectedMovieAnalysis {  get; set; }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (File.Exists("ConnectionData.ini"))
            {
                DataBaseConnection.Text = File.ReadAllText("ConnectionData.ini");
            }
        }

        private async void AuthorizationUser()
        {
            LoadView.Visibility = Visibility.Visible;

            string password = Password.Text;
            string login = Login.Text;
            string dataSource = DataBaseConnection.Text;

            var userData = await Task.Run(() => LoadDataBase(password, login, dataSource));

            LoadView.Visibility = Visibility.Collapsed;

            if (userData != null)
            {
                switch (userData)
                {
                    case "Букер":
                        BookerPanel bookerPanel = new BookerPanel();
                        bookerPanel.Show();
                        this.Hide();
                        break;

                    case "Кассир":
                        CashierPanel cashierPanel = new CashierPanel();
                        cashierPanel.Show();
                        this.Hide();
                        break;

                    case "Директор":
                        DirectorsPanel directorsPanel = new DirectorsPanel();
                        directorsPanel.Show();
                        this.Hide();
                        break;

                    case "Администратор":
                        AdminPanel adminPanel = new AdminPanel();
                        adminPanel.Show();
                        this.Hide();
                        break;
                }
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
        }

        public async Task<string> LoadDataBase(string password, string login, string newDataSource)
        {
            ConnectionStringChanged(newDataSource);

            using (var dataBase = new CinemaEntities())
            {
                var userData = (from emploee in dataBase.Employee
                                join
                                role in dataBase.Role on emploee.IDRole equals role.IDRole
                                where (emploee.Login == login && emploee.Password == password)
                                select new
                                {
                                    emploee.IDEmployee,
                                    Role = role.Title
                                }).FirstOrDefault();

                if (userData != null)
                {
                    TransmittedData.idEmployee = userData.IDEmployee;

                    return userData.Role;
                }
                else
                {
                    return null;
                }
            }
        }     

        private void ConnectionStringChanged(string newDataSource)
        {
            string connectionStringName = "CinemaEntities";

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ConnectionStringSettings settings = config.ConnectionStrings.ConnectionStrings[connectionStringName];

            if (settings != null)
            {
                string currentConnectionString = settings.ConnectionString;

                string currentConnectionName = "";
                string[] tempData = currentConnectionString.Split(';');
                for (int i = 0; i < tempData.Length; i++)
                {
                    if (tempData[i].Contains("data source="))
                    {
                        currentConnectionName = tempData[i].Remove(0, 40);
                        break;
                    }
                }

                string newConnectionString = currentConnectionString.Replace(currentConnectionName, newDataSource);

                settings.ConnectionString = newConnectionString;
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("connectionStrings");

                File.WriteAllText("ConnectionData.ini", newDataSource);
            }
        }

        /*private void AuthorizationUser()
        {
            ConnectionStringChanged();

            using (var dataBase = new CinemaEntities())
            {
                var userData = (from emploee in dataBase.Employee
                                join
                                role in dataBase.Role on emploee.IDRole equals role.IDRole
                                where (emploee.Login == Login.Text && emploee.Password == Password.Text)
                                select new
                                {
                                    emploee.IDEmployee,
                                    Role = role.Title
                                }).FirstOrDefault();

                if (userData != null)
                {
                    TransmittedData.idEmployee = userData.IDEmployee;

                    switch (userData.Role)
                    {
                        case "Букер":
                            BookerPanel bookerPanel = new BookerPanel();
                            bookerPanel.Show();
                            this.Hide();
                            break;

                        case "Кассир":
                            CashierPanel cashierPanel = new CashierPanel();
                            cashierPanel.Show();
                            this.Hide();
                            break;

                        case "Директор":
                            DirectorsPanel directorsPanel = new DirectorsPanel();
                            directorsPanel.Show();
                            this.Hide();
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
        }

        private void ConnectionStringChanged()
        {
            string connectionStringName = "CinemaEntities";
            string newDataSource = DataBaseConnection.Text;

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ConnectionStringSettings settings = config.ConnectionStrings.ConnectionStrings[connectionStringName];

            if (settings != null)
            {
                string currentConnectionString = settings.ConnectionString;

                string currentConnectionName = "";
                string[] tempData = currentConnectionString.Split(';');
                for (int i = 0; i < tempData.Length; i++)
                {
                    if (tempData[i].Contains("data source="))
                    { 
                        currentConnectionName = tempData[i].Remove(0,40);
                        break;
                    }
                }

                string newConnectionString = currentConnectionString.Replace(currentConnectionName, newDataSource);

                settings.ConnectionString = newConnectionString;
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("connectionStrings");

                File.WriteAllText("ConnectionData.ini", newDataSource);
            }
        }*/

        // Авторизация
        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            AuthorizationUser();
        }

        private void DataBaseConnection_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Login.Focus();
            }
        }

        private void Login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Password.Focus();
            }
        }

        private void Password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AuthorizationUser();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
