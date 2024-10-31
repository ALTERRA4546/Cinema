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
            public string sessionCashierDateSessionStart { get; set; }
            public string sessionCashierTimeSessionStart { get; set; }
            public string sessionCashierTicketPrice { get; set; }
            public string sessionSeatsInHall { get; set; }
        }

        List<SessionCashierData> sessionCashierDataList = new List<SessionCashierData>();

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                SessionCashierList.ItemsSource = sessionCashierDataList;

                LoadData(null, FindSessionData);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadData(string findLine, DatePicker findDate)
        {
            try
            {
                using (var dataBase = new CinemaEntities())
                {
                    sessionCashierDataList.Clear();

                    DatePicker endDate = new DatePicker();
                    endDate.SelectedDate = null;

                    if (findDate.SelectedDate != null)
                    {
                        endDate.SelectedDate = findDate.SelectedDate.Value.AddDays(1);
                    }
                    else
                    {
                        endDate.SelectedDate = null;
                    }

                    var sessionCashierData = (from session in dataBase.Session
                                              join
                                              movie in dataBase.Movie on session.IDMovie equals movie.IDMovie into movieGroup
                                              from movie in movieGroup.DefaultIfEmpty()
                                              join
                                              movieGenre in dataBase.MovieGenre on movie.IDMovie equals movieGenre.IDMovie into movieGenereGroup
                                              from movieGenre in movieGenereGroup.DefaultIfEmpty()
                                              join
                                              actorsInMovies in dataBase.ActorsInMovies on movie.IDMovie equals actorsInMovies.IDMovie into actorsInMoviesGroup
                                              from actorsInMovies in actorsInMoviesGroup.DefaultIfEmpty()
                                              join
                                              actors in dataBase.Actor on actorsInMovies.IDActor equals actors.IDActor into actorsGroup
                                              from actors in actorsGroup.DefaultIfEmpty()
                                              join
                                              genre in dataBase.Genre on movieGenre.IDGenre equals genre.IDGenre into genreGroup
                                              from genre in genreGroup.DefaultIfEmpty()
                                              join
                                              country in dataBase.Country on movie.IDCountry equals country.IDCountry into countryGroup
                                              from country in countryGroup.DefaultIfEmpty()
                                              join
                                              ticket in dataBase.Ticket on session.IDSession equals ticket.IDSession into ticketGroup
                                              from ticket in ticketGroup.DefaultIfEmpty()
                                              where ((findLine == null || movie.Title.Contains(findLine) || genre.Title.Contains(findLine) || movie.YearOfPublication.ToString().Contains(findLine) || movie.Description.Contains(findLine) || country.Title.Contains(findLine)) && ((findDate.SelectedDate == null && endDate.SelectedDate == null) || (session.DateAndTimeSession >= findDate.SelectedDate.Value && session.DateAndTimeSession <= endDate.SelectedDate.Value)) && (session.DateAndTimeSession >= DateTime.Now) && (session.IDMovie == TransmittedData.idSelectedCashierMovie))
                                              select new
                                              {
                                                  session.IDSession,
                                                  movie.Cover,
                                                  movieTitle = movie.Title,
                                                  movieGenere = genre.Title,
                                                  movie.YearOfPublication,
                                                  movie.Timing,
                                                  movie.AgeRating,
                                                  movieCountry = country.Title,
                                                  movieActors = actors.Surname + " " + actors.Name + " " + actors.Patronymic + " " + actors.Nickname,
                                                  session.DateAndTimeSession,
                                                  TicketID = ticket == null ? 0 : ticket.IDTicket,
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
                        sessionDataClass.sessionCashierDateSessionStart = sessionLine.DateAndTimeSession.ToString().Split(' ')[0];
                        sessionDataClass.sessionCashierTimeSessionStart = sessionLine.DateAndTimeSession.ToString().Split(' ')[1];
                        sessionDataClass.sessionCashierTicketPrice = sessionLine.TicketPrice.ToString().Remove(sessionLine.TicketPrice.ToString().Length - 2, 2) + " руб.";

                        var seatsHallData = dataBase.Settings.OrderByDescending(o => o.IDSettings).FirstOrDefault();
                        if (seatsHallData != null)
                        {
                            int hidePlaceCount = seatsHallData.HiddenPlaces.Split('|').Count();

                            int purchasedTickets = sessionLine.TicketCount / ((movieGenresCounterTemp - 1) * (movieActorCounterTemp - 1));
                            int totalTickets = (seatsHallData.RowHall * seatsHallData.PlaceHall) - hidePlaceCount;

                            if (purchasedTickets == 1)
                            {
                                var ticketCount = dataBase.Ticket.Where(w => w.IDSession == sessionLine.IDSession).Count();
                                sessionDataClass.sessionSeatsInHall = ticketCount + "/" + totalTickets;
                            }
                            else
                            {
                                sessionDataClass.sessionSeatsInHall = purchasedTickets + "/" + totalTickets;
                            }
                        }
                        else
                        {
                            sessionDataClass.sessionSeatsInHall = "Зал не размечен";
                        }

                        sessionCashierDataList.Add(sessionDataClass);
                    }
                    var fullSessionCashierData = dataBase.Session.Where(w => w.DateAndTimeSession >= DateTime.Now && w.IDMovie == TransmittedData.idSelectedCashierMovie).ToList();
                    FindCounterData.Text = result.Count() + "/" + fullSessionCashierData.Count();
                    SessionCashierList.Items.Refresh();
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
                SessionCodeGrid.Width = 0;
                SessionCoverGrid.Width = ActualWidth - ActualWidth * 0.8;
                SessionInfoGird.Width = ActualWidth - ActualWidth * 0.7;
                SessionDateTimeGrid.Width = ActualWidth - ActualWidth * 0.7;
                SessionPriceGrid.Width = ActualWidth - ActualWidth * 0.8;
                SessionCashierList.Height = ActualHeight - 35;
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
                if (FindDateChecker.IsChecked == false)
                {
                    FindSessionData.SelectedDate = null;
                }
                LoadData(FindData.Text, FindSessionData);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FindDateChecker_Click(object sender, RoutedEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FindSessionData_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (FindDateChecker.IsChecked == false)
                {
                    FindSessionData.SelectedDate = null;
                }
                LoadData(FindData.Text, FindSessionData);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SessionCashierList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var selectedCashierSession = SessionCashierList.SelectedItem as SessionCashierData;
                if (selectedCashierSession != null)
                {
                    TransmittedData.idSelectedCashierSession = selectedCashierSession.sessionCashierID;

                    CashierPanel cashierPanel = Window.GetWindow(this) as CashierPanel;
                    cashierPanel.PlacesPageOpen();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
