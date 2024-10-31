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
    /// Логика взаимодействия для SessionControls.xaml
    /// </summary>
    public partial class SessionControls : Page
    {
        public SessionControls()
        {
            InitializeComponent();
        }

        public class SessionData
        {
            public int sessionID { get; set; }
            public BitmapImage sessionMovieCover { get; set; }
            public string sessionMovieTitle { get; set; }
            public string sessionMovieGenre { get; set; }
            public int sessionMovieYearOfPublication { get; set; }
            public double sessionMovieTiming { get; set; }
            public string sessionMovieAgeRating { get; set; }
            public string sessionMovieCountry { get; set; }
            public string sessionActors { get; set; }
            public string sessionDateSessionStart { get; set; }
            public string sessionTimeSessionStart { get; set; }
            public string sessionTicketPrice { get; set; }
        }

        List<SessionData> sessionDataList = new List<SessionData>();

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                SessionList.ItemsSource = sessionDataList;

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
                    sessionDataList.Clear();

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

                    var sessionData = (from session in dataBase.Session
                                       join
                                       movie in dataBase.Movie on session.IDMovie equals movie.IDMovie into movieGroup
                                       from movie in movieGroup.DefaultIfEmpty()
                                       join
                                       actorsInMovies in dataBase.ActorsInMovies on movie.IDMovie equals actorsInMovies.IDMovie into actorsInMoviesGroup
                                       from actorsInMovie in actorsInMoviesGroup.DefaultIfEmpty()
                                       join
                                       actors in dataBase.Actor on actorsInMovie.IDActor equals actors.IDActor into actorsGroup
                                       from actors in actorsGroup.DefaultIfEmpty()
                                       join
                                       movieGenre in dataBase.MovieGenre on movie.IDMovie equals movieGenre.IDMovie into movieGenereGroup
                                       from movieGenere in movieGenereGroup.DefaultIfEmpty()
                                       join
                                       genre in dataBase.Genre on movieGenere.IDGenre equals genre.IDGenre into genereGroup
                                       from genere in genereGroup.DefaultIfEmpty()
                                       join
                                       country in dataBase.Country on movie.IDCountry equals country.IDCountry into countryGroup
                                       from country in countryGroup.DefaultIfEmpty()
                                       where ((findLine == null || movie.Title.Contains(findLine) || genere.Title.Contains(findLine) || movie.YearOfPublication.ToString().Contains(findLine) || movie.Description.Contains(findLine) || country.Title.Contains(findLine)) && ((findDate.SelectedDate == null && endDate.SelectedDate == null) || (session.DateAndTimeSession >= findDate.SelectedDate.Value && session.DateAndTimeSession <= endDate.SelectedDate.Value)))
                                       select new
                                       {
                                           session.IDSession,
                                           movie.Cover,
                                           movieTitle = movie.Title,
                                           movieGenere = genere.Title,
                                           movieActors = actors.Surname + " " + actors.Name + " " + actors.Patronymic + " " + actors.Nickname,
                                           movie.YearOfPublication,
                                           movie.Timing,
                                           movie.AgeRating,
                                           movieCountry = country.Title,
                                           session.DateAndTimeSession,
                                           session.TicketPrice
                                       }).ToList();

                    var groupedSessionData = sessionData.GroupBy(m => m.IDSession)
                        .Select(g => new
                        {
                            g.Key,
                            genres = g.Select(m => m.movieGenere).Distinct(),
                            actors = g.Select(m => m.movieActors).Distinct(),
                        }).ToList();

                    var result = groupedSessionData.Select(g =>
                    {
                        var session = sessionData.FirstOrDefault(m => m.IDSession == g.Key);
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
                        };
                    }).ToList();

                    foreach (var sessionLine in result)
                    {
                        SessionData sessionDataClass = new SessionData();

                        sessionDataClass.sessionID = sessionLine.IDSession;

                        if (sessionLine.Cover != null)
                        {
                            BitmapImage cover = new BitmapImage();

                            cover.BeginInit();
                            cover.StreamSource = new MemoryStream(sessionLine.Cover);
                            cover.EndInit();

                            sessionDataClass.sessionMovieCover = cover;
                        }
                        else
                        {
                            sessionDataClass.sessionMovieCover = new BitmapImage(new Uri("pack://application:,,,/Resource/NoImage.png", UriKind.Absolute));
                        }

                        sessionDataClass.sessionMovieTitle = sessionLine.movieTitle;

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
                        sessionDataClass.sessionMovieGenre = sessionMovieGenereTemp;

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
                        sessionDataClass.sessionActors = movieActorTemp;

                        sessionDataClass.sessionMovieYearOfPublication = sessionLine.YearOfPublication;
                        sessionDataClass.sessionMovieTiming = sessionLine.Timing;
                        sessionDataClass.sessionMovieAgeRating = sessionLine.AgeRating + "+";
                        sessionDataClass.sessionMovieCountry = sessionLine.movieCountry;
                        sessionDataClass.sessionDateSessionStart = sessionLine.DateAndTimeSession.ToString().Split(' ')[0];
                        sessionDataClass.sessionTimeSessionStart = sessionLine.DateAndTimeSession.ToString().Split(' ')[1];
                        sessionDataClass.sessionTicketPrice = sessionLine.TicketPrice.ToString().Remove(sessionLine.TicketPrice.ToString().Length - 2, 2) + " руб.";

                        sessionDataList.Add(sessionDataClass);
                    }

                    var fullSessionData = dataBase.Session.ToList();
                    FindCounterData.Text = result.Count() + "/" + fullSessionData.Count();

                    SessionList.Items.Refresh();
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
                SessionInfoGird.Width = ActualWidth - ActualWidth * 0.6;
                SessionInformationSeasonGrid.Width = ActualWidth - ActualWidth * 0.8;
                SessionPriceGrid.Width = ActualWidth - ActualWidth * 0.8;
                SessionList.Height = ActualHeight - 35;
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

        private void SessionList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var selectedSession = SessionList.SelectedItem as SessionData;
                if (selectedSession != null)
                {
                    TransmittedData.idSelectedSession = selectedSession.sessionID;

                    AddOrEditSession addOrEditSession = new AddOrEditSession();
                    addOrEditSession.ShowDialog();

                    if (FindDateChecker.IsChecked == false)
                    {
                        FindSessionData.SelectedDate = null;
                    }
                    LoadData(FindData.Text, FindSessionData);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddSession_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TransmittedData.idSelectedSession = -1;

                AddOrEditSession addOrEditSession = new AddOrEditSession();
                addOrEditSession.ShowDialog();

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

        private void RemoveSession_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedSession = SessionList.SelectedItem as SessionData;
                if (selectedSession != null)
                {
                    if (MessageBox.Show("Вы действительно хотите удалить данный сеанс?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        using (var dataBase = new CinemaEntities())
                        {
                            var removeSession = dataBase.Session.Where(w => w.IDSession == selectedSession.sessionID).FirstOrDefault();
                            var removeTicket = dataBase.Ticket.Where(w => w.IDSession == selectedSession.sessionID).ToList();

                            if (removeTicket.Count > 0)
                            {
                                if (MessageBox.Show("Удаляемый сеанс присутсвует в билетах. Вы действительно хотите его удалить?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                                    return;
                            }

                            dataBase.Ticket.RemoveRange(removeTicket);
                            dataBase.Session.Remove(removeSession);

                            dataBase.SaveChanges();
                            MessageBox.Show("Данные были удаленны", "Готово", MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                        if (FindDateChecker.IsChecked == false)
                        {
                            FindSessionData.SelectedDate = null;
                        }
                        LoadData(FindData.Text, FindSessionData);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
