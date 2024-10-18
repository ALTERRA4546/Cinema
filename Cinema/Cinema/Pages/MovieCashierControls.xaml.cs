using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Cinema.Authorization;
using static System.Collections.Specialized.BitVector32;

namespace Cinema
{
    /// <summary>
    /// Логика взаимодействия для MovieCashierControls.xaml
    /// </summary>
    public partial class MovieCashierControls : Page
    {
        public MovieCashierControls()
        {
            InitializeComponent();
        }

        public class MovieCashierData
        {
            public int movieCashierID { get; set; }
            public BitmapImage movieCashierCover { get; set; }
            public string movieCashierTitle { get; set; }
            public string movieCashierGenre { get; set; }
            public int movieCashierYearOfPublication { get; set; }
            public double movieCashierTiming { get; set; }
            public string movieCashierAgeRating { get; set; }
            public string movieCashierCountry { get; set; }
            public string movieCashierDescription { get; set; }
            public string movieCashierActors { get; set; }
        }

        List<MovieCashierData> moviesCashierDataList = new List<MovieCashierData>();

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MovieCashierList.ItemsSource = moviesCashierDataList;

            LoadData(null);
        }

        // Загрузка данных из базы данных
        private void LoadData(string findLine)
        {
            using (var dataBase = new CinemaEntities())
            {
                moviesCashierDataList.Clear();

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
                                 actors in dataBase.Actor on actorsInMovie.IDActor equals actors.IDActors into actorsGroup
                                 from actors in actorsGroup.DefaultIfEmpty()
                                 join
                                 session in dataBase.Session on movie.IDMovie equals session.IDMovie into sessionGroup
                                 from session in sessionGroup.DefaultIfEmpty()
                                 where ((findLine == null || movie.Title.Contains(findLine) || genere.Title.Contains(findLine) || movie.YearOfPublication.ToString().Contains(findLine) || movie.Description.Contains(findLine) || country.Title.Contains(findLine) || actors.Surname.Contains(findLine) || actors.Name.Contains(findLine) || actors.Patronymic.Contains(findLine) || actors.Nickname.Contains(findLine)) && (session.DateAndTimeSession >= DateTime.Now))
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

                var groupedMovieCashierData = movieData.GroupBy(m => m.IDMovie)
                    .Select(g => new
                    {
                        g.Key,
                        genres = g.Select(m => m.movieGenere).Distinct(),
                        actors = g.Select(m => m.movieActor).Distinct()
                    }).ToList();

                var result = groupedMovieCashierData.Select(g =>
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
                    MovieCashierData movieDataClass = new MovieCashierData();

                    movieDataClass.movieCashierID = movieLine.IDMovie;

                    if (movieLine.Cover != null)
                    {
                        BitmapImage cover = new BitmapImage();

                        cover.BeginInit();
                        cover.StreamSource = new MemoryStream(movieLine.Cover);
                        cover.EndInit();

                        movieDataClass.movieCashierCover = cover;
                    }
                    else
                    {
                        movieDataClass.movieCashierCover = new BitmapImage(new Uri("pack://application:,,,/Resource/NoImage.png", UriKind.Absolute));
                    }

                    movieDataClass.movieCashierTitle = movieLine.movieTitle;

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
                    movieDataClass.movieCashierGenre = movieGenereTemp;

                    movieDataClass.movieCashierYearOfPublication = movieLine.YearOfPublication;
                    movieDataClass.movieCashierTiming = movieLine.Timing;
                    movieDataClass.movieCashierAgeRating = movieLine.AgeRating + "+";
                    movieDataClass.movieCashierDescription = movieLine.Description;
                    movieDataClass.movieCashierCountry = movieLine.movieCountry;

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
                    movieDataClass.movieCashierActors = movieActorTemp;

                    moviesCashierDataList.Add(movieDataClass);
                }

                var fullMovieCashierData = (from movie in dataBase.Movie
                                            join
                                            session in dataBase.Session on movie.IDMovie equals session.IDMovie into sessionGroup
                                            from session in sessionGroup.DefaultIfEmpty()
                                            where(session.DateAndTimeSession >= DateTime.Now)
                                            select movie).ToList();

                FindCounterData.Text = result.Count() + "/" + fullMovieCashierData.Count();

                MovieCashierList.Items.Refresh();
            }
        }

        // Изменение размера ListView
        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            MovieCodeGrid.Width = 0;
            ImageGrid.Width = ActualWidth - ActualWidth * 0.8;
            MovieInfoGrid.Width = ActualWidth - ActualWidth * 0.7;
            DescriptionGrid.Width = ActualWidth - ActualWidth * 0.5;
            MovieCashierList.Height = ActualHeight - 25;
        }

        // Поиск фильмов
        private void FindData_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadData(FindData.Text);
        }

        private void MovieCashierList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedCashierMovie = MovieCashierList.SelectedItem as MovieCashierData;
            if (selectedCashierMovie != null)
            {
                TransmittedData.idSelectedCashierMovie = selectedCashierMovie.movieCashierID;

                CashierPanel cashierPanel = Window.GetWindow(this) as CashierPanel;
                cashierPanel.SessionPageOpen();
            }
        }
    }
}
