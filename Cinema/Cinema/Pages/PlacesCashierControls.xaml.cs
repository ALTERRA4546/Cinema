using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using static Cinema.Authorization;
using System.Text.RegularExpressions;
using System.Windows.Controls.Primitives;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using Org.BouncyCastle.Asn1.Ocsp;
using static iTextSharp.text.pdf.events.IndexEvents;
using System.Data.Entity;
using static Cinema.TicketAnalysis;

namespace Cinema
{
    /// <summary>
    /// Логика взаимодействия для PlacesCashierControls.xaml
    /// </summary>
    public partial class PlacesCashierControls : Page
    {
        public PlacesCashierControls()
        {
            InitializeComponent();
        }

        public int selectedRowNumber;
        public int selectedPlaceNumber;
        public Button selectedRowPlaceButton;

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            using (var dataBase = new CinemaEntities())
            {
                var ticketData = dataBase.Ticket.Where(w => w.IDSession == TransmittedData.idSelectedCashierSession).ToList();

                var settingsData = dataBase.Settings.OrderByDescending(o => o.DateTimeChange).FirstOrDefault();

                if (settingsData != null)
                {
                    Hall.Children.Clear();

                    for (int row = 0; row < settingsData.RowHall; row++)
                    {
                        WrapPanel wrapPanel = new WrapPanel();

                        TextBlock textBlockBegin = new TextBlock();
                        textBlockBegin.Text = $"Ряд {row + 1}";
                        textBlockBegin.FontSize = 10;
                        textBlockBegin.Margin = new Thickness(0, 0, 5, 0);
                        textBlockBegin.VerticalAlignment = VerticalAlignment.Center;

                        TextBlock textBlockEnd = new TextBlock();
                        textBlockEnd.Text = $"Ряд {row + 1}";
                        textBlockEnd.FontSize = 10;
                        textBlockEnd.VerticalAlignment = VerticalAlignment.Center;

                        wrapPanel.Children.Add(textBlockBegin);
                        for (int place = 1; place <= settingsData.PlaceHall; place++)
                        {
                            Button button = new Button();
                            button.Content = place;
                            button.Name = $"Row{row}Place{place}";
                            button.Width = 18;
                            button.Height = 18;
                            button.FontSize = 10;
                            button.Background = Brushes.White;
                            button.Margin = new Thickness(0, 0, 5, 0);
                            button.Click += HallButton_Click;

                            if (settingsData.HiddenPlaces != null)
                            {
                                string[] hidePlaceTemp = settingsData.HiddenPlaces.Split('|');
                                foreach (var hidePlaceLine in hidePlaceTemp)
                                {
                                    Match match = Regex.Match(hidePlaceLine, @"Row(\d+)Place(\d+)");

                                    if (match.Success)
                                    {
                                        if (row + 1 == int.Parse(match.Groups[1].Value) + 1 && place == int.Parse(match.Groups[2].Value))
                                        {
                                            button.Click -= HallButton_Click;
                                            button.Opacity = 0;
                                            button.IsEnabled = false;
                                        }
                                    }
                                }
                            }

                            foreach (var ticketLine in ticketData)
                            {
                                if (row + 1 == ticketLine.RowNumber && place == ticketLine.PlaceNumber)
                                {
                                    button.Foreground = Brushes.Red;
                                    button.IsEnabled = false;
                                }
                            }

                            wrapPanel.Children.Add(button);
                        }
                        wrapPanel.Children.Add(textBlockEnd);

                        StackPanel stackPanel = new StackPanel();
                        stackPanel.Margin = new Thickness(0, 0, 0, 5);
                        stackPanel.HorizontalAlignment = HorizontalAlignment.Center;
                        stackPanel.Children.Add(wrapPanel);

                        Hall.Children.Add(stackPanel);
                    }
                }
                else
                { 
                    Label lable = new Label();
                    lable.FontSize = 26;
                    lable.Foreground = Brushes.Red;
                    lable.Content = "Зал не размечен администратором.\rОбратитесь с системному администратору.";
                    lable.HorizontalAlignment = HorizontalAlignment.Center;
                    lable.HorizontalContentAlignment = HorizontalAlignment.Center;

                    Hall.Children.Add(lable);
                }

                var sessionData = dataBase.Session.Where(w => w.IDSession == TransmittedData.idSelectedCashierSession).FirstOrDefault();
                TicketPrice.Content = sessionData.TicketPrice.ToString().Remove(sessionData.TicketPrice.ToString().Length - 2, 2) + " Руб.";
                DateTimeSession.Content = sessionData.DateAndTimeSession;
            }
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ResizeWindows();
        }

