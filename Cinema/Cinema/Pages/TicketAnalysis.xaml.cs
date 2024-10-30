using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Cinema.Authorization;
using static Cinema.SessionControls;
using Microsoft.Win32;
using System.Diagnostics;

namespace Cinema
{
    /// <summary>
    /// Логика взаимодействия для TicketAnalysis.xaml
    /// </summary>
    public partial class TicketAnalysis : Page
    {
        public TicketAnalysis()
        {
            InitializeComponent();
        }

        public class TicketData
        {
            public int ticketMovieID { get; set; }
            public BitmapImage ticketMovieCover { get; set; }
            public string ticketMovieTitle { get; set; }
            public string ticketMovieGenre { get; set; }
            public int ticketMovieYearOfPublication { get; set; }
            public double ticketMovieTiming { get; set; }
            public string ticketMovieAgeRating { get; set; }
            public string ticketMovieCountry { get; set; }
            public string ticketMovieActors { get; set; }
            public string ticketCount { get; set; }
            public string ticketSummaryCost { get; set; }
        }

        List<TicketData> ticketDataList = new List<TicketData>();

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            TicketList.ItemsSource = ticketDataList;

            LoadData(null, BeginDate, EndDate);
        }

        private void LoadData(string findLine, DatePicker beginDate, DatePicker endDate)
        {
            using (var dataBase = new CinemaEntities())
            {
                ticketDataList.Clear();

                var ticketData = (from ticket in dataBase.Ticket
                                 join
                                 session in dataBase.Session on ticket.IDSession equals session.IDSession into SessionGroup
                                 from session in SessionGroup.DefaultIfEmpty()
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
                                 genre in dataBase.Genre on movieGenere.IDGenre equals genre.IDGenre into genreGroup
                                 from genre in genreGroup.DefaultIfEmpty()
                                 join
                                 country in dataBase.Country on movie.IDCountry equals country.IDCountry into countryGroup
                                 from country in countryGroup.DefaultIfEmpty()
                                 where ((findLine == null || movie.Title.Contains(findLine) || genre.Title.Contains(findLine) || movie.YearOfPublication.ToString().Contains(findLine) || movie.Description.Contains(findLine) || country.Title.Contains(findLine)) && ((beginDate.SelectedDate == null || session.DateAndTimeSession > beginDate.SelectedDate.Value) && (endDate.SelectedDate == null || session.DateAndTimeSession < endDate.SelectedDate.Value)))
                                 select new
                                 {
                                     movie.IDMovie,
                                     movie.Cover,
                                     movieTitle = movie.Title,
                                     movieGenre = genre.Title,
                                     movie.YearOfPublication,
                                     movie.Timing,
                                     movie.AgeRating,
                                     movieCountry = country.Title,
                                     movieActors = actors.Surname + " " + actors.Name + " " + actors.Patronymic + " " + actors.Nickname,
                                     session.TicketPrice
                                 }
                                 ).ToList();

                var groupedMovieData = ticketData.GroupBy(m => m.IDMovie)
                    .Select(g => new
                    {
                        g.Key,
                        genres = g.Select(m => m.movieGenre).Distinct(),
                        actors = g.Select(m => m.movieActors).Distinct(),
                        TicketCount = g.Count(),
                        TotalCost = g.Sum(t => t.TicketPrice)
                    }).ToList();

                var result = groupedMovieData.Select(g =>
                {
                    var movie = ticketData.FirstOrDefault(m => m.IDMovie == g.Key);
                    return new
                    {
                        movie.IDMovie,
                        movie.Cover,
                        movie.movieTitle,
                        movieGenres = g.genres,
                        movie.YearOfPublication,
                        movie.Timing,
                        movie.AgeRating,
                        movie.movieCountry,
                        movieActors = g.actors,
                        g.TicketCount,
                        g.TotalCost,
                        movie.TicketPrice,
                    };
                }).ToList();

                foreach (var ticketLine in result)
                {
                    TicketData ticketDataClass = new TicketData();

                    ticketDataClass.ticketMovieID = ticketLine.IDMovie;

                    if (ticketLine.Cover != null)
                    {
                        BitmapImage cover = new BitmapImage();

                        cover.BeginInit();
                        cover.StreamSource = new MemoryStream(ticketLine.Cover);
                        cover.EndInit();

                        ticketDataClass.ticketMovieCover = cover;
                    }
                    else
                    {
                        ticketDataClass.ticketMovieCover = new BitmapImage(new Uri("pack://application:,,,/Resource/NoImage.png", UriKind.Absolute));
                    }

                    ticketDataClass.ticketMovieTitle = ticketLine.movieTitle;

                    string movieGenereTemp = "";
                    int movieGenresCounterTemp = 1;
                    foreach (var genere in ticketLine.movieGenres)
                    {
                        if (movieGenresCounterTemp != ticketLine.movieGenres.Count())
                            movieGenereTemp += genere + ", ";
                        else
                            movieGenereTemp += genere;

                        movieGenresCounterTemp++;
                    }
                    ticketDataClass.ticketMovieGenre = movieGenereTemp;

                    string movieActorTemp = "";
                    int movieActorCounterTemp = 1;
                    foreach (var actor in ticketLine.movieActors)
                    {
                        if (movieActorCounterTemp != ticketLine.movieActors.Count())
                        {
                            movieActorTemp += actor + ", ";
                        }
                        else
                        {
                            movieActorTemp += actor;
                        }

                        movieActorCounterTemp++;
                    }
                    ticketDataClass.ticketMovieActors = movieActorTemp;

                    ticketDataClass.ticketMovieYearOfPublication = ticketLine.YearOfPublication;
                    ticketDataClass.ticketMovieTiming = ticketLine.Timing;
                    ticketDataClass.ticketMovieAgeRating = ticketLine.AgeRating + "+";
                    ticketDataClass.ticketMovieCountry = ticketLine.movieCountry;

                    ticketDataClass.ticketCount = (ticketLine.TicketCount/((movieGenresCounterTemp-1) * (movieActorCounterTemp - 1))).ToString();
                    ticketDataClass.ticketSummaryCost = (ticketLine.TotalCost/((movieGenresCounterTemp-1) * (movieActorCounterTemp - 1))).ToString().Remove((ticketLine.TotalCost / ((movieGenresCounterTemp - 1) * (movieActorCounterTemp - 1))).ToString().Length -2, 2) + " руб.";

                    ticketDataList.Add(ticketDataClass);
                }

                var fullMovieData = (from ticket in dataBase.Ticket
                                     join
                                     session in dataBase.Session on ticket.IDSession equals session.IDSession into sessionGroup
                                     from session in sessionGroup.DefaultIfEmpty()
                                     join
                                     movie in dataBase.Movie on session.IDMovie equals movie.IDMovie into movieGroup
                                     from movie in movieGroup.DefaultIfEmpty()
                                     group ticket by movie into movieTickets
                                     where movieTickets.Count() > 1
                                     select new
                                     {
                                         Movie = movieTickets.Key,
                                         TicketCount = movieTickets.Count()
                                     }).ToList();

                FindCounterData.Text = result.Count() + "/" + fullMovieData.Count();

                TicketList.Items.Refresh();
            }
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            MovieCodeGrid.Width = 0;
            ImageGrid.Width = ActualWidth - ActualWidth * 0.8;
            MovieInfoGrid.Width = ActualWidth - ActualWidth * 0.6;
            AnalyticsGrid.Width = ActualWidth - ActualWidth * 0.6;
            TicketList.Height = ActualHeight - 35;
        }

