using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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
using static Cinema.Authorization;

namespace Cinema
{
    /// <summary>
    /// Логика взаимодействия для MovieControls.xaml
    /// </summary>
    public partial class MovieControls : Page
    {
        public MovieControls()
        {
            InitializeComponent();
        }

        public class MovieData
        { 
            public int movieID {  get; set; }
            public BitmapImage movieCover { get; set; }
            public string movieTitle { get; set; }
            public string movieGenre {  get; set; }
            public int movieYearOfPublication { get; set; }
            public double movieTiming { get; set; }
            public string movieAgeRating { get; set; }
            public string movieCountry { get; set; }
            public string movieDescription { get; set; }
            public string movieActors { get; set; }
        }

        List<MovieData> moviesDataList = new List<MovieData>();

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MovieList.ItemsSource = moviesDataList;

            LoadData(null);
        }

        // Загрузка данных из базы данных
        private void LoadData(string findLine)
        {
            using (var dataBase = new CinemaEntities())
            {
                moviesDataList.Clear();

                var movieData = (from movie in dataBase.Movie
                                 join
                                 movieGenre in dataBase.MovieGenre on movie.IDMovie equals movieGenre.IDMovie into movieGenereGroup
                                 from movieGenere in movieGenereGroup.DefaultIfEmpty()
                                 join
                                 genre in dataBase.Genre on movieGenere.IDGenre equals genre.IDGenre into genereGroup
                                 from genere in genereGroup.DefaultIfEmpty()
                                 join
                                 country in dataBase.Country on movie.IDCountry equals country.IDCountry into countryGroup
                                 from country in countryGroup.DefaultIfEmpty()
                                 join
                                 actorsInMovies in dataBase.ActorsInMovies on movie.IDMovie equals actorsInMovies.IDMovie into actorsInMoviesGroup
                                 from actorsInMovie in actorsInMoviesGroup.DefaultIfEmpty()
                                 join
                                 actors in dataBase.Actor on actorsInMovie.IDActor equals actors.IDActor into actorsGroup
                                 from actors in actorsGroup.DefaultIfEmpty()
                                 where (findLine == null || movie.Title.Contains(findLine) || genere.Title.Contains(findLine) || movie.YearOfPublication.ToString().Contains(findLine) || movie.Description.Contains(findLine) || country.Title.Contains(findLine) || actors.Surname.Contains(findLine) || actors.Name.Contains(findLine) || actors.Patronymic.Contains(findLine) || actors.Nickname.Contains(findLine))
                                 select new
                                 { 
                                    movie.IDMovie,
                                    movie.Cover,
                                    movieTitle = movie.Title,
                                    movieGenere = genere.Title,
                                    movie.YearOfPublication,
                                    movie.Timing,
                                    movie.AgeRating,
                                    movie.Description,
                                    movieCountry = country.Title,
                                    movieActor = actors.Surname + " " + actors.Name + " " + actors.Patronymic + " " + actors.Nickname
                                 }
                                 ).ToList();

                var groupedMovieData = movieData.GroupBy(m => m.IDMovie)
                    .Select(g => new
                    {
                        g.Key,
                        genres = g.Select(m => m.movieGenere).Distinct(),
                        actors = g.Select(m => m.movieActor).Distinct()
                    }).ToList();

                var result = groupedMovieData.Select(g =>
                {
                    var movie = movieData.FirstOrDefault(m => m.IDMovie == g.Key);
                    return new
                    {
                        movie.IDMovie,
                        movie.Cover,
                        movie.movieTitle,
                        movieGenres = g.genres,
                        movie.YearOfPublication,
                        movie.Timing,
                        movie.AgeRating,
                        movie.Description,
                        movie.movieCountry,
                        movieActors = g.actors
                    };
                }).ToList();

                foreach (var movieLine in result)
                { 
                    MovieData movieDataClass = new MovieData();

                    movieDataClass.movieID = movieLine.IDMovie;

                    if (movieLine.Cover != null)
                    {
                        BitmapImage cover = new BitmapImage();

                        cover.BeginInit();
                        cover.StreamSource = new MemoryStream(movieLine.Cover);
                        cover.EndInit();

                        movieDataClass.movieCover = cover;
                    }
                    else
                    {
                        movieDataClass.movieCover = new BitmapImage(new Uri("pack://application:,,,/Resource/NoImage.png", UriKind.Absolute));
                    }

                    movieDataClass.movieTitle = movieLine.movieTitle;

                    string movieGenereTemp = "";
                    int movieGenresCounterTemp = 1;
                    foreach (var genere in movieLine.movieGenres)
                    {
                        if (movieGenresCounterTemp != movieLine.movieGenres.Count())
                            movieGenereTemp += genere + ", ";
                        else
                            movieGenereTemp += genere;

                        movieGenresCounterTemp++;
                    }
                    movieDataClass.movieGenre = movieGenereTemp;

                    movieDataClass.movieYearOfPublication = movieLine.YearOfPublication;
                    movieDataClass.movieTiming = movieLine.Timing;
                    movieDataClass.movieAgeRating = movieLine.AgeRating + "+";
                    movieDataClass.movieDescription = movieLine.Description;
                    movieDataClass.movieCountry = movieLine.movieCountry;

                    string movieActorTemp = "";
                    int movieActorCounterTemp = 1;
                    foreach (var actor in movieLine.movieActors)
                    {
                        if (movieActorCounterTemp != movieLine.movieActors.Count())
                        {
                            movieActorTemp += actor + ", ";
                        }
                        else
                        {
                            movieActorTemp += actor;
                        }

                        movieActorCounterTemp++;
                    }
                    movieDataClass.movieActors = movieActorTemp;

                    moviesDataList.Add(movieDataClass);
                }

                var fullMovieData = dataBase.Movie.ToList();
                FindCounterData.Text = result.Count() + "/" + fullMovieData.Count();

                MovieList.Items.Refresh();
            }
        }

