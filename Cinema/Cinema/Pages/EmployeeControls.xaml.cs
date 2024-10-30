using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
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
using static Cinema.Authorization;
using static Cinema.MovieControls;

namespace Cinema
{
    /// <summary>
    /// Логика взаимодействия для EmployeeControls.xaml
    /// </summary>
    public partial class EmployeeControls : Page
    {
        public EmployeeControls()
        {
            InitializeComponent();
        }

        public class EmployeeData
        {
            public int employeeID {  get; set; }
            public BitmapImage employeePhoto { get; set; }
            public string employeeSurname {  get; set; }
            public string employeeName { get; set; }
            public string employeePatronymic {  get; set; }
            public string employeePhone { get; set; }
            public string employeeEmail { get; set; }
            public string employeeLogin { get; set; }
            public string employeePassword { get; set; }
            public string employeeRole {  get; set; }
        }

        List <EmployeeData> employeeDataList = new List <EmployeeData> ();

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            EmployeeList.ItemsSource = employeeDataList;

            LoadData(null);
        }

        private void LoadData(string findLIne)
        {
            using (var dataBase = new CinemaEntities())
            {
                employeeDataList.Clear();

                var employeeData = (from employee in dataBase.Employee
                                    join
                                    role in dataBase.Role on employee.IDRole equals role.IDRole into roleGroup
                                    from role in roleGroup.DefaultIfEmpty()
                                    where (findLIne == null || employee.Surname.Contains(findLIne) || employee.Name.Contains(findLIne) || employee.Patronymic.Contains(findLIne) || employee.Phone.Contains(findLIne) || employee.Email.Contains(findLIne) || employee.Login.Contains(findLIne) || employee.Password.Contains(findLIne))
                                    select new
                                    {
                                        employee.IDEmployee,
                                        employee.Photo,
                                        employee.Surname,
                                        employee.Name,
                                        employee.Patronymic,
                                        employee.Phone,
                                        employee.Email,
                                        employee.Login,
                                        employee.Password,
                                        employeeRole = role.Title,
                                    }).ToList();

                foreach (var employeeLine in employeeData)
                {
                    EmployeeData employeeDataClass = new EmployeeData();

                    employeeDataClass.employeeID = employeeLine.IDEmployee;

                    if (employeeLine.Photo != null)
                    {
                        BitmapImage bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        bitmapImage.StreamSource = new MemoryStream(employeeLine.Photo);
                        bitmapImage.EndInit();

                        employeeDataClass.employeePhoto = bitmapImage;
                    }
                    else
                    {
                        employeeDataClass.employeePhoto = new BitmapImage(new Uri("pack://application:,,,/Resource/NoImage.png", UriKind.Absolute));
                    }

                    employeeDataClass.employeeSurname = employeeLine.Surname;
                    employeeDataClass.employeeName = employeeLine.Name;
                    employeeDataClass.employeePatronymic = employeeLine.Patronymic;
                    employeeDataClass.employeePhone = employeeLine.Phone;
                    employeeDataClass.employeeEmail = employeeLine.Email;
                    employeeDataClass.employeeLogin = employeeLine.Login;
                    employeeDataClass.employeePassword = employeeLine.Password;
                    employeeDataClass.employeeRole = employeeLine.employeeRole;

                    employeeDataList.Add(employeeDataClass);
                }

                var fullEmployeeData = dataBase.Employee.ToList();
                FindCounterData.Text = employeeData.Count() + "/" + fullEmployeeData.Count();

                EmployeeList.Items.Refresh();
            }
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            EmployeeCodeGrid.Width = 0;
            ImageGrid.Width = ActualWidth - ActualWidth * 0.8;
            EmployeeInfoGrid.Width = ActualWidth - ActualWidth * 0.6;
            AuthorizationGrid.Width = ActualWidth - ActualWidth * 0.6;
            EmployeeList.Height = ActualHeight - 35;
        }

        private void FindData_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadData(FindData.Text);
        }

        private void EmployeeList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedEmployee = EmployeeList.SelectedItem as EmployeeData;
            if (selectedEmployee != null)
            {
                TransmittedData.idSelectedEmployee = selectedEmployee.employeeID;

                AddOrEditEmployee addOrEditEmployee = new AddOrEditEmployee();
                addOrEditEmployee.ShowDialog();

                LoadData(FindData.Text);
            }
        }

        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            TransmittedData.idSelectedEmployee = -1;

            AddOrEditEmployee addOrEditEmployee = new AddOrEditEmployee();
            addOrEditEmployee.ShowDialog();

            LoadData(FindData.Text);
        }

        private void RemoveEmployee_Click(object sender, RoutedEventArgs e)
        {
            var selectedEmployee = EmployeeList.SelectedItem as EmployeeData;
            if (selectedEmployee != null)
            {
                if (MessageBox.Show("Вы действительно хотите удалить данного сотрудника?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    using (var dataBase = new CinemaEntities())
                    {
                        var removeEmployee = dataBase.Employee.Where(w=>w.IDEmployee == selectedEmployee.employeeID).FirstOrDefault();
                        var removeTickets = dataBase.Ticket.Where(w => w.IDEmployee == selectedEmployee.employeeID).ToList();

                        if (removeTickets.Count > 0)
                        {
                            if (MessageBox.Show("Удаляемый сотрудник имеет проданные билеты. Вы действительно хотите его удалить?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                                return;
                        }

                        dataBase.Ticket.RemoveRange(removeTickets);
                        dataBase.Employee.Remove(removeEmployee);

                        dataBase.SaveChanges();
                        MessageBox.Show("Данные были удаленны", "Готово", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                    LoadData(FindData.Text);
                }
            }
        }
    }
}