        private void BeginDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadData(FindData.Text, BeginDate, EndDate);
        }

        private void EndDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadData(FindData.Text, BeginDate, EndDate);
        }

        private void FindDateChecker_Click(object sender, RoutedEventArgs e)
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

            LoadData(FindData.Text, BeginDate, EndDate);
        }

        private void FindData_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadData(FindData.Text, BeginDate, EndDate);
        }

        private void TicketList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedSession = TicketList.SelectedItem as TicketData;
            if (selectedSession != null)
            {
                TransmittedData.idSelectedMovieAnalysis = selectedSession.ticketMovieID;

                Window activeWindow = Window.GetWindow(this);

                if (activeWindow.Title == "Панель директора")
                {
                    DirectorsPanel directorsPanel = Window.GetWindow(this) as DirectorsPanel;
                    directorsPanel.MovieAnalysisOpen();
                }
                else
                if(activeWindow.Title == "Панель менеджера")
                {
                    BookerPanel bookerPanel = Window.GetWindow(this) as BookerPanel;
                    bookerPanel.MovieAnalysisOpen();
                }
            }
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            using (var dataBase = new CinemaEntities())
            {
                string findLine = FindData.Text;

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";

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

                    Paragraph mainParagraph = new Paragraph("Отчет продаж", fontHead);
                    mainParagraph.Alignment = Element.ALIGN_CENTER;
                    doc.Add(mainParagraph);

                    PdfPTable table = new PdfPTable(10);
                    table.SpacingBefore = 10;
                    table.WidthPercentage = 100;

                    table.AddCell(new Paragraph("Код фильма", font));
                    table.AddCell(new Paragraph("Название фильма", font));
                    table.AddCell(new Paragraph("Жанры", font));
                    table.AddCell(new Paragraph("Год публикации", font));
                    table.AddCell(new Paragraph("Длительность фильма", font));
                    table.AddCell(new Paragraph("Возрастной рейтинг", font));
                    table.AddCell(new Paragraph("Страна", font));
                    table.AddCell(new Paragraph("Актеры", font));
                    table.AddCell(new Paragraph("Количество проданных билетов", font));
                    table.AddCell(new Paragraph("Выручка с проданных билетов", font));

                    foreach (var line in ticketDataList)
                    {
                        table.AddCell(new Paragraph(line.ticketMovieID.ToString(), font));
                        table.AddCell(new Paragraph(line.ticketMovieTitle, font));
                        table.AddCell(new Paragraph(line.ticketMovieGenre, font));
                        table.AddCell(new Paragraph(line.ticketMovieYearOfPublication.ToString(), font));
                        table.AddCell(new Paragraph(line.ticketMovieTiming.ToString(), font));
                        table.AddCell(new Paragraph(line.ticketMovieAgeRating, font));
                        table.AddCell(new Paragraph(line.ticketMovieCountry, font));
                        table.AddCell(new Paragraph(line.ticketMovieActors, font));
                        table.AddCell(new Paragraph(line.ticketCount, font));
                        table.AddCell(new Paragraph(line.ticketSummaryCost, font));
                    }
                    doc.Add(table);

                    double ticketPriceSumm = 0;
                    foreach (var line in ticketDataList)
                    {
                        ticketPriceSumm += Convert.ToDouble(line.ticketSummaryCost.Replace(" руб.",""));
                    }

                    Paragraph paragraph = new Paragraph("Итог: " + ticketPriceSumm.ToString() + " Руб.", font);
                    doc.Add(paragraph);

                    doc.Close();
                    MessageBox.Show("Файл сохранен", "Готово", MessageBoxButton.OK, MessageBoxImage.Information);

                    Process.Start(saveFileDialog.FileName);
                }
            }
        }

        // UsnitTest
        /*
        public string PrintPDF()
        {
            string findLine = FindData.Text;

            string path = "C:\\AIChat\\lol.txt";

            if (path != null)
            {
                Document doc = new Document();
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream($@"{path}", FileMode.Create));
                doc.SetPageSize(PageSize.A4.Rotate());

                BaseFont baseFont = BaseFont.CreateFont("C:\\Windows\\Fonts\\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                Font font = new Font(baseFont, 12);

                BaseFont baseFontHead = BaseFont.CreateFont("C:\\Windows\\Fonts\\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                Font fontHead = new Font(baseFont, 20, Font.BOLD);

                doc.Open();

                Paragraph mainParagraph = new Paragraph("Отчет продаж", fontHead);
                mainParagraph.Alignment = Element.ALIGN_CENTER;
                doc.Add(mainParagraph);

                PdfPTable table = new PdfPTable(10);
                table.SpacingBefore = 10;
                table.WidthPercentage = 100;

                table.AddCell(new Paragraph("Код фильма", font));
                table.AddCell(new Paragraph("Название фильма", font));
                table.AddCell(new Paragraph("Жанры", font));
                table.AddCell(new Paragraph("Год публикации", font));
                table.AddCell(new Paragraph("Длительность фильма", font));
                table.AddCell(new Paragraph("Возрастной рейтинг", font));
                table.AddCell(new Paragraph("Страна", font));
                table.AddCell(new Paragraph("Актеры", font));
                table.AddCell(new Paragraph("Количество проданных билетов", font));
                table.AddCell(new Paragraph("Выручка с проданных билетов", font));
                doc.Add(table);

                double ticketPriceSumm = 0;
                foreach (var line in ticketDataList)
                {
                    ticketPriceSumm += Convert.ToDouble(line.ticketSummaryCost.Replace(" руб.", ""));
                }

                Paragraph paragraph = new Paragraph("Итог: " + ticketPriceSumm.ToString() + " Руб.", font);
                doc.Add(paragraph);

                doc.Close();
            }

            return path;
        }
        */
    }
}
