using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using static Cinema.Authorization;

namespace Cinema
{
    /// <summary>
    /// Логика взаимодействия для AddOrEditMovie.xaml
    /// </summary>
    public partial class AddOrEditMovie : Window
    {
        public AddOrEditMovie()
        {
            InitializeComponent();
        }

        List<int> selectedGenre = new List<int>();
        List<int> selectedActor = new List<int>();
        public string coverPath;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadGridData();
            LoadUserData();
        }

        private void LoadGridData()
        {
            try
            {
                using (var dataBase = new CinemaEntities())
                {
                    var genreAllData = dataBase.Genre.Select(s => new { s.IDGenre, s.Title }).ToList();
                    var actorAllData = dataBase.Actor.Select(s => new { s.IDActor, s.Surname, s.Name, s.Patronymic, s.Nickname }).ToList();

                    MovieGenreGrid.ItemsSource = genreAllData;
                    MovieActorGrid.ItemsSource = actorAllData;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadUserData()
        {
            try
            {
                using (var dataBase = new CinemaEntities())
                {
                    var countryAllData = dataBase.Country.Select(s => s.Title).ToList();

                    MovieCountry.ItemsSource = countryAllData;

                    if (TransmittedData.idSelectedMovie != -1)
                    {

                        var movieData = (from movie in dataBase.Movie
                                         join
                                         country in dataBase.Country on movie.IDCountry equals country.IDCountry into countryGroup
                                         from country in countryGroup.DefaultIfEmpty()
                                         where (movie.IDMovie == TransmittedData.idSelectedMovie)
                                         select new
                                         {
                                             movie.IDMovie,
                                             movie.Cover,
                                             movieTitle = movie.Title,
                                             movie.YearOfPublication,
                                             movie.Timing,
                                             movie.AgeRating,
                                             movie.Description,
                                             movieCountry = country.Title,
                                         }
                                         ).FirstOrDefault();

                        var genreData = (from genre in dataBase.Genre
                                         join
                                         movieGenre in dataBase.MovieGenre on genre.IDGenre equals movieGenre.IDGenre into movieGenreGroup
                                         from movieGenre in movieGenreGroup.DefaultIfEmpty()
                                         where (movieGenre.IDMovie == TransmittedData.idSelectedMovie)
                                         select new
                                         {
                                             genre.IDGenre,
                                             genre.Title,
                                         }).ToList();

                        foreach (var genreLine in genreData)
                        {
                            selectedGenre.Add(genreLine.IDGenre);
                        }

                        var actorData = (from actor in dataBase.Actor
                                         join
                                         actorInMovie in dataBase.ActorsInMovies on actor.IDActor equals actorInMovie.IDActor into actorInMovieGroup
                                         from actorInMovie in actorInMovieGroup.DefaultIfEmpty()
                                         where (actorInMovie.IDMovie == TransmittedData.idSelectedMovie)
                                         select new
                                         {
                                             actor.IDActor,
                                             actor.Surname,
                                             actor.Name,
                                             actor.Patronymic,
                                             actor.Nickname,
                                         }).ToList();

                        foreach (var actorLine in actorData)
                        {
                            selectedActor.Add(actorLine.IDActor);
                        }

                        MovieTitle.Text = movieData.movieTitle;
                        MovieCountry.SelectedItem = movieData.movieCountry;
                        MovieYearOfPublication.Text = movieData.YearOfPublication.ToString();
                        MovieTiming.Text = movieData.Timing.ToString();
                        MovieAgeRating.Text = movieData.AgeRating.ToString();
                        MovieDescription.Text = movieData.Description;

                        if (movieData.Cover != null)
                        {
                            this.Height = 780;
                            MovieCover.Visibility = Visibility.Visible;

                            BitmapImage bitmapImage = new BitmapImage();
                            bitmapImage.BeginInit();
                            bitmapImage.StreamSource = new MemoryStream(movieData.Cover);
                            bitmapImage.EndInit();

                            MovieCover.Source = bitmapImage;
                        }

                        foreach (var genreLine in genreData)
                        {
                            MovieGenere.Text += "|" + genreLine.Title + "|";
                        }

                        foreach (var actorLine in actorData)
                        {
                            MovieActor.Text += "|" + actorLine.Surname + " " + actorLine.Name + " " + actorLine.Patronymic + " " + actorLine.Nickname + "|";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearGenre_Click(object sender, RoutedEventArgs e)
        {
            selectedGenre.Clear();
            MovieGenere.Clear();
        }

        private void ClearActor_Click(object sender, RoutedEventArgs e)
        {
            selectedActor.Clear();
            MovieActor.Clear();
        }

        private void LoadCover_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "JPG (*.jpg)|*.jpg|PNG (*.png)|*.png";

                if (openFileDialog.ShowDialog() == true)
                {
                    coverPath = openFileDialog.FileName;

                    this.Height = 780;
                    MovieCover.Visibility = Visibility.Visible;
                    MovieCover.Source = new BitmapImage(new Uri(openFileDialog.FileName));
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
                if (MovieCountry.SelectedIndex < 0 || MovieTitle.Text == "" || !int.TryParse(MovieYearOfPublication.Text, out _) || !double.TryParse(MovieTiming.Text, out _) || !int.TryParse(MovieAgeRating.Text, out _) || MovieDescription.Text == "" || MovieGenere.Text == "" || MovieActor.Text == "")
                {
                    MessageBox.Show("Данные заполнены неправильно", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                using (var dataBase = new CinemaEntities())
                {
                    if (TransmittedData.idSelectedMovie != -1)
                    {
                        var movieData = dataBase.Movie.Where(w => w.IDMovie == TransmittedData.idSelectedMovie).FirstOrDefault();

                        movieData.Title = MovieTitle.Text;

                        var selectedCountry = dataBase.Country.Where(w => w.Title == MovieCountry.SelectedItem.ToString()).FirstOrDefault();
                        movieData.IDCountry = selectedCountry.IDCountry;

                        movieData.YearOfPublication = Convert.ToInt32(MovieYearOfPublication.Text);
                        movieData.Timing = Convert.ToInt32(MovieTiming.Text);
                        movieData.AgeRating = Convert.ToInt32(MovieAgeRating.Text);
                        movieData.Description = MovieDescription.Text;

                        if (coverPath != null)
                        {
                            movieData.Cover = File.ReadAllBytes(coverPath);
                        }

                        var removeGenre = dataBase.MovieGenre.Where(w => w.IDMovie == TransmittedData.idSelectedMovie).ToList();
                        if (removeGenre != null)
                            dataBase.MovieGenre.RemoveRange(removeGenre);

                        foreach (var genreLine in selectedGenre)
                        {
                            var newMovieGenre = new MovieGenre();
                            newMovieGenre.IDMovie = movieData.IDMovie;
                            newMovieGenre.IDGenre = genreLine;

                            dataBase.MovieGenre.Add(newMovieGenre);
                        }

                        var removeActorInMovie = dataBase.ActorsInMovies.Where(w => w.IDMovie == TransmittedData.idSelectedMovie).ToList();
                        if (removeActorInMovie != null)
                            dataBase.ActorsInMovies.RemoveRange(removeActorInMovie);

                        foreach (var actorLine in selectedActor)
                        {
                            var newMovieActor = new ActorsInMovies();
                            newMovieActor.IDMovie = movieData.IDMovie;
                            newMovieActor.IDActor = actorLine;

                            dataBase.ActorsInMovies.Add(newMovieActor);
                        }

                        dataBase.SaveChanges();
                    }
                    else
                    {
                        var newMovieData = new Movie();

                        newMovieData.Title = MovieTitle.Text;

                        var selectedCountry = dataBase.Country.Where(w => w.Title == MovieCountry.SelectedItem.ToString()).FirstOrDefault();
                        newMovieData.IDCountry = selectedCountry.IDCountry;

                        newMovieData.YearOfPublication = Convert.ToInt32(MovieYearOfPublication.Text);
                        newMovieData.Timing = Convert.ToInt32(MovieTiming.Text);
                        newMovieData.AgeRating = Convert.ToInt32(MovieAgeRating.Text);
                        newMovieData.Description = MovieDescription.Text;

                        if (coverPath != null)
                        {
                            newMovieData.Cover = File.ReadAllBytes(coverPath);
                        }

                        dataBase.Movie.Add(newMovieData);
                        dataBase.SaveChanges();

                        foreach (var genreLine in selectedGenre)
                        {
                            var newMovieGenre = new MovieGenre();
                            newMovieGenre.IDMovie = newMovieData.IDMovie;
                            newMovieGenre.IDGenre = genreLine;

                            dataBase.MovieGenre.Add(newMovieGenre);
                        }

                        foreach (var actorLine in selectedActor)
                        {
                            var newMovieActor = new ActorsInMovies();
                            newMovieActor.IDMovie = newMovieData.IDMovie;
                            newMovieActor.IDActor = actorLine;

                            dataBase.ActorsInMovies.Add(newMovieActor);
                        }

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

        private void MovieGenreGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DataGridRow row = (DataGridRow)MovieGenreGrid.ItemContainerGenerator.ContainerFromIndex(MovieGenreGrid.SelectedIndex);
                DataGridCell cellId = MovieGenreGrid.Columns[0].GetCellContent(row).Parent as DataGridCell;
                DataGridCell cellTitle = MovieGenreGrid.Columns[1].GetCellContent(row).Parent as DataGridCell;
                int idGenre = Convert.ToInt32(((TextBlock)cellId.Content).Text);
                string titleGenre = ((TextBlock)cellTitle.Content).Text;

                bool availabilityGenre = false;
                foreach (var genreLine in selectedGenre)
                {
                    if (genreLine == idGenre)
                    {
                        availabilityGenre = true;
                    }
                }

                if (!availabilityGenre)
                {
                    selectedGenre.Add(idGenre);
                    MovieGenere.Text += "|" + titleGenre + "|";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MovieActorGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DataGridRow row = (DataGridRow)MovieActorGrid.ItemContainerGenerator.ContainerFromIndex(MovieActorGrid.SelectedIndex);
                DataGridCell cellId = MovieActorGrid.Columns[0].GetCellContent(row).Parent as DataGridCell;
                DataGridCell cellSurname = MovieActorGrid.Columns[1].GetCellContent(row).Parent as DataGridCell;
                DataGridCell cellName = MovieActorGrid.Columns[2].GetCellContent(row).Parent as DataGridCell;
                DataGridCell cellPatronymic = MovieActorGrid.Columns[3].GetCellContent(row).Parent as DataGridCell;
                DataGridCell cellNickname = MovieActorGrid.Columns[4].GetCellContent(row).Parent as DataGridCell;
                int idActor = Convert.ToInt32(((TextBlock)cellId.Content).Text);
                string surnameActor = ((TextBlock)cellSurname.Content).Text;
                string nameActor = ((TextBlock)cellName.Content).Text;
                string patronymicActor = ((TextBlock)cellPatronymic.Content).Text;
                string nicknameActor = ((TextBlock)cellNickname.Content).Text;

                bool availabilityActor = false;
                foreach (var actorLine in selectedActor)
                {
                    if (actorLine == idActor)
                    {
                        availabilityActor = true;
                    }
                }

                if (!availabilityActor)
                {
                    selectedActor.Add(idActor);
                    MovieActor.Text += "|" + surnameActor + " " + nameActor + " " + patronymicActor + " " + nicknameActor + "|";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddGenre_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TransmittedData.idSelectedGenre = -1;
                AddOrEditGenre addOrEditGenre = new AddOrEditGenre();
                addOrEditGenre.ShowDialog();
                LoadGridData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditGenre_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MovieGenreGrid.SelectedIndex >= 0)
                {
                    DataGridRow row = (DataGridRow)MovieGenreGrid.ItemContainerGenerator.ContainerFromIndex(MovieGenreGrid.SelectedIndex) as DataGridRow;
                    DataGridCell cell = MovieGenreGrid.Columns[0].GetCellContent(row).Parent as DataGridCell;
                    TransmittedData.idSelectedGenre = Convert.ToInt32(((TextBlock)cell.Content).Text);

                    AddOrEditGenre addOrEditGenre = new AddOrEditGenre();
                    addOrEditGenre.ShowDialog();
                    LoadGridData();
                }
                else
                {
                    MessageBox.Show("Выбирите строку для редактирования", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RemoveGenre_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MovieGenreGrid.SelectedIndex >= 0)
                {
                    if (MessageBox.Show("Вы действительно хотите удалить выбранный жанр?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        using (var dataBase = new CinemaEntities())
                        {
                            DataGridRow row = (DataGridRow)MovieGenreGrid.ItemContainerGenerator.ContainerFromIndex(MovieGenreGrid.SelectedIndex) as DataGridRow;
                            DataGridCell cell = MovieGenreGrid.Columns[0].GetCellContent(row).Parent as DataGridCell;
                            int selectedGenre = Convert.ToInt32(((TextBlock)cell.Content).Text);
                            var removeGenre = dataBase.Genre.Where(w => w.IDGenre == selectedGenre).FirstOrDefault();
                            var removeMovieGenre = dataBase.MovieGenre.Where(w => w.IDGenre == selectedGenre).ToList();

                            if (removeMovieGenre.Count != 0)
                            {
                                if (MessageBox.Show("Удаляемый жанр используеться в фильмах. Вы действительно хотите его удалить?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                                {
                                    dataBase.MovieGenre.RemoveRange(removeMovieGenre);
                                    dataBase.Genre.Remove(removeGenre);

                                    dataBase.SaveChanges();

                                    MessageBox.Show("Жанр был удален", "Готово", MessageBoxButton.OK, MessageBoxImage.Information);
                                    LoadGridData();
                                }
                                else
                                {
                                    return;
                                }
                            }
                            else
                            {
                                dataBase.Genre.Remove(removeGenre);

                                dataBase.SaveChanges();

                                MessageBox.Show("Жанр был удален", "Готово", MessageBoxButton.OK, MessageBoxImage.Information);
                                LoadGridData();
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Выбирите строку для редактирования", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddActor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TransmittedData.idSelectedActor = -1;
                AddOrEditActor addOrEditActor = new AddOrEditActor();
                addOrEditActor.ShowDialog();
                LoadGridData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditActor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MovieActorGrid.SelectedIndex >= 0)
                {
                    DataGridRow row = (DataGridRow)MovieActorGrid.ItemContainerGenerator.ContainerFromIndex(MovieActorGrid.SelectedIndex) as DataGridRow;
                    DataGridCell cell = MovieActorGrid.Columns[0].GetCellContent(row).Parent as DataGridCell;
                    TransmittedData.idSelectedActor = Convert.ToInt32(((TextBlock)cell.Content).Text);

                    AddOrEditActor addOrEditActor = new AddOrEditActor();
                    addOrEditActor.ShowDialog();
                    LoadGridData();
                }
                else
                {
                    MessageBox.Show("Выбирите строку для редактирования", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RemoveActor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MovieActorGrid.SelectedIndex >= 0)
                {
                    if (MessageBox.Show("Вы действительно хотите удалить выбранный жанр?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        using (var dataBase = new CinemaEntities())
                        {
                            DataGridRow row = (DataGridRow)MovieActorGrid.ItemContainerGenerator.ContainerFromIndex(MovieActorGrid.SelectedIndex) as DataGridRow;
                            DataGridCell cell = MovieActorGrid.Columns[0].GetCellContent(row).Parent as DataGridCell;
                            int selectedActor = Convert.ToInt32(((TextBlock)cell.Content).Text);
                            var removeActor = dataBase.Actor.Where(w => w.IDActor == selectedActor).FirstOrDefault();
                            var removeActorInMovie = dataBase.ActorsInMovies.Where(w => w.IDActor == selectedActor).ToList();

                            if (removeActorInMovie.Count != 0)
                            {
                                if (MessageBox.Show("Удаляемый актер присутствует в других фильмах. Вы действительно хотите его удалить?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                                {
                                    dataBase.ActorsInMovies.RemoveRange(removeActorInMovie);
                                    dataBase.Actor.Remove(removeActor);

                                    dataBase.SaveChanges();

                                    MessageBox.Show("Актер был удален", "Готово", MessageBoxButton.OK, MessageBoxImage.Information);
                                    LoadGridData();
                                }
                                else
                                {
                                    return;
                                }
                            }
                            else
                            {
                                dataBase.Actor.Remove(removeActor);

                                dataBase.SaveChanges();

                                MessageBox.Show("Жанр был удален", "Готово", MessageBoxButton.OK, MessageBoxImage.Information);
                                LoadGridData();
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Выбирите строку для редактирования", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
