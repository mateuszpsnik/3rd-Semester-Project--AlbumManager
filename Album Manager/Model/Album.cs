using System;
using System.Collections.Generic;
using System.Text;

namespace Album_Manager.Model
{
    class Album
    {
        public string Title { get; private set; }
        public string Artist { get; private set; }
        public int Year { get; private set; }
        public string ImageUrl { get; private set; }
        public List<Genre> Genres { get; private set; }
        public DayOrNight DayOrNight { get; private set; }
        public List<Mood> Mood { get; private set; }

        public Album(string title, string artist, int year, string imageUrl, List<Genre> genres,
            DayOrNight dayOrNight, List<Mood> mood)
        {
            Title = title;
            Artist = artist;
            Year = year;
            ImageUrl = imageUrl;
            Genres = genres;
            DayOrNight = dayOrNight;
            Mood = mood;
        }
            
    }
}
