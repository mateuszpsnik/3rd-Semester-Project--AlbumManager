using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Album_Manager.ViewModel;

namespace Album_Manager.View
{
    /// <summary>
    /// Interaction logic for RandomAlbumWindow.xaml
    /// </summary>
    public partial class RandomAlbumWindow : Window
    {
        public RandomAlbumWindow(AlbumViewModel viewModel)
        {
            InitializeComponent();

            Cover.Source = new BitmapImage(new Uri(viewModel.CurrentAlbumImageUrl));
            TitleBlock.Text = viewModel.CurrentAlbumTitle;
            ArtistBlock.Text = viewModel.CurrentAlbumArtist;
            YearBlock.Text = viewModel.CurrentAlbumYear.ToString();

            webBrowser.Navigate(viewModel.CurrentAlbumUri);
        }

        private void anotherAlbumButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
