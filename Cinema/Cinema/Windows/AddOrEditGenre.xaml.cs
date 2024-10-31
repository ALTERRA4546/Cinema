using System;
using System.Linq;
using System.Windows;
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
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
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
