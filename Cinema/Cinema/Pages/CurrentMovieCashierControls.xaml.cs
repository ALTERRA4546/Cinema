using System;
using System.Collections.Generic;
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

namespace Cinema.Pages
{
    /// <summary>
    /// Логика взаимодействия для CurrentMovieCashierControls.xaml
    /// </summary>
    public partial class CurrentMovieCashierControls : Page
    {
        public CurrentMovieCashierControls()
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
            try
            {
                MovieCashierList.ItemsSource = moviesCashierDataList;

                LoadData(null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadData(string findLine)
        {
            try
            {
                using (var dataBase = new CinemaEntities())
                {
                    moviesCashierDataList.Clear();

                    DateTime dateTimeToday = DateTime.Now;
                    DateTime dateTimeTomorrow = DateTime.Now.AddDays(1).Date;

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
                                     join
                                     session in dataBase.Session on movie.IDMovie equals session.IDMovie into sessionGroup
                                     from session in sessionGroup.DefaultIfEmpty()
                                     where ((findLine == null || movie.Title.Contains(findLine) || genere.Title.Contains(findLine) || movie.YearOfPublication.ToString().Contains(findLine) || movie.Description.Contains(findLine) || country.Title.Contains(findLine) || actors.Surname.Contains(findLine) || actors.Name.Contains(findLine) || actors.Patronymic.Contains(findLine) || actors.Nickname.Contains(findLine)) && (session.DateAndTimeSession >= dateTimeToday && session.DateAndTimeSession <= dateTimeTomorrow))
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
                                                where (session.DateAndTimeSession >= dateTimeToday && session.DateAndTimeSession <= dateTimeTomorrow)
                                                group movie by movie.Title into groupedMovies
                                                select new
                                                {
                                                    MovieTitle = groupedMovies.Key,
                                                    TotalSessions = groupedMovies.Count()
                                                }).ToList();

                    FindCounterData.Text = result.Count() + "/" + fullMovieCashierData.Count();

                    MovieCashierList.Items.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            try
            {
                MovieCodeGrid.Width = 0;
                ImageGrid.Width = ActualWidth - ActualWidth * 0.8;
                MovieInfoGrid.Width = ActualWidth - ActualWidth * 0.7;
                DescriptionGrid.Width = ActualWidth - ActualWidth * 0.5;
                MovieCashierList.Height = ActualHeight - 35;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FindData_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                LoadData(FindData.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MovieCashierList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var selectedCashierMovie = MovieCashierList.SelectedItem as MovieCashierData;
                if (selectedCashierMovie != null)
                {
                    TransmittedData.idSelectedCashierMovie = selectedCashierMovie.movieCashierID;

                    CashierPanel cashierPanel = Window.GetWindow(this) as CashierPanel;
                    cashierPanel.SessionPageOpen();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
