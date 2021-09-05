using System;
using Netflix.API.Models.Enums;

namespace Netflix.API.Models.Database
{
    public class Movie : MediaEntity
    {
        public long DurationTicks { get; set; }

        public MovieGenre Genre { get; set; }
    }
}