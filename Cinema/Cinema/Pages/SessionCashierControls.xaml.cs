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
using static Cinema.SessionCashierControls;
using static System.Collections.Specialized.BitVector32;

namespace Cinema
{
    /// <summary>
    /// Логика взаимодействия для SessionCashierControls.xaml
    /// </summary>
    public partial class SessionCashierControls : Page
    {
        public SessionCashierControls()
        {
            InitializeComponent();
        }

        public class SessionCashierData
        {
            public int sessionCashierID { get; set; }
            public BitmapImage sessionCashierMovieCover { get; set; }
            public string sessionCashierMovieTitle { get; set; }
            public string sessionCashierMovieGenre { get; set; }
            public int sessionCashierMovieYearOfPublication { get; set; }
            public double sessionCashierMovieTiming { get; set; }
            public string sessionCashierMovieAgeRating { get; set; }
            public string sessionCashierMovieCountry { get; set; }
            public string sessionCashierActors { get; set; }
            public DateTime sessionCashierDateAndTimeSessionStart { get; set; }
            public string sessionCashierTicketPrice { get; set; }
            public string sessionSeatsInHall { get; set; }
        }

        List<SessionCashierData> sessionCashierDataList = new List<SessionCashierData>();

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SessionCashierList.ItemsSource = sessionCashierDataList;

            LoadData(null, FindSessionData);
        }

