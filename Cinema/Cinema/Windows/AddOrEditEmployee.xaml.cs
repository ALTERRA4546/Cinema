using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media.Imaging;
using static Cinema.Authorization;

namespace Cinema
{
    /// <summary>
    /// Логика взаимодействия для AddOrEditEmployee.xaml
    /// </summary>
    public partial class AddOrEditEmployee : Window
    {
        public AddOrEditEmployee()
        {
            InitializeComponent();
        }

        public string employeePhotoPath;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadData()
        {
            try
            {
                using (var dataBase = new CinemaEntities())
                {
                    var roleData = dataBase.Role.Select(s => s.Title).ToList();
                    EmployeeRole.ItemsSource = roleData;

                    if (TransmittedData.idSelectedEmployee != -1)
                    {

                        var employeeData = (from employee in dataBase.Employee
                                            join
                                            role in dataBase.Role on employee.IDRole equals role.IDRole into roleGroup
                                            from role in roleGroup.DefaultIfEmpty()
                                            where (employee.IDEmployee == TransmittedData.idSelectedEmployee)
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
                                            }).FirstOrDefault();

                        EmployeeSurname.Text = employeeData.Surname;
                        EmployeeName.Text = employeeData.Name;
                        EmployeePatronymic.Text = employeeData.Patronymic;
                        EmployeePhone.Text = employeeData.Phone;
                        EmployeeEmail.Text = employeeData.Email;
                        EmployeeRole.SelectedItem = employeeData.employeeRole;
                        EmployeeLoggin.Text = employeeData.Login;
                        EmployeePassword.Text = employeeData.Password;

                        if (employeeData.Photo != null)
                        {
                            this.Height = 695;
                            EmployeePhoto.Visibility = Visibility.Visible;

                            BitmapImage bitmapImage = new BitmapImage();
                            bitmapImage.BeginInit();
                            bitmapImage.StreamSource = new MemoryStream(employeeData.Photo);
                            bitmapImage.EndInit();

                            EmployeePhoto.Source = bitmapImage;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadPhoto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "PNG (*.png)|*.png|JPG (*.jpg)|*.jpg";

                if (openFileDialog.ShowDialog() == true)
                {
                    employeePhotoPath = openFileDialog.FileName;

                    this.Height = 695;
                    EmployeePhoto.Visibility = Visibility.Visible;
                    EmployeePhoto.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string phonePattern = @"^\+7\(\d{3}\)\d{3}-\d{2}-\d{2}$"; 
                string mailPattern = @"^[a-zA-Z0-9._%+-]+@mail\.ru$";
                string gmailPattern = @"^[a-zA-Z0-9._%+-]+@gmail\.com$";
                string yandexMailPattern = @"^[a-zA-Z0-9._%+-]+@yandex\.ru$";

                if (EmployeeSurname.Text == "" && EmployeeName.Text == "" || EmployeePatronymic.Text == "" || EmployeeLoggin.Text == "" || EmployeePassword.Text == "" || EmployeeRole.SelectedIndex < 0)
                {
                    MessageBox.Show("Данные не были заполнены", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!Regex.IsMatch(EmployeePhone.Text, phonePattern))
                {
                    MessageBox.Show("Номер телефона заполняеться в формате +7(999)999-99-99", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!Regex.IsMatch(EmployeeEmail.Text, mailPattern) && !Regex.IsMatch(EmployeeEmail.Text, gmailPattern) && !Regex.IsMatch(EmployeeEmail.Text, yandexMailPattern))
                {
                    MessageBox.Show("Почта должна содержать доменный адрес (@mail.ru / @gmail.com / @yandex.ru)", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }    

                using (var dataBase = new CinemaEntities())
                {
                    if (TransmittedData.idSelectedEmployee != -1)
                    {
                        var employeeData = dataBase.Employee.Where(w => w.IDEmployee == TransmittedData.idSelectedEmployee).FirstOrDefault();

                        employeeData.Surname = EmployeeSurname.Text;
                        employeeData.Name = EmployeeName.Text;
                        employeeData.Patronymic = EmployeePatronymic.Text;
                        employeeData.Phone = EmployeePhone.Text;
                        employeeData.Email = EmployeeEmail.Text;

                        var roleData = dataBase.Role.Where(w => w.Title == EmployeeRole.SelectedItem.ToString()).FirstOrDefault();

                        employeeData.IDRole = roleData.IDRole;
                        employeeData.Login = EmployeeLoggin.Text;
                        employeeData.Password = EmployeePassword.Text;

                        if (employeePhotoPath != null)
                        {
                            employeeData.Photo = File.ReadAllBytes(employeePhotoPath);
                        }

                        dataBase.SaveChanges();
                    }
                    else
                    {
                        var newEmployeeData = new Employee();

                        newEmployeeData.Surname = EmployeeSurname.Text;
                        newEmployeeData.Name = EmployeeName.Text;
                        newEmployeeData.Patronymic = EmployeePatronymic.Text;
                        newEmployeeData.Phone = EmployeePhone.Text;
                        newEmployeeData.Email = EmployeeEmail.Text;

                        var roleData = dataBase.Role.Where(w => w.Title == EmployeeRole.SelectedItem.ToString()).FirstOrDefault();

                        newEmployeeData.IDRole = roleData.IDRole;
                        newEmployeeData.Login = EmployeeLoggin.Text;
                        newEmployeeData.Password = EmployeePassword.Text;

                        if (employeePhotoPath != null)
                        {
                            newEmployeeData.Photo = File.ReadAllBytes(employeePhotoPath);
                        }

                        dataBase.Employee.Add(newEmployeeData);

                        dataBase.SaveChanges();
                    }

                    MessageBox.Show("Данные были сохранены", "Готово", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
