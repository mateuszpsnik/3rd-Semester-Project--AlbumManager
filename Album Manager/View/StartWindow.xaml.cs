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
using Album_Manager.ViewModel;
using Album_Manager.Model;

namespace Album_Manager.View
{
    /// <summary>
    /// Interaction logic for StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();

            generateCombos();
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

        private void randomButton_Click(object sender, RoutedEventArgs e)
        {
            RandomAlbumWindow randomAlbumWindow = new RandomAlbumWindow();
            randomAlbumWindow.Show();
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            EditCollectionWindow editCollectionWindow = new EditCollectionWindow();
            editCollectionWindow.Show();
            this.Close();
        }
    }
}
