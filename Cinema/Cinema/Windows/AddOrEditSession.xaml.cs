using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using static Cinema.Authorization;

namespace Cinema
{
    /// <summary>
    /// Логика взаимодействия для AddOrEditSession.xaml
    /// </summary>
    public partial class AddOrEditSession : Window
    {
        public AddOrEditSession()
        {
            InitializeComponent();
        }

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
                    var movieData = dataBase.Movie.ToList();
                    List<string> movieDataList = new List<string>();

                    foreach (var movieLine in movieData)
                    {
                        movieDataList.Add(movieLine.IDMovie + "|" + movieLine.Title);
                    }

                    SessionMovie.ItemsSource = movieDataList;

                    if (TransmittedData.idSelectedSession != -1)
                    {
                        var sessionData = (from session in dataBase.Session
                                           join
                                           movie in dataBase.Movie on session.IDMovie equals movie.IDMovie into movieGroup
                                           from movie in movieGroup.DefaultIfEmpty()
                                           where (session.IDSession == TransmittedData.idSelectedSession)
                                           select new
                                           {
                                               movie.IDMovie,
                                               movie.Title,
                                               session.DateAndTimeSession,
                                               session.TicketPrice
                                           }).FirstOrDefault();

                        SessionMovie.SelectedItem = sessionData.IDMovie + "|" + sessionData.Title;
                        SessionDate.SelectedDate = sessionData.DateAndTimeSession.Date;
                        SessionTime.Text = sessionData.DateAndTimeSession.TimeOfDay.ToString();
                        SessionTicketPrice.Text = sessionData.TicketPrice.ToString();
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
                string timePattern = @"^(?:(?:[01]\d|2[0-3]):[0-5]\d:[0-5]\d)$";
                if (SessionMovie.SelectedIndex < 0 || SessionDate.SelectedDate == null || SessionTicketPrice.Text == null || !decimal.TryParse(SessionTicketPrice.Text, out _))
                {
                    MessageBox.Show("Данные были заполнены неверно", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!Regex.IsMatch(SessionTime.Text, timePattern))
                {
                    MessageBox.Show("Время указываеться в формате 00:00:00", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                using (var dataBase = new CinemaEntities())
                {
                    if (TransmittedData.idSelectedSession != -1)
                    {
                        var sessionData = dataBase.Session.Where(w => w.IDSession == TransmittedData.idSelectedSession).FirstOrDefault();

                        string[] sessionMovieTemp = SessionMovie.SelectedItem.ToString().Split('|');
                        string[] sessionDateTemp = SessionDate.SelectedDate.Value.ToString().Split(' ');

                        sessionData.IDMovie = Convert.ToInt32(sessionMovieTemp[0]);
                        sessionData.DateAndTimeSession = Convert.ToDateTime(sessionDateTemp[0] + " " + SessionTime.Text);
                        sessionData.TicketPrice = Convert.ToDecimal(SessionTicketPrice.Text);

                        dataBase.SaveChanges();
                    }
                    else
                    {
                        var newSessionData = new Session();

                        string[] sessionMovieTemp = SessionMovie.SelectedItem.ToString().Split('|');
                        string[] sessionDateTemp = SessionDate.SelectedDate.Value.ToString().Split(' ');

                        newSessionData.IDMovie = Convert.ToInt32(sessionMovieTemp[0]);
                        newSessionData.DateAndTimeSession = Convert.ToDateTime(sessionDateTemp[0] + " " + SessionTime.Text);
                        newSessionData.TicketPrice = Convert.ToDecimal(SessionTicketPrice.Text);

                        dataBase.Session.Add(newSessionData);

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
