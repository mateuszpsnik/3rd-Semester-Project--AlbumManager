using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Album_Manager.Model;
using Microsoft.Win32;
using System.Runtime.Serialization;
using System.IO;

namespace Album_Manager.View
{
    /// <summary>
    /// Interaction logic for EditCollectionWindow.xaml
    /// </summary>
    public partial class EditCollectionWindow : Window
    {
        public EditCollectionWindow()
        {
            InitializeComponent();

            generateCombos();
        }

        List<Genre> genresList = new List<Genre>();
        List<Mood> moodsList = new List<Mood>();
        List<DayOrNight> dayOrNight = new List<DayOrNight>();

        private void backHomeButton_Click(object sender, RoutedEventArgs e)
        {
            StartWindow startWindow = new StartWindow();
            startWindow.Show();
            this.Close();
        }

        private void generateCombos()
        {
            foreach (Genre genreComboItem in Enum.GetValues(typeof(Genre)))
            {
                genresComboBox.Items.Add(genreComboItem);
            }


            foreach (DayOrNight dayNightComboItem in Enum.GetValues(typeof(DayOrNight)))
            {
                dayNightComboBox.Items.Add(dayNightComboItem);
            }


            foreach (Mood moodComboItem in Enum.GetValues(typeof(Mood)))
            {
                moodComboBox.Items.Add(moodComboItem);
            }

        }

        private void addDayNight_Click(object sender, RoutedEventArgs e)
        {
            dayOrNight.Add((DayOrNight)dayNightComboBox.SelectedItem);
            dayNightList.Items.Add(dayNightComboBox.SelectedItem);
        }

        private void addGenre_Click(object sender, RoutedEventArgs e)
        {
            genresList.Add((Genre)genresComboBox.SelectedItem);
            genres.Items.Add(genresComboBox.SelectedItem);
        }

        private void addMood_Click(object sender, RoutedEventArgs e)
        {
            moodsList.Add((Mood)moodComboBox.SelectedItem);
            moods.Items.Add(moodComboBox.SelectedItem);
        }

        private void openButton_Click(object sender, RoutedEventArgs e)
        {
            Album albumDeserialized;

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Album Manager";
            dialog.ShowDialog();

            string path = dialog.FileName;

            var dataSerializer = new DataContractSerializer(typeof(Album));

            using (Stream stream = File.OpenRead(path))
                albumDeserialized = dataSerializer.ReadObject(stream) as Album;

            titleBox.Text = albumDeserialized.Title;
            artistBox.Text = albumDeserialized.Artist;
            yearBox.Text = albumDeserialized.Year.ToString();
            coverBox.Text = albumDeserialized.ImageUrl;
            foreach (var item in albumDeserialized.Genres)
                genres.Items.Add(item);
            dayNightComboBox.SelectedItem = albumDeserialized.DayOrNight;
            foreach (var item in albumDeserialized.Mood)
                moods.Items.Add(item);
            spotifyBox.Text = albumDeserialized.AlbumUri.ToString();            
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                + @"\Album Manager\" + titleBox.Text + @".xml";

            Album albumToSerialize = new Album(titleBox.Text, artistBox.Text, int.Parse(yearBox.Text), coverBox.Text, genresList,
                dayOrNight, moodsList, new Uri(spotifyBox.Text));

            var dataSerializer = new DataContractSerializer(typeof(Album));

            using (Stream stream = File.Create(path))
                dataSerializer.WriteObject(stream, albumToSerialize);

            titleBox.Text = "";
            artistBox.Text = "";
            yearBox.Text = "";
            coverBox.Text = "";
            dayOrNight.Clear();
            dayNightList.Items.Clear();
            genres.Items.Clear();
            genresList.Clear();
            moods.Items.Clear();
            moodsList.Clear();
            spotifyBox.Text = "";
        }
    }
}
