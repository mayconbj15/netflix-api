using System;

namespace Netflix.API.Models.Database
{
    public class Episode : Entity
    {
        public int SeasonId { get; set; }

        public string Title { get; set; }

        public long DurationTicks { get; set; }

        public string Synopsis { get; set; }
    }
}