using System;

namespace Netflix.API.Models.Database
{
    public class Movie : MediaEntity
    {
        public TimeSpan Duration { get; set; }
    }
}