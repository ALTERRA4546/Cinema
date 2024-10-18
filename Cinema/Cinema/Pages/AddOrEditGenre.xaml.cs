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
    /// Логика взаимодействия для AddOrEditGenre.xaml
    /// </summary>
    public partial class AddOrEditGenre : Window
    {
        public AddOrEditGenre()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            if (TransmittedData.idSelectedGenre != -1)
            {
                using (var dataBase = new CinemaEntities())
                {
                    var genreData = dataBase.Genre.Where(w => w.IDGenre == TransmittedData.idSelectedGenre).FirstOrDefault();

                    GenreTitle.Text = genreData.Title;
                }
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (GenreTitle.Text == "")
            {
                MessageBox.Show("Данные не были заполнены", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using (var dataBase = new CinemaEntities())
            {
                if (TransmittedData.idSelectedGenre != -1)
                {
                    var genreData = dataBase.Genre.Where(w => w.IDGenre == TransmittedData.idSelectedGenre).FirstOrDefault();

                    genreData.Title = GenreTitle.Text;

                    dataBase.SaveChanges();
                }
                else
                {
                    var newGenreData = new Genre();

                    newGenreData.Title = GenreTitle.Text;

                    dataBase.Genre.Add(newGenreData);
                    dataBase.SaveChanges();
                }

                MessageBox.Show("Данные были сохранены","Готово",MessageBoxButton.OK,MessageBoxImage.Information);
                this.Close();
            }
        }
    }
}
