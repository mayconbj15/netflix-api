using System;

namespace Netflix.API.Models.Database
{
    public abstract class Entity
    {
        public string Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}