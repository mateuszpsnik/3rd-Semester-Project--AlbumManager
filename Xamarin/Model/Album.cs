using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace AlbumManagerMobile.Model
{
    [DataContract]
    public class Album
    {
        [DataMember]
        public string Title { get; private set; }
        [DataMember]
        public string Artist { get; private set; }
        [DataMember]
        public int Year { get; private set; }
        [DataMember]
        public string ImageUrl { get; private set; }
        [DataMember]
        public List<Genre> Genres { get; private set; }
        [DataMember]
        public List<DayOrNight> DayOrNight { get; private set; }
        [DataMember]
        public List<Mood> Mood { get; private set; }
        [DataMember]
        public Uri AlbumUri { get; private set; }

        public Album(string title, string artist, int year, string imageUrl, List<Genre> genres,
            List<DayOrNight> dayOrNight, List<Mood> mood, Uri albumUri)
        {
            Title = title;
            Artist = artist;
            Year = year;
            ImageUrl = imageUrl;
            Genres = genres;
            DayOrNight = dayOrNight;
            Mood = mood;
            AlbumUri = albumUri;
        }
            
    }
}
