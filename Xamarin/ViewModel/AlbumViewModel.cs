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
            applyFilters = false;
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
            }
        }

        public bool ChooseRandomAlbum()
        {
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
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void addItems()
        {
            fullCollection.Add(new Album("OK Computer", "Radiohead", 1997, @"https://upload.wikimedia.org/wikipedia/en/a/a1/Radiohead.okcomputer.albumart.jpg",
                new List<Genre>() { Genre.Rock, Genre.Alternative }, new List<DayOrNight>() { DayOrNight.Day, DayOrNight.Night },
                new List<Mood>() { Mood.Sad, Mood.Calm },
                new Uri(@"https://open.spotify.com/album/7dxKtc08dYeRVHt3p9CZJn?si=nXw84AYxSgavSZJoEyVT3Q")));
            fullCollection.Add(new Album("A Moon Shaped Pool", "Radiohead", 2016,
                @"https://upload.wikimedia.org/wikipedia/en/c/c1/A_Moon_Shaped_Pool.jpg",
                new List<Genre>() { Genre.Alternative }, new List<DayOrNight>() { DayOrNight.Day, DayOrNight.Night },
                new List<Mood>() { Mood.Calm, Mood.Sad },
                new Uri(@"https://open.spotify.com/album/6vuykQgDLUCiZ7YggIpLM9?si=5Q66jCZ5Q4CcR0ifDCbIfQ")));
            fullCollection.Add(new Album("Kid A", "Radiohead", 2000,
                @"https://upload.wikimedia.org/wikipedia/en/b/b5/Radiohead.kida.albumart.jpg",
                new List<Genre>() { Genre.Alternative, Genre.Electronic }, new List<DayOrNight>() { DayOrNight.Night },
                new List<Mood>() { Mood.Calm, Mood.Sad },
                new Uri(@"https://open.spotify.com/album/19RUXBFyM4PpmrLRdtqWbp?si=5B5OEjAsSZuk5mRROr-Ozw")));
            fullCollection.Add(new Album("Amnesiac", "Radiohead", 2001,
                @"https://upload.wikimedia.org/wikipedia/en/8/8c/Radiohead_-_Amnesiac_cover.png",
                new List<Genre>() { Genre.Alternative }, new List<DayOrNight>() { DayOrNight.Night },
                new List<Mood>() { Mood.Calm, Mood.Sad },
                new Uri(@"https://open.spotify.com/album/6V9YnBmFjWmXCBaUVRCVXP?si=ZRDkPBnUTq-3fWXhAmjr1Q")));
            fullCollection.Add(new Album("Parachutes", "Coldplay", 2000,
                @"https://upload.wikimedia.org/wikipedia/en/5/57/Coldplayparachutesalbumcover.jpg",
                new List<Genre>() { Genre.Alternative, Genre.Rock }, new List<DayOrNight>() { DayOrNight.Night },
                new List<Mood>() { Mood.Calm, Mood.Sad, Mood.Romantic },
                new Uri(@"https://open.spotify.com/album/6ZG5lRT77aJ3btmArcykra?si=cnGjzbxJSAaKujwZ_gxQNQ")));
            fullCollection.Add(new Album("In Rainbows", "Radiohead", 2007,
                @"https://upload.wikimedia.org/wikipedia/en/2/2e/In_Rainbows_Official_Cover.jpg",
                new List<Genre>() { Genre.Alternative, Genre.Rock }, new List<DayOrNight>() { DayOrNight.Night },
                new List<Mood>() { Mood.Calm, Mood.Sad, Mood.Romantic },
                new Uri(@"https://open.spotify.com/album/7eyQXxuf2nGj9d2367Gi5f?si=LjtD2bGkTmyNiaLk78moAA")));
            fullCollection.Add(new Album("Currents", "Tame Impala", 2015,
                @"https://upload.wikimedia.org/wikipedia/en/9/9b/Tame_Impala_-_Currents.png",
                new List<Genre>() { Genre.Alternative, Genre.Electronic }, new List<DayOrNight>() { DayOrNight.Night },
                new List<Mood>() { Mood.Calm, Mood.Sad, Mood.Energetic, Mood.Danceable, Mood.Romantic, Mood.Driving, Mood.Party },
                new Uri(@"https://open.spotify.com/album/79dL7FLiJFOO0EoehUHQBv?si=9GETgtPQQ_WTytmEIC6fgg")));
            fullCollection.Add(new Album("Posłuchaj to do Ciebie", "Kult", 1987,
                @"https://lastfm.freetls.fastly.net/i/u/770x0/f59c007401fd90f3ab483390c2fb760e.webp#f59c007401fd90f3ab483390c2fb760e",
                new List<Genre>() { Genre.Alternative, Genre.Rock }, new List<DayOrNight>() { DayOrNight.Day, DayOrNight.Night },
                new List<Mood>() { Mood.Energetic, Mood.Driving },
                new Uri(@"https://open.spotify.com/album/1aeNPtcRju5nXhQZYGYzdn?si=kQ9_RhAxTDqUlLTW2Pbo0w")));
            fullCollection.Add(new Album("Humbug", "Arctic Monkeys", 2009,
                @"https://upload.wikimedia.org/wikipedia/en/2/20/Arcticmonkeys-humbug.jpg",
                new List<Genre>() { Genre.Rock, Genre.Alternative }, new List<DayOrNight>() { DayOrNight.Night },
                new List<Mood>() { Mood.Energetic, Mood.Danceable, Mood.Romantic, Mood.Driving, Mood.Party },
                new Uri(@"https://open.spotify.com/album/5IEoiwkThhRmSMBANhpxl2?si=KzRlpLfTSyepBZ0EtBMCOA")));
            fullCollection.Add(new Album("Queens of the Stone Age", "Queens of the Stone Age", 1998,
                @"https://upload.wikimedia.org/wikipedia/en/f/f0/Queens_of_the_Stone_Age_%28Queens_of_the_Stone_Age_album_-_cover_art%29.jpg",
                new List<Genre>() { Genre.Alternative, Genre.Rock }, new List<DayOrNight>() { DayOrNight.Day, DayOrNight.Night },
                new List<Mood>() { Mood.Energetic, Mood.Driving, Mood.Party, Mood.Summer },
                new Uri(@"https://open.spotify.com/album/4ZVYZI1wY4TmLOQ3as1UNI?si=uT2nNBziRMKynvNgdzAuRQ")));
            fullCollection.Add(new Album("Villains", "Queens of the Stone Age", 2017,
                @"https://upload.wikimedia.org/wikipedia/en/7/72/Villains_cover_artwork.png",
                new List<Genre>() { Genre.Alternative, Genre.Rock }, new List<DayOrNight>() { DayOrNight.Day, DayOrNight.Night },
                new List<Mood>() { Mood.Danceable, Mood.Energetic, Mood.Summer, Mood.Driving, Mood.Party },
                new Uri(@"https://open.spotify.com/album/6JdX9MGiEMypqYLMKyIE8a?si=FlrKJ6aTTXuFOSOqSdJDBQ")));
            fullCollection.Add(new Album("How Did We Get So Dark?", "Royal Blood", 2017,
                @"https://upload.wikimedia.org/wikipedia/en/b/ba/Royal-Blood-How-Did-We-Get-So-Dark.jpg",
                new List<Genre>() { Genre.Alternative, Genre.Rock }, new List<DayOrNight>() { DayOrNight.Day, DayOrNight.Night },
                new List<Mood>() { Mood.Energetic, Mood.Driving, Mood.Party, Mood.Summer },
                new Uri(@"https://open.spotify.com/album/3Rz6kF8eGqrDOEteo5YsBj?si=gLUgqwU8RmOfZFCj7EPSVA")));
            fullCollection.Add(new Album("The Bends", "Radiohead", 1995,
                @"https://upload.wikimedia.org/wikipedia/en/8/8b/Radiohead.bends.albumart.jpg",
                new List<Genre>() { Genre.Rock, Genre.Alternative }, new List<DayOrNight>() { DayOrNight.Night },
                new List<Mood>() { Mood.Calm, Mood.Sad },
                new Uri(@"https://open.spotify.com/album/500FEaUzn8lN9zWFyZG5C2?si=Axt1Zjo6Tp21JEI6Lut0pQ")));
            fullCollection.Add(new Album("Selected Ambient Works 85-92", "Aphex Twin", 1992,
                @"https://upload.wikimedia.org/wikipedia/en/8/82/Selected_Ambient_Works_85-92.png",
                new List<Genre>() { Genre.Electronic, Genre.Instrumental }, new List<DayOrNight>() { DayOrNight.Day, DayOrNight.Night },
                new List<Mood>() { Mood.Calm, Mood.Energetic },
                new Uri(@"https://open.spotify.com/album/7aNclGRxTysfh6z0d8671k?si=5NZ7MBx3Tf-ttT-U-icHdA")));
            fullCollection.Add(new Album("Lateralus", "Tool", 2001,
                @"https://upload.wikimedia.org/wikipedia/en/6/63/Tool_-_Lateralus.jpg",
                new List<Genre>() { Genre.Metal }, new List<DayOrNight>() { DayOrNight.Night },
                new List<Mood>() { Mood.Sad, Mood.Aggresive, Mood.Energetic },
                new Uri(@"https://open.spotify.com/album/5l5m1hnH4punS1GQXgEi3T?si=EWjzKnjlQF-zmipznSeQ9w")));
            fullCollection.Add(new Album("...Like Clockwork", "Queens of the Stone Age", 2013,
                @"https://upload.wikimedia.org/wikipedia/en/1/16/Queens_of_the_Stone_Age_-_%E2%80%A6Like_Clockwork.png",
                new List<Genre>() { Genre.Alternative, Genre.Rock }, new List<DayOrNight>() { DayOrNight.Day, DayOrNight.Night },
                new List<Mood>() { Mood.Energetic, Mood.Driving, Mood.Party, Mood.Summer },
                new Uri(@"https://open.spotify.com/album/06S2JBsr4U1Dz3YaenPdVq?si=Mro35AWeTtOtm8WCkREpNQ")));
            fullCollection.Add(new Album("Songs for the Deaf", "Queens of the Stone Age", 2002,
                @"https://upload.wikimedia.org/wikipedia/en/0/01/Queens_of_the_Stone_Age_-_Songs_for_the_Deaf.png",
                new List<Genre>() { Genre.Alternative, Genre.Rock }, new List<DayOrNight>() { DayOrNight.Day, DayOrNight.Night },
                new List<Mood>() { Mood.Energetic, Mood.Driving, Mood.Summer, Mood.Party },
                new Uri(@"https://open.spotify.com/album/4w3NeXtywU398NYW4903rY?si=do6x5tynSCm-yp6ZWQoOqw")));
            fullCollection.Add(new Album("Blood Sugar Sex Magik", "Red Hot Chili Peppers", 1991,
                @"https://upload.wikimedia.org/wikipedia/en/5/5e/RHCP-BSSM.jpg",
                new List<Genre>() { Genre.Alternative, Genre.Rock }, new List<DayOrNight>() { DayOrNight.Day },
                new List<Mood>() { Mood.Energetic, Mood.Happy, Mood.Driving, Mood.Party, Mood.Summer },
                new Uri(@"https://open.spotify.com/album/30Perjew8HyGkdSmqguYyg?si=WPt3ltmTQ2yfcicmR7BxiQ")));
            fullCollection.Add(new Album("13", "Blur", 1999,
                @"https://upload.wikimedia.org/wikipedia/en/4/4c/13_%28Blur_album_-_cover_art%29.jpg",
                new List<Genre>() { Genre.Alternative, Genre.Electronic, Genre.Rock }, new List<DayOrNight>() { DayOrNight.Night },
                new List<Mood>() { Mood.Calm },
                new Uri(@"https://open.spotify.com/album/5YuZ4DjvtZBywtIbHIqtGJ?si=EiaynA1CTfGls6rZKI35Qg")));
            fullCollection.Add(new Album("Paranoid", "Black Sabbath", 1970,
                @"https://upload.wikimedia.org/wikipedia/en/6/64/Black_Sabbath_-_Paranoid.jpg",
                new List<Genre>() { Genre.Rock, Genre.Metal }, new List<DayOrNight>() { DayOrNight.Night },
                new List<Mood>() { Mood.Energetic, Mood.Driving },
                new Uri(@"https://open.spotify.com/album/132qAo1cDiEJdA3fv4xyNK?si=ylWnu8HzTI-_jNEHLoek5A")));
            fullCollection.Add(new Album("Spokojnie", "Kult", 1988,
                @"https://lastfm.freetls.fastly.net/i/u/770x0/cf552f0a64e9468d87c147ead8e15adc.webp#cf552f0a64e9468d87c147ead8e15adc",
                new List<Genre>() { Genre.Alternative, Genre.Rock }, new List<DayOrNight>() { DayOrNight.Night, DayOrNight.Day },
                new List<Mood>() { Mood.Energetic, Mood.Driving },
                new Uri(@"https://open.spotify.com/album/4G5x7bVzIa09nPgdPEpokm?si=9Akj_sEBQx2mo_shSQMgjg")));
            fullCollection.Add(new Album("Royal Blood", "Royal Blood", 2014,
                @"https://upload.wikimedia.org/wikipedia/en/b/b0/Royal_Blood_-_Royal_Blood_%28Artwork%29.jpg",
                new List<Genre>() { Genre.Alternative, Genre.Rock }, new List<DayOrNight>() { DayOrNight.Day, DayOrNight.Night },
                new List<Mood>() { Mood.Energetic, Mood.Driving, Mood.Party, Mood.Summer },
                new Uri(@"https://open.spotify.com/album/0BFzNaeaNv4mahOzwZFGHK?si=lwtJbI6yR3KbHGHfOQn-yw")));

        }
    }
}
