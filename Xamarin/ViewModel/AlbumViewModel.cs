using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using AlbumManagerMobile.Model;
using Xamarin.Forms;
using System.Runtime.Serialization;
using System.IO;

namespace AlbumManagerMobile.ViewModel
{
    public class AlbumViewModel : INotifyPropertyChanged
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
            ChooseRandomAlbum();
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
                /*
                 if (applyFilters)
                {
                    var collectionToReturn = from item in value
                                             where item.Genres.Contains((Genre)selectedGenre)
                                             where item.DayOrNight.Contains((DayOrNight)selectedDayOrNight)
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
                 */
                var collectionToReturn = from item in value
                                         select item;

                viewCollection = new ObservableCollection<Album>(collectionToReturn);
                if (viewCollection.Count != 0)
                    currentAlbum = viewCollection[0];

                OnPropertyChanged(nameof(Collection));
            }
        }

        public bool ChooseRandomAlbum()
        {
            /*
                if (applyFilters)
                {
                    var filtered = from item in viewCollection
                                   where item.Genres.Contains((Genre)selectedGenre)
                                   where item.DayOrNight.Contains((DayOrNight)selectedDayOrNight)
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
             */
            var filtered = from item in viewCollection
                           select item;
            List<Album> filteredCollection = new List<Album>(filtered);
            currentAlbum = filteredCollection[random.Next(filteredCollection.Count)];
            return true;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void addItems()
        {
            fullCollection.Add(new Album("aaaa", "bbbb", 2122, @"https://upload.wikimedia.org/wikipedia/commons/4/4f/White_Island_2013.jpg",
                new List<Genre>() { Genre.Alternative }, new List<DayOrNight>() { DayOrNight.Day }, new List<Mood>() { Mood.Calm },
                new Uri(@"https://open.spotify.com/album/5YuZ4DjvtZBywtIbHIqtGJ?si=_p-tsxHzT8WhFsTCRm-VxQ")));
        }
    }
}
