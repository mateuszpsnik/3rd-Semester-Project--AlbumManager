using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AlbumManagerMobile.ViewModel;

namespace AlbumManagerMobile.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage : ContentPage
    {
        public StartPage()
        {
            InitializeComponent();

        }
        private async void StartButton_Clicked(object sender, EventArgs e)
        {
            AlbumViewModel albumViewModel = new AlbumViewModel();
            //albumViewModel.ApplyFilters = (bool)applyFiltersCheckbox.IsChecked;
            bool success = albumViewModel.ChooseRandomAlbum();
            if (success)
            {
                AlbumPage albumPage = new AlbumPage(albumViewModel);
                await Navigation.PushModalAsync(albumPage);
            }
        }

        private async void FiltersButton_Clicked(object sender, EventArgs e)
        {
            FiltersPage filtersPage = new FiltersPage();
            await Navigation.PushModalAsync(filtersPage);
        }
    }
}