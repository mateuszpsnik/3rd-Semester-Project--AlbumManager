using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
        bool filters;
        public AlbumPage(AlbumViewModel viewModel, bool filters)
        {
            InitializeComponent();
            this.viewModel = viewModel;
            this.filters = filters;

            WebClient Client = new WebClient();
            var byteArray = Client.DownloadData(viewModel.CurrentAlbumImageUrl.ToString());
            Cover.Source = ImageSource.FromStream(() => new MemoryStream(byteArray));

            TitleBlock.Text = viewModel.CurrentAlbumTitle;
            ArtistBlock.Text = viewModel.CurrentAlbumArtist;
            YearBlock.Text = viewModel.CurrentAlbumYear.ToString();
        }

        private void SpotifyButton_Clicked(object sender, EventArgs e)
        {
            Device.OpenUri(viewModel.CurrentAlbumUri);
        }

        private async void NewRandomButton_Clicked(object sender, EventArgs e)
        {
            AlbumViewModel albumViewModel = new AlbumViewModel();
            albumViewModel.ApplyFilters = filters;
            if (filters)
            {
                albumViewModel.SelectedDayOrNight = viewModel.SelectedDayOrNight;
                albumViewModel.SelectedGenre = viewModel.SelectedGenre;
                albumViewModel.SelectedMood = viewModel.SelectedMood;
            }
            bool success = albumViewModel.ChooseRandomAlbum();
            if (success)
            {
                AlbumPage albumPage = new AlbumPage(albumViewModel, filters);
                await Navigation.PushModalAsync(albumPage);
            }
        }
    }
}