        private void LoadData(string findLine, DatePicker findDate)
        {
            using (var dataBase = new CinemaEntities())
            {
                sessionCashierDataList.Clear();

                var sessionCashierData = (from session in dataBase.Session
                                   join
                                   movie in dataBase.Movie on session.IDMovie equals movie.IDMovie into movieGroup
                                   from movie in movieGroup.DefaultIfEmpty()
                                   join
                                   movieGenre in dataBase.MovieGenre on movie.IDMovie equals movieGenre.IDMovie into movieGenereGroup
                                   from movieGenere in movieGenereGroup.DefaultIfEmpty()
                                   join
                                   actorsInMovies in dataBase.ActorsInMovies on movie.IDMovie equals actorsInMovies.IDMovie into actorsInMoviesGroup
                                   from actorsInMovie in actorsInMoviesGroup.DefaultIfEmpty()
                                   join
                                   actors in dataBase.Actor on actorsInMovie.IDActor equals actors.IDActors into actorsGroup
                                   from actors in actorsGroup.DefaultIfEmpty()
                                   join
                                   genre in dataBase.Genre on movieGenere.IDGenre equals genre.IDGenre into genereGroup
                                   from genere in genereGroup.DefaultIfEmpty()
                                   join
                                   country in dataBase.Country on movie.IDCountry equals country.IDCountry into countryGroup
                                   from country in countryGroup.DefaultIfEmpty()
                                   join
                                   ticket in dataBase.Ticket on session.IDSession equals ticket.IDSession into ticketGroup
                                   from ticket in ticketGroup.DefaultIfEmpty()
                                   where ((findLine == null || movie.Title.Contains(findLine) || genere.Title.Contains(findLine) || movie.YearOfPublication.ToString().Contains(findLine) || movie.Description.Contains(findLine) || country.Title.Contains(findLine)) && (findDate.SelectedDate == null || session.DateAndTimeSession == findDate.SelectedDate.Value) && (session.DateAndTimeSession >= DateTime.Now) && (session.IDMovie == TransmittedData.idSelectedCashierMovie))
                                   select new
                                   {
                                       session.IDSession,
                                       movie.Cover,
                                       movieTitle = movie.Title,
                                       movieGenere = genere.Title,
                                       movie.YearOfPublication,
                                       movie.Timing,
                                       movie.AgeRating,
                                       movieCountry = country.Title,
                                       movieActors = actors.Surname + " " + actors.Name + " " + actors.Patronymic + " " + actors.Nickname,
                                       session.DateAndTimeSession,
                                       ticket.IDTicket,
                                       session.TicketPrice
                                   }).ToList();

                var groupedSessionCashierData = sessionCashierData.GroupBy(m => m.IDSession)
                    .Select(g => new
                    {
                        g.Key,
                        genres = g.Select(m => m.movieGenere).Distinct(),
                        actors = g.Select(m => m.movieActors).Distinct(),
                        TicketCount = g.Count()
                    }).ToList();

                var result = groupedSessionCashierData.Select(g =>
                {
                    var session = sessionCashierData.FirstOrDefault(m => m.IDSession == g.Key);
                    return new
                    {
                        session.IDSession,
                        session.Cover,
                        session.movieTitle,
                        sessionMovieGenres = g.genres,
                        session.YearOfPublication,
                        session.Timing,
                        session.AgeRating,
                        session.movieCountry,
                        session.DateAndTimeSession,
                        session.TicketPrice,
                        movieActors = g.actors,
                        g.TicketCount,
                    };
                }).ToList();

                foreach (var sessionLine in result)
                {
                    SessionCashierData sessionDataClass = new SessionCashierData();

                    sessionDataClass.sessionCashierID = sessionLine.IDSession;

                    if (sessionLine.Cover != null)
                    {
                        BitmapImage cover = new BitmapImage();

                        cover.BeginInit();
                        cover.StreamSource = new MemoryStream(sessionLine.Cover);
                        cover.EndInit();

                        sessionDataClass.sessionCashierMovieCover = cover;
                    }
                    else
                    {
                        sessionDataClass.sessionCashierMovieCover = new BitmapImage(new Uri("pack://application:,,,/Resource/NoImage.png", UriKind.Absolute));
                    }

                    sessionDataClass.sessionCashierMovieTitle = sessionLine.movieTitle;

                    string sessionMovieGenereTemp = "";
                    int movieGenresCounterTemp = 1;
                    foreach (var genere in sessionLine.sessionMovieGenres)
                    {
                        if (movieGenresCounterTemp != sessionLine.sessionMovieGenres.Count())
                            sessionMovieGenereTemp += genere + ", ";
                        else
                            sessionMovieGenereTemp += genere;

                        movieGenresCounterTemp++;
                    }
                    sessionDataClass.sessionCashierMovieGenre = sessionMovieGenereTemp;

                    string movieActorTemp = "";
                    int movieActorCounterTemp = 1;
                    foreach (var actor in sessionLine.movieActors)
                    {
                        if (movieActorCounterTemp != sessionLine.movieActors.Count())
                        {
                            movieActorTemp += actor + ", ";
                        }
                        else
                        {
                            movieActorTemp += actor;
                        }

                        movieActorCounterTemp++;
                    }
                    sessionDataClass.sessionCashierActors = movieActorTemp;

                    sessionDataClass.sessionCashierMovieYearOfPublication = sessionLine.YearOfPublication;
                    sessionDataClass.sessionCashierMovieTiming = sessionLine.Timing;
                    sessionDataClass.sessionCashierMovieAgeRating = sessionLine.AgeRating + "+";
                    sessionDataClass.sessionCashierMovieCountry = sessionLine.movieCountry;
                    sessionDataClass.sessionCashierDateAndTimeSessionStart = sessionLine.DateAndTimeSession;
                    sessionDataClass.sessionCashierTicketPrice = sessionLine.TicketPrice.ToString().Remove(sessionLine.TicketPrice.ToString().Length - 2, 2) + " руб.";
                    sessionDataClass.sessionSeatsInHall = sessionLine.TicketCount/((movieGenresCounterTemp-1) * (movieActorCounterTemp - 1)) + "/120";

                    sessionCashierDataList.Add(sessionDataClass);
                }
                var fullSessionCashierData = dataBase.Session.Where(w=>w.DateAndTimeSession >= DateTime.Now && w.IDMovie == TransmittedData.idSelectedCashierMovie).ToList();
                FindCounterData.Text = result.Count() + "/" + fullSessionCashierData.Count();
                SessionCashierList.Items.Refresh();
            }
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            SessionCodeGrid.Width = 0;
            SessionCoverGrid.Width = ActualWidth - ActualWidth * 0.8;
            SessionInfoGird.Width = ActualWidth - ActualWidth * 0.7;
            SessionDateTimeGrid.Width = ActualWidth - ActualWidth * 0.7;
            SessionPriceGrid.Width = ActualWidth - ActualWidth * 0.8;
            SessionCashierList.Height = ActualHeight - 25;
        }

        private void FindData_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (FindDateChecker.IsChecked == false)
            {
                FindSessionData.SelectedDate = null;
            }
            LoadData(FindData.Text, FindSessionData);
        }

        private void FindDateChecker_Click(object sender, RoutedEventArgs e)
        {
            if (FindDateChecker.IsChecked == false)
            {
                FindSessionData.IsEnabled = false;
                FindSessionData.SelectedDate = null;
            }
            else
            {
                FindSessionData.IsEnabled = true;
                FindSessionData.SelectedDate = DateTime.Now.Date;
            }
            LoadData(FindData.Text, FindSessionData);
        }

        private void FindSessionData_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FindDateChecker.IsChecked == false)
            {
                FindSessionData.SelectedDate = null;
            }
            LoadData(FindData.Text, FindSessionData);
        }

        private void SessionCashierList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedCashierSession = SessionCashierList.SelectedItem as SessionCashierData;
            if (selectedCashierSession != null)
            {
                TransmittedData.idSelectedCashierSession = selectedCashierSession.sessionCashierID;

                CashierPanel cashierPanel = Window.GetWindow(this) as CashierPanel;
                cashierPanel.PlacesPageOpen();
            }
        }
    }
}
