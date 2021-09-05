using System.Collections.Generic;
using Netflix.API.Models.Enums;

namespace Netflix.API.Models.Database
{
    public class Serie : MediaEntity
    {
        public int NumbersOfSeasons { get; set; }

        public IEnumerable<Season> Seasons { get; set; }

        public SerieGenre Genre { get; set; }
    }
}