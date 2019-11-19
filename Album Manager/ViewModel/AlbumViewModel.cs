using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Album_Manager.Model;
using Album_Manager.View;

namespace Album_Manager.ViewModel
{
    class AlbumViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Album> fullCollection = new ObservableCollection<Album>();
        private ObservableCollection<Album> viewCollection;
        Random random = new Random();
        public AlbumViewModel()
        {
            addItems();
            applyFilters = true;
            viewCollection = fullCollection;
            Collection = fullCollection;
        }

        private static int selectedGenre;
        private static int selectedDayOrNight;
        private static int selectedMood;
        private static int yearFrom = 1970;
        private static int yearTo = DateTime.Now.Year;

        private bool applyFilters;
        public bool ApplyFilters
        {
            get => applyFilters;
            set
            {
                applyFilters = value;
                OnPropertyChanged(nameof(ApplyFilters));
                Collection = fullCollection;
            }
        }

        private static Album currentAlbum;
        public Album CurrentAlbum
        {
            get => currentAlbum;
            set
            {
                currentAlbum = value as Album;
                OnPropertyChanged(nameof(CurrentAlbum));
            }
        }
        public string CurrentAlbumImageUrl => CurrentAlbum.ImageUrl;
        public string CurrentAlbumTitle => CurrentAlbum.Title;
        public string CurrentAlbumArtist => CurrentAlbum.Artist;
        public int CurrentAlbumYear => CurrentAlbum.Year;
        public Uri CurrentAlbumUri => CurrentAlbum.AlbumUri;

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
                if (applyFilters)
                {
                    var collectionToReturn = from item in value
                                             where item.Genres.Contains((Genre)selectedGenre)
                                             where item.DayOrNight == (DayOrNight)selectedDayOrNight
                                             where item.Mood.Contains((Mood)selectedMood)
                                             where item.Year >= yearFrom
                                             where item.Year <= yearTo
                                             select item;

                    viewCollection = new ObservableCollection<Album>(collectionToReturn);
                    if (viewCollection.Count != 0)
                        currentAlbum = viewCollection[0];
                }
                else
                {
                    var collectionToReturn = from item in value
                                             select item;

                    viewCollection = new ObservableCollection<Album>(collectionToReturn);
                    if (viewCollection.Count != 0)
                        currentAlbum = viewCollection[0];
                }

                OnPropertyChanged(nameof(Collection));
            }
        }

        public bool ChooseRandomAlbum()
        {
            try
            {
                if (applyFilters)
                {
                    var filtered = from item in viewCollection
                                   where item.Genres.Contains((Genre)selectedGenre)
                                   where item.DayOrNight == (DayOrNight)selectedDayOrNight
                                   where item.Mood.Contains((Mood)selectedMood)
                                   where item.Year >= yearFrom
                                   where item.Year <= yearTo
                                   select item;
                    List<Album> filteredCollection = new List<Album>(filtered);
                    currentAlbum = filteredCollection[random.Next(filteredCollection.Count)];
                    return true;
                }
                else
                {
                    var filtered = from item in viewCollection
                                   select item;
                    List<Album> filteredCollection = new List<Album>(filtered);
                    currentAlbum = filteredCollection[random.Next(filteredCollection.Count)];
                    return true;
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                System.Windows.MessageBox.Show("A random album cannot be chosen, if the number of items on the screen equals zero!",
                    "Too little items", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                return false;
            }
        }

        private void addItems()
        {
            fullCollection.Add(new Album("Alternative, Both, Sad", "Artist", 1999,
                @"https://upload.wikimedia.org/wikipedia/commons/c/c2/HMS_Minerva_%281895%29.jpg",
                new List<Genre>() { Genre.Alternative }, DayOrNight.Both, new List<Mood>() { Mood.Sad }, 
                new Uri(@"spotify:album:7aNclGRxTysfh6z0d8671k")));
            fullCollection.Add(new Album("Electronic, Day, Happy&Calm", "Artist", 1999,
                @"https://upload.wikimedia.org/wikipedia/commons/c/c2/HMS_Minerva_%281895%29.jpg",
                new List<Genre>() { Genre.Electronic }, DayOrNight.Day, new List<Mood>() { Mood.Happy, Mood.Calm },
                new Uri(@"spotify:album:7aNclGRxTysfh6z0d8671k")));
            fullCollection.Add(new Album("Alternative, Both, Aggresive", "Artist", 1999,
                @"https://upload.wikimedia.org/wikipedia/commons/c/c2/HMS_Minerva_%281895%29.jpg",
                new List<Genre>() { Genre.Alternative }, DayOrNight.Both, new List<Mood>() { Mood.Aggresive },
                new Uri(@"spotify:album:7aNclGRxTysfh6z0d8671k")));
            fullCollection.Add(new Album("Electronic&Alternative, Both, Aggresive", "Artist", 1999,
                @"https://upload.wikimedia.org/wikipedia/commons/c/c2/HMS_Minerva_%281895%29.jpg",
                new List<Genre>() { Genre.Electronic, Genre.Alternative }, DayOrNight.Both, new List<Mood>() { Mood.Aggresive },
                new Uri(@"spotify:album:7aNclGRxTysfh6z0d8671k")));
            fullCollection.Add(new Album("Alternative, Night, Happy", "Artist", 1999,
                @"https://upload.wikimedia.org/wikipedia/commons/c/c2/HMS_Minerva_%281895%29.jpg",
                new List<Genre>() { Genre.Alternative }, DayOrNight.Night, new List<Mood>() { Mood.Happy },
                new Uri(@"spotify:album:7aNclGRxTysfh6z0d8671k")));
            fullCollection.Add(new Album("Electronic, Both, Aggresive&Sad", "Artist", 1999,
                @"https://upload.wikimedia.org/wikipedia/commons/c/c2/HMS_Minerva_%281895%29.jpg",
                new List<Genre>() { Genre.Electronic }, DayOrNight.Both, new List<Mood>() { Mood.Aggresive, Mood.Sad },
                new Uri(@"spotify:album:7aNclGRxTysfh6z0d8671k")));
            fullCollection.Add(new Album("Alternative, Night, Aggresive", "Artist", 1999,
                @"https://upload.wikimedia.org/wikipedia/commons/c/c2/HMS_Minerva_%281895%29.jpg",
                new List<Genre>() { Genre.Alternative }, DayOrNight.Night, new List<Mood>() { Mood.Aggresive },
                new Uri(@"spotify:album:7aNclGRxTysfh6z0d8671k")));
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
