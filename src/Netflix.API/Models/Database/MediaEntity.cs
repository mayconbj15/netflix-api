using Netflix.API.Models.Enums;

namespace Netflix.API.Models.Database
{
    public abstract class MediaEntity : Entity
    {
        public string Title { get; set; }

        public int Year { get; set; }

        public AgeRating AgeRating { get; set; }

        public VideoQuality VideoQuality { get; set; }

        public string Synopsis { get; set; }
    }
}