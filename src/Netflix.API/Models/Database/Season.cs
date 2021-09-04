using System.Collections.Generic;

namespace Netflix.API.Models.Database
{
    public class Season : Entity
    {
        public int SerieId { get; set; }
        public Serie Serie { get; set; }

        public int SerialNumber { get; set; }

        public IEnumerable<Episode> Episodes { get; set; }
    }
}