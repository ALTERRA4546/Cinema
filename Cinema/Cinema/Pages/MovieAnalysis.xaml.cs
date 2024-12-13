using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using static Cinema.Authorization;

namespace Cinema
{
    /// <summary>
    /// Логика взаимодействия для MovieAnalysis.xaml
    /// </summary>
    public partial class MovieAnalysis : Page
    {
        public MovieAnalysis()
        {
            InitializeComponent();
        }

        public class SessionDataAnalysis
        {
            public int idSession { get; set; }
            public string dateSession { get; set; }
            public string timeSession { get; set; }
            public string ticketCountSession { get; set; }
            public string ticketPrice { get; set; }
            public string ticketSummarySession { get; set; }
        }

        List<SessionDataAnalysis> sessionDataAnalysesList = new List<SessionDataAnalysis>();

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                SessionList.ItemsSource = sessionDataAnalysesList;

                LoadData(BeginDate, EndDate);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadData(DatePicker beginDate, DatePicker endDate)
        {
            try
            {
                using (var dataBase = new CinemaEntities())
                {
                    sessionDataAnalysesList.Clear();

                    var sessionData = (from session in dataBase.Session
                                       join
                                       ticket in dataBase.Ticket on session.IDSession equals ticket.IDSession into ticketGroup
                                       from ticket in ticketGroup.DefaultIfEmpty()
                                       where ((session.IDMovie == TransmittedData.idSelectedMovieAnalysis) && ((beginDate.SelectedDate == null || session.DateAndTimeSession >= beginDate.SelectedDate.Value) && (endDate.SelectedDate == null || session.DateAndTimeSession <= endDate.SelectedDate.Value)))
                                       select new
                                       {
                                           session.IDSession,
                                           session.DateAndTimeSession,
                                           TicketID = ticket == null ? 0 : ticket.IDTicket,
                                           session.TicketPrice,
                                       }).ToList();

                    var groupedSessionData = sessionData.GroupBy(m => m.IDSession)
                        .Select(g => new
                        {
                            g.Key,
                            TicketCount = g.Count(),
                            TotalCost = g.Sum(t => t.TicketPrice)
                        }).ToList();

                    var result = groupedSessionData.Select(g =>
                    {
                        var session = sessionData.FirstOrDefault(m => m.IDSession == g.Key);
                        return new
                        {
                            session.IDSession,
                            session.DateAndTimeSession,
                            session.TicketPrice,
                            g.TicketCount,
                            g.TotalCost
                        };
                    }).ToList();

                    foreach (var sessionLine in result)
                    {
                        SessionDataAnalysis sessionDataAnalysisClass = new SessionDataAnalysis();

                        sessionDataAnalysisClass.idSession = sessionLine.IDSession;
                        sessionDataAnalysisClass.dateSession = sessionLine.DateAndTimeSession.ToString().Split(' ')[0];
                        sessionDataAnalysisClass.timeSession = sessionLine.DateAndTimeSession.ToString().Split(' ')[1];
                        sessionDataAnalysisClass.ticketPrice = sessionLine.TicketPrice.ToString().Remove(sessionLine.TicketPrice.ToString().Length - 2, 2) + " руб."; ;

                        if (sessionLine.TicketCount == 1)
                        {
                            var ticketCount = dataBase.Ticket.Where(w => w.IDSession == sessionLine.IDSession).Count();
                            sessionDataAnalysisClass.ticketCountSession = ticketCount.ToString();
                            sessionDataAnalysisClass.ticketSummarySession = (sessionLine.TicketPrice * ticketCount).ToString().Remove((sessionLine.TicketPrice * ticketCount).ToString().Length - 2, 2) + " руб.";
                        }
                        else
                        {
                            sessionDataAnalysisClass.ticketCountSession = sessionLine.TicketCount.ToString();
                            sessionDataAnalysisClass.ticketSummarySession = sessionLine.TotalCost.ToString().Remove(sessionLine.TotalCost.ToString().Length - 2, 2) + " руб.";
                        }

                        sessionDataAnalysesList.Add(sessionDataAnalysisClass);
                    }

                    var fullSessionData = dataBase.Session.Where(w => w.IDMovie == TransmittedData.idSelectedMovieAnalysis).ToList();
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
                SessionInformationGrid.Width = ActualWidth - ActualWidth * 0.5;
                AnalyticsGrid.Width = ActualWidth - ActualWidth * 0.5;
                SessionList.Height = ActualHeight - 35;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GoBackPage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Window activeWindow = Window.GetWindow(this);

                if (activeWindow.Title == "Панель директора")
                {
                    DirectorsPanel directorsPanel = Window.GetWindow(this) as DirectorsPanel;
                    directorsPanel.TicketAnalysisOpen();
                }
                else
                if (activeWindow.Title == "Панель менеджера")
                {
                    BookerPanel bookerPanel = Window.GetWindow(this) as BookerPanel;
                    bookerPanel.TicketAnalysisOpen();
                }
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
                    BeginDate.IsEnabled = false;
                    BeginDate.SelectedDate = null;
                    EndDate.IsEnabled = false;
                    EndDate.SelectedDate = null;
                }
                else
                {
                    BeginDate.IsEnabled = true;
                    BeginDate.SelectedDate = DateTime.Now;
                    EndDate.IsEnabled = true;
                    EndDate.SelectedDate = DateTime.Now;
                }

                LoadData(BeginDate, EndDate);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FindDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                LoadData(BeginDate, EndDate);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var dataBase = new CinemaEntities())
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";

                    var sessionData = (from session in dataBase.Session
                                       join
                                       movie in dataBase.Movie on session.IDMovie equals movie.IDMovie into movieGroup
                                       from movie in movieGroup.DefaultIfEmpty()
                                       where ((session.IDMovie == TransmittedData.idSelectedMovieAnalysis) && ((BeginDate.SelectedDate == null || session.DateAndTimeSession >= BeginDate.SelectedDate.Value) && (EndDate.SelectedDate == null || session.DateAndTimeSession <= EndDate.SelectedDate.Value)))
                                       select new
                                       {
                                           session.IDSession,
                                           movie.Title,
                                           movie.YearOfPublication,
                                           session.DateAndTimeSession,
                                       }).FirstOrDefault();

                    if (saveFileDialog.ShowDialog() == true)
                    {
                        Document doc = new Document();
                        PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream($@"{saveFileDialog.FileName}", FileMode.Create));
                        doc.SetPageSize(PageSize.A4.Rotate());

                        BaseFont baseFont = BaseFont.CreateFont("C:\\Windows\\Fonts\\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                        Font font = new Font(baseFont, 12);

                        BaseFont baseFontHead = BaseFont.CreateFont("C:\\Windows\\Fonts\\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                        Font fontHead = new Font(baseFont, 20, Font.BOLD);

                        doc.Open();

                        Paragraph mainParagraph = new Paragraph($"Отчет продаж по фильму {sessionData.Title + " " + sessionData.YearOfPublication}", fontHead);
                        mainParagraph.Alignment = Element.ALIGN_CENTER;
                        doc.Add(mainParagraph);

                        PdfPTable table = new PdfPTable(4);
                        table.SpacingBefore = 10;
                        table.WidthPercentage = 100;

                        table.AddCell(new Paragraph("Код сеанса", font));
                        table.AddCell(new Paragraph("Дата и время проведения сеанса", font));
                        table.AddCell(new Paragraph("Количество проданных билетов", font));
                        table.AddCell(new Paragraph("Выручка с проданных билетов", font));

                        foreach (var line in sessionDataAnalysesList)
                        {
                            table.AddCell(new Paragraph(line.idSession.ToString(), font));
                            table.AddCell(new Paragraph(line.dateSession + " " + line.timeSession, font));
                            table.AddCell(new Paragraph(line.ticketCountSession, font));
                            table.AddCell(new Paragraph(line.ticketSummarySession.ToString(), font));

                        }
                        doc.Add(table);

                        double ticketPriceSumm = 0;
                        foreach (var line in sessionDataAnalysesList)
                        {
                            ticketPriceSumm += Convert.ToDouble(line.ticketSummarySession.Replace(" руб.", ""));
                        }

                        Paragraph paragraph = new Paragraph("Итог: " + ticketPriceSumm.ToString() + " Руб.", font);
                        doc.Add(paragraph);

                        doc.Close();
                        MessageBox.Show("Файл сохранен", "Готово", MessageBoxButton.OK, MessageBoxImage.Information);

                        Process.Start(saveFileDialog.FileName);
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
