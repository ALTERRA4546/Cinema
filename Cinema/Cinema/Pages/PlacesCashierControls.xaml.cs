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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            using (var dataBase = new CinemaEntities())
            {
                var ticketData = dataBase.Ticket.Where(w => w.IDSession == TransmittedData.idSelectedCashierSession).ToList();

                foreach (var ticketLine in ticketData)
                {
                    switch (ticketLine.RowNumber)
                    {
                        case 1:
                            switch (ticketLine.PlaceNumber)
                            {
                                case 1:
                                    Row1Place1.Foreground = Brushes.Red;
                                    Row1Place1.IsEnabled = false;
                                    break;

                                case 2:
                                    Row1Place2.Foreground = Brushes.Red;
                                    Row1Place2.IsEnabled = false;
                                    break;

                                case 3:
                                    Row1Place3.Foreground = Brushes.Red;
                                    Row1Place3.IsEnabled = false;
                                    break;

                                case 4:
                                    Row1Place4.Foreground = Brushes.Red;
                                    Row1Place4.IsEnabled = false;
                                    break;

                                case 5:
                                    Row1Place5.Foreground = Brushes.Red;
                                    Row1Place5.IsEnabled = false;
                                    break;

                                case 6:
                                    Row1Place6.Foreground = Brushes.Red;
                                    Row1Place6.IsEnabled = false;
                                    break;

                                case 7:
                                    Row1Place7.Foreground = Brushes.Red;
                                    Row1Place7.IsEnabled = false;
                                    break;

                                case 8:
                                    Row1Place8.Foreground = Brushes.Red;
                                    Row1Place8.IsEnabled = false;
                                    break;

                                case 9:
                                    Row1Place9.Foreground = Brushes.Red;
                                    Row1Place9.IsEnabled = false;
                                    break;

                                case 10:
                                    Row1Place10.Foreground = Brushes.Red;
                                    Row1Place10.IsEnabled = false;
                                    break;

                                case 11:
                                    Row1Place11.Foreground = Brushes.Red;
                                    Row1Place11.IsEnabled = false;
                                    break;

                                case 12:
                                    Row1Place12.Foreground = Brushes.Red;
                                    Row1Place12.IsEnabled = false;
                                    break;

                                case 13:
                                    Row1Place13.Foreground = Brushes.Red;
                                    Row1Place13.IsEnabled = false;
                                    break;

                                case 14:
                                    Row1Place14.Foreground = Brushes.Red;
                                    Row1Place14.IsEnabled = false;
                                    break;

                                case 15:
                                    Row1Place15.Foreground = Brushes.Red;
                                    Row1Place15.IsEnabled = false;
                                    break;
                            }
                            break;

                        case 2:
                            switch (ticketLine.PlaceNumber)
                            {
                                case 1:
                                    Row2Place1.Foreground = Brushes.Red;
                                    Row2Place1.IsEnabled = false;
                                    break;

                                case 2:
                                    Row2Place2.Foreground = Brushes.Red;
                                    Row2Place2.IsEnabled = false;
                                    break;

                                case 3:
                                    Row2Place3.Foreground = Brushes.Red;
                                    Row2Place3.IsEnabled = false;
                                    break;

                                case 4:
                                    Row2Place4.Foreground = Brushes.Red;
                                    Row2Place4.IsEnabled = false;
                                    break;

                                case 5:
                                    Row2Place5.Foreground = Brushes.Red;
                                    Row2Place5.IsEnabled = false;
                                    break;

                                case 6:
                                    Row2Place6.Foreground = Brushes.Red;
                                    Row2Place6.IsEnabled = false;
                                    break;

                                case 7:
                                    Row2Place7.Foreground = Brushes.Red;
                                    Row2Place7.IsEnabled = false;
                                    break;

                                case 8:
                                    Row2Place8.Foreground = Brushes.Red;
                                    Row2Place8.IsEnabled = false;
                                    break;

                                case 9:
                                    Row2Place9.Foreground = Brushes.Red;
                                    Row2Place9.IsEnabled = false;
                                    break;

                                case 10:
                                    Row2Place10.Foreground = Brushes.Red;
                                    Row2Place10.IsEnabled = false;
                                    break;

                                case 11:
                                    Row2Place11.Foreground = Brushes.Red;
                                    Row2Place11.IsEnabled = false;
                                    break;

                                case 12:
                                    Row2Place12.Foreground = Brushes.Red;
                                    Row2Place12.IsEnabled = false;
                                    break;

                                case 13:
                                    Row2Place13.Foreground = Brushes.Red;
                                    Row2Place13.IsEnabled = false;
                                    break;

                                case 14:
                                    Row2Place14.Foreground = Brushes.Red;
                                    Row2Place14.IsEnabled = false;
                                    break;

                                case 15:
                                    Row2Place15.Foreground = Brushes.Red;
                                    Row2Place15.IsEnabled = false;
                                    break;
                            }
                            break;

                        case 3:
                            switch (ticketLine.PlaceNumber)
                            {
                                case 1:
                                    Row3Place1.Foreground = Brushes.Red;
                                    Row3Place1.IsEnabled = false;
                                    break;

                                case 2:
                                    Row3Place2.Foreground = Brushes.Red;
                                    Row3Place2.IsEnabled = false;
                                    break;

                                case 3:
                                    Row3Place3.Foreground = Brushes.Red;
                                    Row3Place3.IsEnabled = false;
                                    break;

                                case 4:
                                    Row3Place4.Foreground = Brushes.Red;
                                    Row3Place4.IsEnabled = false;
                                    break;

                                case 5:
                                    Row3Place5.Foreground = Brushes.Red;
                                    Row3Place5.IsEnabled = false;
                                    break;

                                case 6:
                                    Row3Place6.Foreground = Brushes.Red;
                                    Row3Place6.IsEnabled = false;
                                    break;

                                case 7:
                                    Row3Place7.Foreground = Brushes.Red;
                                    Row3Place7.IsEnabled = false;
                                    break;

                                case 8:
                                    Row3Place8.Foreground = Brushes.Red;
                                    Row3Place8.IsEnabled = false;
                                    break;

                                case 9:
                                    Row3Place9.Foreground = Brushes.Red;
                                    Row3Place9.IsEnabled = false;
                                    break;

                                case 10:
                                    Row3Place10.Foreground = Brushes.Red;
                                    Row3Place10.IsEnabled = false;
                                    break;

                                case 11:
                                    Row3Place11.Foreground = Brushes.Red;
                                    Row3Place11.IsEnabled = false;
                                    break;

                                case 12:
                                    Row3Place12.Foreground = Brushes.Red;
                                    Row3Place12.IsEnabled = false;
                                    break;

                                case 13:
                                    Row3Place13.Foreground = Brushes.Red;
                                    Row3Place13.IsEnabled = false;
                                    break;

                                case 14:
                                    Row3Place14.Foreground = Brushes.Red;
                                    Row3Place14.IsEnabled = false;
                                    break;

                                case 15:
                                    Row3Place15.Foreground = Brushes.Red;
                                    Row3Place15.IsEnabled = false;
                                    break;
                            }
                            break;

                        case 4:
                            switch (ticketLine.PlaceNumber)
                            {
                                case 1:
                                    Row4Place1.Foreground = Brushes.Red;
                                    Row4Place1.IsEnabled = false;
                                    break;

                                case 2:
                                    Row4Place2.Foreground = Brushes.Red;
                                    Row4Place2.IsEnabled = false;
                                    break;

                                case 3:
                                    Row4Place3.Foreground = Brushes.Red;
                                    Row4Place3.IsEnabled = false;
                                    break;

                                case 4:
                                    Row4Place4.Foreground = Brushes.Red;
                                    Row4Place4.IsEnabled = false;
                                    break;

                                case 5:
                                    Row4Place5.Foreground = Brushes.Red;
                                    Row4Place5.IsEnabled = false;
                                    break;

                                case 6:
                                    Row4Place6.Foreground = Brushes.Red;
                                    Row4Place6.IsEnabled = false;
                                    break;

                                case 7:
                                    Row4Place7.Foreground = Brushes.Red;
                                    Row4Place7.IsEnabled = false;
                                    break;

                                case 8:
                                    Row4Place8.Foreground = Brushes.Red;
                                    Row4Place8.IsEnabled = false;
                                    break;

                                case 9:
                                    Row4Place9.Foreground = Brushes.Red;
                                    Row4Place9.IsEnabled = false;
                                    break;

                                case 10:
                                    Row4Place10.Foreground = Brushes.Red;
                                    Row4Place10.IsEnabled = false;
                                    break;

                                case 11:
                                    Row4Place11.Foreground = Brushes.Red;
                                    Row4Place11.IsEnabled = false;
                                    break;

                                case 12:
                                    Row4Place12.Foreground = Brushes.Red;
                                    Row4Place12.IsEnabled = false;
                                    break;

                                case 13:
                                    Row4Place13.Foreground = Brushes.Red;
                                    Row4Place13.IsEnabled = false;
                                    break;

                                case 14:
                                    Row4Place14.Foreground = Brushes.Red;
                                    Row4Place14.IsEnabled = false;
                                    break;

                                case 15:
                                    Row4Place15.Foreground = Brushes.Red;
                                    Row4Place15.IsEnabled = false;
                                    break;
                            }
                            break;

                        case 5:
                            switch (ticketLine.PlaceNumber)
                            {
                                case 1:
                                    Row5Place1.Foreground = Brushes.Red;
                                    Row5Place1.IsEnabled = false;
                                    break;

                                case 2:
                                    Row5Place2.Foreground = Brushes.Red;
                                    Row5Place2.IsEnabled = false;
                                    break;

                                case 3:
                                    Row5Place3.Foreground = Brushes.Red;
                                    Row5Place3.IsEnabled = false;
                                    break;

                                case 4:
                                    Row5Place4.Foreground = Brushes.Red;
                                    Row5Place4.IsEnabled = false;
                                    break;

                                case 5:
                                    Row5Place5.Foreground = Brushes.Red;
                                    Row5Place5.IsEnabled = false;
                                    break;

                                case 6:
                                    Row5Place6.Foreground = Brushes.Red;
                                    Row5Place6.IsEnabled = false;
                                    break;

                                case 7:
                                    Row5Place7.Foreground = Brushes.Red;
                                    Row5Place7.IsEnabled = false;
                                    break;

                                case 8:
                                    Row5Place8.Foreground = Brushes.Red;
                                    Row5Place8.IsEnabled = false;
                                    break;

                                case 9:
                                    Row5Place9.Foreground = Brushes.Red;
                                    Row5Place9.IsEnabled = false;
                                    break;

                                case 10:
                                    Row5Place10.Foreground = Brushes.Red;
                                    Row5Place10.IsEnabled = false;
                                    break;

                                case 11:
                                    Row5Place11.Foreground = Brushes.Red;
                                    Row5Place11.IsEnabled = false;
                                    break;

                                case 12:
                                    Row5Place12.Foreground = Brushes.Red;
                                    Row5Place12.IsEnabled = false;
                                    break;

                                case 13:
                                    Row5Place13.Foreground = Brushes.Red;
                                    Row5Place13.IsEnabled = false;
                                    break;

                                case 14:
                                    Row5Place14.Foreground = Brushes.Red;
                                    Row5Place14.IsEnabled = false;
                                    break;

                                case 15:
                                    Row5Place15.Foreground = Brushes.Red;
                                    Row5Place15.IsEnabled = false;
                                    break;
                            }
                            break;

                        case 6:
                            switch (ticketLine.PlaceNumber)
                            {
                                case 1:
                                    Row6Place1.Foreground = Brushes.Red;
                                    Row6Place1.IsEnabled = false;
                                    break;

                                case 2:
                                    Row6Place2.Foreground = Brushes.Red;
                                    Row6Place2.IsEnabled = false;
                                    break;

                                case 3:
                                    Row6Place3.Foreground = Brushes.Red;
                                    Row6Place3.IsEnabled = false;
                                    break;

                                case 4:
                                    Row6Place4.Foreground = Brushes.Red;
                                    Row6Place4.IsEnabled = false;
                                    break;

                                case 5:
                                    Row6Place5.Foreground = Brushes.Red;
                                    Row6Place5.IsEnabled = false;
                                    break;

                                case 6:
                                    Row6Place6.Foreground = Brushes.Red;
                                    Row6Place6.IsEnabled = false;
                                    break;

                                case 7:
                                    Row6Place7.Foreground = Brushes.Red;
                                    Row6Place7.IsEnabled = false;
                                    break;

                                case 8:
                                    Row6Place8.Foreground = Brushes.Red;
                                    Row6Place8.IsEnabled = false;
                                    break;

                                case 9:
                                    Row6Place9.Foreground = Brushes.Red;
                                    Row6Place9.IsEnabled = false;
                                    break;

                                case 10:
                                    Row6Place10.Foreground = Brushes.Red;
                                    Row6Place10.IsEnabled = false;
                                    break;

                                case 11:
                                    Row6Place11.Foreground = Brushes.Red;
                                    Row6Place11.IsEnabled = false;
                                    break;

                                case 12:
                                    Row6Place12.Foreground = Brushes.Red;
                                    Row6Place12.IsEnabled = false;
                                    break;

                                case 13:
                                    Row6Place13.Foreground = Brushes.Red;
                                    Row6Place13.IsEnabled = false;
                                    break;

                                case 14:
                                    Row6Place14.Foreground = Brushes.Red;
                                    Row6Place14.IsEnabled = false;
                                    break;

                                case 15:
                                    Row6Place15.Foreground = Brushes.Red;
                                    Row6Place15.IsEnabled = false;
                                    break;
                            }
                            break;

                        case 7:
                            switch (ticketLine.PlaceNumber)
                            {
                                case 1:
                                    Row7Place1.Foreground = Brushes.Red;
                                    Row7Place1.IsEnabled = false;
                                    break;

                                case 2:
                                    Row7Place2.Foreground = Brushes.Red;
                                    Row7Place2.IsEnabled = false;
                                    break;

                                case 3:
                                    Row7Place3.Foreground = Brushes.Red;
                                    Row7Place3.IsEnabled = false;
                                    break;

                                case 4:
                                    Row7Place4.Foreground = Brushes.Red;
                                    Row7Place4.IsEnabled = false;
                                    break;

                                case 5:
                                    Row7Place5.Foreground = Brushes.Red;
                                    Row7Place5.IsEnabled = false;
                                    break;

                                case 6:
                                    Row7Place6.Foreground = Brushes.Red;
                                    Row7Place6.IsEnabled = false;
                                    break;

                                case 7:
                                    Row7Place7.Foreground = Brushes.Red;
                                    Row7Place7.IsEnabled = false;
                                    break;

                                case 8:
                                    Row7Place8.Foreground = Brushes.Red;
                                    Row7Place8.IsEnabled = false;
                                    break;

                                case 9:
                                    Row7Place9.Foreground = Brushes.Red;
                                    Row7Place9.IsEnabled = false;
                                    break;

                                case 10:
                                    Row7Place10.Foreground = Brushes.Red;
                                    Row7Place10.IsEnabled = false;
                                    break;

                                case 11:
                                    Row7Place11.Foreground = Brushes.Red;
                                    Row7Place11.IsEnabled = false;
                                    break;

                                case 12:
                                    Row7Place12.Foreground = Brushes.Red;
                                    Row7Place12.IsEnabled = false;
                                    break;

                                case 13:
                                    Row7Place13.Foreground = Brushes.Red;
                                    Row7Place13.IsEnabled = false;
                                    break;

                                case 14:
                                    Row7Place14.Foreground = Brushes.Red;
                                    Row7Place14.IsEnabled = false;
                                    break;

                                case 15:
                                    Row7Place15.Foreground = Brushes.Red;
                                    Row7Place15.IsEnabled = false;
                                    break;
                            }
                            break;

                        case 8:
                            switch (ticketLine.PlaceNumber)
                            {
                                case 1:
                                    Row8Place1.Foreground = Brushes.Red;
                                    Row8Place1.IsEnabled = false;
                                    break;

                                case 2:
                                    Row8Place2.Foreground = Brushes.Red;
                                    Row8Place2.IsEnabled = false;
                                    break;

                                case 3:
                                    Row8Place3.Foreground = Brushes.Red;
                                    Row8Place3.IsEnabled = false;
                                    break;

                                case 4:
                                    Row8Place4.Foreground = Brushes.Red;
                                    Row8Place4.IsEnabled = false;
                                    break;

                                case 5:
                                    Row8Place5.Foreground = Brushes.Red;
                                    Row8Place5.IsEnabled = false;
                                    break;

                                case 6:
                                    Row8Place6.Foreground = Brushes.Red;
                                    Row8Place6.IsEnabled = false;
                                    break;

                                case 7:
                                    Row8Place7.Foreground = Brushes.Red;
                                    Row8Place7.IsEnabled = false;
                                    break;

                                case 8:
                                    Row8Place8.Foreground = Brushes.Red;
                                    Row8Place8.IsEnabled = false;
                                    break;

                                case 9:
                                    Row8Place9.Foreground = Brushes.Red;
                                    Row8Place9.IsEnabled = false;
                                    break;

                                case 10:
                                    Row8Place10.Foreground = Brushes.Red;
                                    Row8Place10.IsEnabled = false;
                                    break;

                                case 11:
                                    Row8Place11.Foreground = Brushes.Red;
                                    Row8Place11.IsEnabled = false;
                                    break;

                                case 12:
                                    Row8Place12.Foreground = Brushes.Red;
                                    Row8Place12.IsEnabled = false;
                                    break;

                                case 13:
                                    Row8Place13.Foreground = Brushes.Red;
                                    Row8Place13.IsEnabled = false;
                                    break;

                                case 14:
                                    Row8Place14.Foreground = Brushes.Red;
                                    Row8Place14.IsEnabled = false;
                                    break;

                                case 15:
                                    Row8Place15.Foreground = Brushes.Red;
                                    Row8Place15.IsEnabled = false;
                                    break;
                            }
                            break;
                    }
                }

                var sessionData = dataBase.Session.Where(w => w.IDSession == TransmittedData.idSelectedCashierSession).FirstOrDefault();
                TicketPrice.Content = sessionData.TicketPrice.ToString().Remove(sessionData.TicketPrice.ToString().Length-2,2) + " Руб.";
                DateTimeSession.Content = sessionData.DateAndTimeSession;
            }
        }

        private void Row1_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                selectedRowNumber = 1;
                selectedPlaceNumber = Convert.ToInt32(clickedButton.Content.ToString());

                SelectedPlaceChange();
            }
        }

        private void Row2_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                selectedRowNumber = 2;
                selectedPlaceNumber = Convert.ToInt32(clickedButton.Content.ToString());

                SelectedPlaceChange();
            }
        }

        private void Row3_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                selectedRowNumber = 3;
                selectedPlaceNumber = Convert.ToInt32(clickedButton.Content.ToString());

                SelectedPlaceChange();
            }
        }

        private void Row4_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                selectedRowNumber = 4;
                selectedPlaceNumber = Convert.ToInt32(clickedButton.Content.ToString());

                SelectedPlaceChange();
            }
        }

        private void Row5_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                selectedRowNumber = 5;
                selectedPlaceNumber = Convert.ToInt32(clickedButton.Content.ToString());

                SelectedPlaceChange();
            }
        }

        private void Row6_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                selectedRowNumber = 6;
                selectedPlaceNumber = Convert.ToInt32(clickedButton.Content.ToString());

                SelectedPlaceChange();
            }
        }

        private void Row7_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                selectedRowNumber = 7;
                selectedPlaceNumber = Convert.ToInt32(clickedButton.Content.ToString());

                SelectedPlaceChange();
            }
        }

        private void Row8_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                selectedRowNumber = 8;
                selectedPlaceNumber = Convert.ToInt32(clickedButton.Content.ToString());

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
            SelectedPlaceChange();
        }

        private void PrintTicket(int selectedTicket)
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

                    PrintTicket(newTicket.IDTicket);

                    ClearPlaceChange();
                    LoadData();
                }
            }
        }
    }
}
