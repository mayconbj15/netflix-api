using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Netflix.API.Models.Database
{
    public class Season : Entity
    {
        public int SerieId { get; set; }

        [JsonIgnore] //Fix #1
        public Serie Serie { get; set; }

        public int SerialNumber { get; set; }

        public IEnumerable<Episode> Episodes { get; set; }
    }
}