        private void Hall_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ResizeWindows();
        }

        private void ResizeWindows()
        {
            double maxWidth = this.ActualWidth - 10;
            double maxHeight = this.ActualHeight - 10;

            double scale = this.ActualWidth / (Hall.ActualWidth * 1.28);
            double centerX = Hall.ActualWidth / 2;
            double centerY = Hall.ActualHeight / 2;

            double newWidth = Hall.ActualWidth * scale;
            double newHeight = Hall.ActualHeight * scale;

            if (newWidth > maxWidth)
            {
                scale = maxWidth / Hall.ActualWidth;
            }
            if (newHeight > maxHeight)
            {
                scale = Math.Min(scale, maxHeight / Hall.ActualHeight);
            }

            ScaleTransform scaleTransform = new ScaleTransform(scale, scale, centerX, centerY);
            Hall.RenderTransform = scaleTransform;
        }

        private void HallButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;

            if (clickedButton != null)
            {
                if (selectedRowPlaceButton != null)
                {
                    selectedRowPlaceButton.Background = Brushes.White;
                    selectedRowPlaceButton.Foreground = Brushes.Black;
                    selectedRowPlaceButton.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#FF707070"));
                }

                Match match = Regex.Match(clickedButton.Name, @"Row(\d+)Place(\d+)");

                if (match.Success)
                {
                    selectedRowNumber = int.Parse(match.Groups[1].Value)+1;
                    selectedPlaceNumber = int.Parse(match.Groups[2].Value);

                    clickedButton.Background = Brushes.Aqua;
                    clickedButton.Foreground = Brushes.Black;
                    clickedButton.BorderBrush = Brushes.Black;
                    selectedRowPlaceButton = clickedButton;
                }

                SelectedPlaceChange();
            }
        }

        private void SelectedPlaceChange()
        {
            if (selectedRowNumber != 0)
            {
                SelectedRow.Content = selectedRowNumber;
            }
            else
            {
                SelectedRow.Content = "Нет";
            }

            if (selectedPlaceNumber != 0)
            {
                SelectedPlace.Content = selectedPlaceNumber;
            }
            else
            {
                SelectedPlace.Content = "Нет";
            }
        }

        private void ClearPlaceChange()
        {
            selectedRowNumber = 0;
            selectedPlaceNumber = 0;
            selectedRowPlaceButton = null;
            SelectedPlaceChange();
        }

        private async Task PrintTicket(int selectedTicket)
        {
            using (var dataBase = new CinemaEntities())
            {
                var ticketData = (from ticket in dataBase.Ticket
                                  join
                                  session in dataBase.Session on ticket.IDSession equals session.IDSession into sessionGroup
                                  from session in sessionGroup.DefaultIfEmpty()
                                  join
                                  movie in dataBase.Movie on session.IDMovie equals movie.IDMovie into movieGroup
                                  from movie in movieGroup.DefaultIfEmpty()
                                  join
                                  employee in dataBase.Employee on ticket.IDEmployee equals employee.IDEmployee into employeeGroup
                                  from employee in employeeGroup.DefaultIfEmpty()
                                  where (ticket.IDTicket == selectedTicket)
                                  select new
                                  {
                                      ticket.IDTicket,
                                      ticket.RowNumber,
                                      ticket.PlaceNumber,
                                      movie = movie.Title,
                                      session.DateAndTimeSession,
                                      employee.Surname,
                                      employee.Name,
                                      employee.Patronymic,
                                      session.TicketPrice
                                  }).FirstOrDefault();


                /*string textTicket = $"Номер билета:\t{ticketData.IDTicket}\nФильм:\t{ticketData.movie}\nРяд:\t{ticketData.RowNumber}\nМесто:\t{ticketData.PlaceNumber}\nДата и время начала сеанса:\t{ticketData.DateAndTimeSession}\nКассир:\t{ticketData.Surname} {ticketData.Name} {ticketData.Patronymic}\nДата продажи:\t{DateTime.Now}\nЦена:\t{ticketData.TicketPrice.ToString().Remove(ticketData.TicketPrice.ToString().Length-2,2)} руб.";

                string tempFilePath = Path.GetTempFileName() + ".txt";
                File.WriteAllText(tempFilePath, textTicket);

                Process.Start("notepad.exe", tempFilePath);*/

                string tempFilePath = Path.GetTempFileName() + ".pdf";

                Document doc = new Document();
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream($@"{tempFilePath}", FileMode.Create));
                //doc.SetPageSize(PageSize.A4.Rotate());

                BaseFont baseFont = BaseFont.CreateFont("C:\\Windows\\Fonts\\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                Font font = new Font(baseFont, 16);

                BaseFont baseFontHead = BaseFont.CreateFont("C:\\Windows\\Fonts\\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                Font fontHead = new Font(baseFont, 20, Font.BOLD);

                doc.Open();

                Paragraph mainParagraph = new Paragraph("Билет", fontHead);
                mainParagraph.Alignment = Element.ALIGN_CENTER;
                Paragraph paragraph = new Paragraph("Номер билета: " + ticketData.IDTicket, font);
                Paragraph paragraph1 = new Paragraph("Фильм: " + ticketData.movie, font);
                Paragraph paragraph2 = new Paragraph("Ряд: " + ticketData.RowNumber, font);
                Paragraph paragraph3 = new Paragraph("Место: " + ticketData.PlaceNumber, font);
                Paragraph paragraph4 = new Paragraph("Дата и время начала сеанса: " + ticketData.DateAndTimeSession, font);
                Paragraph paragraph5 = new Paragraph("Кассир: " + ticketData.Surname + " " + ticketData.Name + " " + ticketData.Patronymic, font);
                Paragraph paragraph6 = new Paragraph("Дата продажи: " + DateTime.Now, font);
                Paragraph paragraph7 = new Paragraph("Цена: " + ticketData.TicketPrice.ToString().Remove(ticketData.TicketPrice.ToString().Length - 2, 2) + " Руб.", font);
                
                doc.Add(mainParagraph);
                doc.Add(paragraph);
                doc.Add(paragraph1);
                doc.Add(paragraph2);
                doc.Add(paragraph3);
                doc.Add(paragraph4);
                doc.Add(paragraph5);
                doc.Add(paragraph6);
                doc.Add(paragraph7);

                doc.Close();

                Process.Start(tempFilePath);
            }
        }

        private void BookingTicket_Click(object sender, RoutedEventArgs e)
        {
            if (selectedRowNumber == 0 && selectedPlaceNumber == 0)
            {
                MessageBox.Show("Место в зале не выбранно", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using (var dataBase = new CinemaEntities())
            {
                if (MessageBox.Show("Забронировать место на сеанс?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    var newTicket = new Ticket();

                    newTicket.IDSession = TransmittedData.idSelectedCashierSession;
                    newTicket.RowNumber = selectedRowNumber;
                    newTicket.PlaceNumber = selectedPlaceNumber;
                    newTicket.IDEmployee = TransmittedData.idEmployee;

                    dataBase.Ticket.Add(newTicket);
                    dataBase.SaveChanges();

                    MessageBox.Show("Место на сеанс зарезервировано", "Готово", MessageBoxButton.OK, MessageBoxImage.Information);

                    Task.Run(()=> PrintTicket(newTicket.IDTicket));

                    selectedRowPlaceButton.Background = (Brush)(new BrushConverter().ConvertFrom("#FFDDDDDD"));
                    selectedRowPlaceButton.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#FF707070"));
                    selectedRowPlaceButton.Foreground = Brushes.Red;
                    selectedRowPlaceButton.IsEnabled = false;

                    ClearPlaceChange();
                }
            }

        }

        // UnitTest
        /*
        public string PrintTicketPDF()
        {
            string tempFilePath = Path.GetTempFileName() + ".pdf";

            Document doc = new Document();
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream($@"{tempFilePath}", FileMode.Create));
            //doc.SetPageSize(PageSize.A4.Rotate());

            BaseFont baseFont = BaseFont.CreateFont("C:\\Windows\\Fonts\\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            Font font = new Font(baseFont, 16);

            BaseFont baseFontHead = BaseFont.CreateFont("C:\\Windows\\Fonts\\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            Font fontHead = new Font(baseFont, 20, Font.BOLD);

            doc.Open();

            Paragraph mainParagraph = new Paragraph("Билет", fontHead);
            mainParagraph.Alignment = Element.ALIGN_CENTER;

            doc.Add(mainParagraph);

            doc.Close();

            return tempFilePath;
        }
        */
    }
}
