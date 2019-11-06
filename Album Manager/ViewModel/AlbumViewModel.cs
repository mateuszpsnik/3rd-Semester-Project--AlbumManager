using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Album_Manager.Model;

namespace Album_Manager.ViewModel
{
    class AlbumViewModel
    {
        public AlbumViewModel()
        {
            for (int i = 0; i < 50; i++)
            {
                collection.Add(new Album("Album", "Artist", 1999,
                    @"https://upload.wikimedia.org/wikipedia/commons/c/c2/HMS_Minerva_%281895%29.jpg",
                    new List<Genre>() { Genre.Alternative }, DayOrNight.Both, new List<Mood>() { Mood.Aggresive }));
            }
        }

        public Album CurrentAlbum => collection[20];
        public string CurrentAlbumImageUrl => CurrentAlbum.ImageUrl;
        public string CurrentAlbumTitle => CurrentAlbum.Title;
        public string CurrentAlbumArtist => CurrentAlbum.Artist;
        public int CurrentAlbumYear => CurrentAlbum.Year;
        public string SomeString => "gówno";

        private ObservableCollection<Album> collection = new ObservableCollection<Album>();     
        public ObservableCollection<Album> Collection => collection;
    }
}
