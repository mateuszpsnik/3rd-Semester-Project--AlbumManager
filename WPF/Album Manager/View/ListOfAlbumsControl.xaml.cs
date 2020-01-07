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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Album_Manager.ViewModel;
using Album_Manager.Model;

namespace Album_Manager.View
{
    /// <summary>
    /// Interaction logic for ListOfAlbumsControl.xaml
    /// </summary>
    public partial class ListOfAlbumsControl : UserControl
    {
        public ListOfAlbumsControl()
        {
            InitializeComponent();
        }

        private void listView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            AlbumViewModel viewModel = new AlbumViewModel();
            viewModel.CurrentAlbum = listView.SelectedItem as Album;
            bool filters = viewModel.ApplyFilters;

            RandomAlbumWindow randomAlbumWindow = new RandomAlbumWindow(viewModel, filters);
            randomAlbumWindow.Show();
        }
    }
}
