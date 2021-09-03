using System.Collections.Generic;

namespace Netflix.API.Models.Database
{
    public class Serie : MediaEntity
    {
        public int NumbersOfSeasons { get; set; }

        public IEnumerable<Season> Seasons { get; set; }
    }
}