using Microsoft.EntityFrameworkCore;
using Netflix.API.Models.Database;

namespace Netflix.API.Repositories
{
    public class SerieContext : DbContext
    {
        public DbSet<Serie> Series { get; set; }

        public DbSet<Season> Seasons { get; set; }

        public DbSet<Episode> Episodes { get; set; }

        public SerieContext(DbContextOptions<SerieContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}