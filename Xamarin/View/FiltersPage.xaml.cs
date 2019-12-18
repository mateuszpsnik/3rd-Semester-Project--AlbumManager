using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlbumManagerMobile.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlbumManagerMobile.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FiltersPage : ContentPage
    {
        public FiltersPage()
        {
            InitializeComponent();

            generatePickers();
        }

        private void generatePickers()
        {
            foreach (Genre genrePickerItem in Enum.GetValues(typeof(Genre)))
            {
                genresPicker.Items.Add(genrePickerItem.ToString());
            }

            
            foreach (DayOrNight dayNightPickerItem in Enum.GetValues(typeof(DayOrNight)))
            {
                dayNightPicker.Items.Add(dayNightPickerItem.ToString());
            }

            
            foreach (Mood moodPickerItem in Enum.GetValues(typeof(Mood)))
            {
                moodPicker.Items.Add(moodPickerItem.ToString());
            }
        }

        private void startButton_Clicked(object sender, EventArgs e)
        {

        }

        private async void backHomeButton_Clicked(object sender, EventArgs e)
        {
            StartPage startPage = new StartPage();
            await Navigation.PushModalAsync(startPage);
        }
    }
}