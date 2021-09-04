using System;

namespace Netflix.API.Models.Database
{
    public class Movie : MediaEntity
    {
        public long DurationTicks { get; set; }
    }
}