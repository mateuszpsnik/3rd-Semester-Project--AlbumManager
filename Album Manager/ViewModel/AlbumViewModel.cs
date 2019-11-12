using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Album_Manager.Model;

namespace Album_Manager.ViewModel
{
    class AlbumViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Album> fullCollection = new ObservableCollection<Album>();
        private ObservableCollection<Album> viewCollection;
        public AlbumViewModel()
        {
            addItems();
            viewCollection = fullCollection;
        }

        private int selectedGenre;
        private int selectedDayOrNight;
        private int selectedMood;
        private int yearFrom = 1970;
        private int yearTo = DateTime.Now.Year;

        public Album CurrentAlbum => fullCollection[2];
        public string CurrentAlbumImageUrl => CurrentAlbum.ImageUrl;
        public string CurrentAlbumTitle => CurrentAlbum.Title;
        public string CurrentAlbumArtist => CurrentAlbum.Artist;
        public int CurrentAlbumYear => CurrentAlbum.Year;

        public int SelectedGenre 
        { 
            get => selectedGenre; 
            set 
            {
                selectedGenre = value;
                OnPropertyChanged(nameof(SelectedGenre));
                Collection = fullCollection;
            } 
        }
        public int SelectedDayOrNight
        {
            get => selectedDayOrNight;
            set
            {
                selectedDayOrNight = value;
                OnPropertyChanged(nameof(SelectedDayOrNight));
                Collection = fullCollection;
            }
        }
        public int SelectedMood
        {
            get => selectedMood;
            set
            {
                selectedMood = value;
                OnPropertyChanged(nameof(SelectedMood));
                Collection = fullCollection;
            }
        }
        public int YearFrom
        {
            get => yearFrom;
            set
            {
                yearFrom = value;
                OnPropertyChanged(nameof(YearFrom));
                Collection = fullCollection;
            }
        }
        public int YearTo
        {
            get => yearTo;
            set
            {
                yearTo = value;
                OnPropertyChanged(nameof(YearTo));
                Collection = fullCollection;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Album> Collection
        {
            get => viewCollection;
            set
            {
                var collectionToReturn = from item in value
                                             where item.Genres.Contains((Genre)selectedGenre)
                                             where item.DayOrNight == (DayOrNight)selectedDayOrNight
                                             where item.Mood.Contains((Mood)selectedMood)
                                             where item.Year >= yearFrom
                                             where item.Year <= yearTo
                                         select item;

                viewCollection = new ObservableCollection<Album>(collectionToReturn);

                OnPropertyChanged(nameof(Collection));
            }
        }

        private void addItems()
        {
            fullCollection.Add(new Album("Alternative, Both, Sad", "Artist", 1999,
                @"https://upload.wikimedia.org/wikipedia/commons/c/c2/HMS_Minerva_%281895%29.jpg",
                new List<Genre>() { Genre.Alternative }, DayOrNight.Both, new List<Mood>() { Mood.Sad }));
            fullCollection.Add(new Album("Electronic, Day, Happy&Calm", "Artist", 1999,
                @"https://upload.wikimedia.org/wikipedia/commons/c/c2/HMS_Minerva_%281895%29.jpg",
                new List<Genre>() { Genre.Electronic }, DayOrNight.Day, new List<Mood>() { Mood.Happy, Mood.Calm }));
            fullCollection.Add(new Album("Alternative, Both, Aggresive", "Artist", 1999,
                @"https://upload.wikimedia.org/wikipedia/commons/c/c2/HMS_Minerva_%281895%29.jpg",
                new List<Genre>() { Genre.Alternative }, DayOrNight.Both, new List<Mood>() { Mood.Aggresive }));
            fullCollection.Add(new Album("Electronic&Alternative, Both, Aggresive", "Artist", 1999,
                @"https://upload.wikimedia.org/wikipedia/commons/c/c2/HMS_Minerva_%281895%29.jpg",
                new List<Genre>() { Genre.Electronic, Genre.Alternative }, DayOrNight.Both, new List<Mood>() { Mood.Aggresive }));
            fullCollection.Add(new Album("Alternative, Night, Happy", "Artist", 1999,
                @"https://upload.wikimedia.org/wikipedia/commons/c/c2/HMS_Minerva_%281895%29.jpg",
                new List<Genre>() { Genre.Alternative }, DayOrNight.Night, new List<Mood>() { Mood.Happy }));
            fullCollection.Add(new Album("Electronic, Both, Aggresive&Sad", "Artist", 1999,
                @"https://upload.wikimedia.org/wikipedia/commons/c/c2/HMS_Minerva_%281895%29.jpg",
                new List<Genre>() { Genre.Electronic }, DayOrNight.Both, new List<Mood>() { Mood.Aggresive, Mood.Sad }));
            fullCollection.Add(new Album("Alternative, Night, Aggresive", "Artist", 1999,
                @"https://upload.wikimedia.org/wikipedia/commons/c/c2/HMS_Minerva_%281895%29.jpg",
                new List<Genre>() { Genre.Alternative }, DayOrNight.Night, new List<Mood>() { Mood.Aggresive }));
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