        // Изменение размера ListView
        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            MovieCodeGrid.Width = 0;
            ImageGrid.Width = ActualWidth - ActualWidth * 0.8;
            MovieInfoGrid.Width = ActualWidth - ActualWidth * 0.7;
            DescriptionGrid.Width = ActualWidth - ActualWidth * 0.5;
            MovieList.Height = ActualHeight - 35;
        }

        // Открытие окна редактирования
        private void MovieList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedMovie = MovieList.SelectedItem as MovieData;
            if (selectedMovie != null)
            {
                TransmittedData.idSelectedMovie = selectedMovie.movieID;

                AddOrEditMovie addOrEditMovie = new AddOrEditMovie();
                addOrEditMovie.ShowDialog();

                LoadData(FindData.Text);
            }
        }

        // Поиск фильмов
        private void FindData_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadData(FindData.Text);
        }

        // Открытие окна добавления данных
        private void AddMovie_Click(object sender, RoutedEventArgs e)
        {
            TransmittedData.idSelectedMovie = -1;

            AddOrEditMovie addOrEditMovie = new AddOrEditMovie();
            addOrEditMovie.ShowDialog();

            LoadData(FindData.Text);
        }

        // Удаление фильма
        private void RemoveMovie_Click(object sender, RoutedEventArgs e)
        {
            var selectedMovie = MovieList.SelectedItem as MovieData;
            if (selectedMovie != null)
            {
                if (MessageBox.Show("Вы действительно хотите удалить данный фильм?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    using (var dataBase = new CinemaEntities())
                    {
                        var removeMovie = dataBase.Movie.Where(w => w.IDMovie == selectedMovie.movieID).FirstOrDefault();
                        var removeMovieGenere = dataBase.MovieGenre.Where(w => w.IDMovie == selectedMovie.movieID).ToList();
                        var removeActorsInMovies = dataBase.ActorsInMovies.Where(w => w.IDMovie == selectedMovie.movieID).ToList();
                        var removeTicket = (from session in dataBase.Session
                                            join
                                            ticket in dataBase.Ticket on session.IDSession equals ticket.IDSession
                                            where (session.IDMovie == selectedMovie.movieID)
                                            select ticket).ToList();
                        var removeSession = dataBase.Session.Where(w => w.IDMovie == selectedMovie.movieID).ToList();

                        if (removeSession.Count > 0)
                        {
                            if(MessageBox.Show("Удаляемый фильм присутсвует в сесии. Вы действительно хотите его удалить?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                                return;
                        }

                        dataBase.Ticket.RemoveRange(removeTicket);
                        dataBase.Session.RemoveRange(removeSession);
                        dataBase.ActorsInMovies.RemoveRange(removeActorsInMovies);
                        dataBase.MovieGenre.RemoveRange(removeMovieGenere);
                        dataBase.Movie.Remove(removeMovie);

                        dataBase.SaveChanges();
                        MessageBox.Show("Данные были удаленны", "Готово", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                    LoadData(FindData.Text);
                }
            }
        }
    }
}
