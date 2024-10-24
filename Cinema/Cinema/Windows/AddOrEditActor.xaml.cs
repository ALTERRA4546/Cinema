﻿using Microsoft.Win32;
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
using System.Windows.Shapes;
using static Cinema.Authorization;

namespace Cinema
{
    /// <summary>
    /// Логика взаимодействия для AddOrEditActor.xaml
    /// </summary>
    public partial class AddOrEditActor : Window
    {
        public AddOrEditActor()
        {
            InitializeComponent();
        }

        public string actorPhotoPath;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ActorPhoto.Visibility = Visibility.Collapsed;
            LoadData();
        }

        private void LoadData()
        {
            using (var dataBase = new CinemaEntities())
            {
                if (TransmittedData.idSelectedActor != -1)
                {
                    var actorData = dataBase.Actor.Where(w => w.IDActors == TransmittedData.idSelectedActor).FirstOrDefault();

                    ActorSurname.Text = actorData.Surname;
                    ActorName.Text = actorData.Name;
                    ActorPatronymic.Text = actorData.Patronymic;
                    ActorNickname.Text = actorData.Nickname;

                    if (actorData.Image != null)
                    { 
                        BitmapImage bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        bitmapImage.StreamSource = new MemoryStream(actorData.Image);
                        bitmapImage.EndInit();

                        this.Height = 495;
                        ActorPhoto.Visibility = Visibility.Visible;
                        ActorPhoto.Source = bitmapImage;
                    }                       
                }
            }
        }

        private void LoadPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PNG (*.png)|*.png|JPG (*.jpg)|*.jpg";

            if (openFileDialog.ShowDialog() == true)
            {
                actorPhotoPath = openFileDialog.FileName;

                this.Height = 495;
                ActorPhoto.Visibility = Visibility.Visible;
                ActorPhoto.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (ActorSurname.Text == "" && ActorName.Text == "")
            {
                MessageBox.Show("Данные не были заполнены", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using (var dataBase = new CinemaEntities())
            {
                if (TransmittedData.idSelectedActor != -1)
                {
                    var actorData = dataBase.Actor.Where(w => w.IDActors == TransmittedData.idSelectedActor).FirstOrDefault();

                    actorData.Surname = ActorSurname.Text;
                    actorData.Name = ActorName.Text;
                    actorData.Patronymic = ActorPatronymic.Text;
                    actorData.Nickname = ActorNickname.Text;

                    if (actorPhotoPath != null)
                    {
                        actorData.Image = File.ReadAllBytes(actorPhotoPath);
                    }

                    dataBase.SaveChanges();
                }
                else
                {
                    var newActorData = new Actor();

                    newActorData.Surname = ActorSurname.Text;
                    newActorData.Name = ActorName.Text;
                    newActorData.Patronymic = ActorPatronymic.Text;
                    newActorData.Nickname = ActorNickname.Text;

                    if (actorPhotoPath != null)
                    {
                        newActorData.Image = File.ReadAllBytes(actorPhotoPath);
                    }

                    dataBase.Actor.Add(newActorData);
                    dataBase.SaveChanges();
                }

                MessageBox.Show("Данные были сохранены", "Готово", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
        }
    }
}
