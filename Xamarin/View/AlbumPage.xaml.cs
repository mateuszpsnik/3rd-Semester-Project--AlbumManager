using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlbumManagerMobile.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlbumManagerMobile.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlbumPage : ContentPage
    {
        AlbumViewModel viewModel;
        public AlbumPage(AlbumViewModel viewModel)
        {
            InitializeComponent();

            this.viewModel = viewModel;

            Cover.Source = new UriImageSource() { Uri = new Uri(viewModel.CurrentAlbumImageUrl) };
            TitleBlock.Text = viewModel.CurrentAlbumTitle;
            ArtistBlock.Text = viewModel.CurrentAlbumArtist;
            YearBlock.Text = viewModel.CurrentAlbumYear.ToString();
        }

        private void SpotifyButton_Clicked(object sender, EventArgs e)
        {
            Device.OpenUri(viewModel.CurrentAlbumUri);
        }

        private void NewRandomButton_Clicked(object sender, EventArgs e)
        {
            AlbumViewModel albumViewModel = new AlbumViewModel();
            //albumViewModel.ApplyFilters = (bool)applyFiltersCheckbox.IsChecked;
            bool success = albumViewModel.ChooseRandomAlbum();
            if (success)
            {
                AlbumPage albumPage = new AlbumPage(albumViewModel);
                Navigation.PushModalAsync(albumPage);
            }
        }
    }
}