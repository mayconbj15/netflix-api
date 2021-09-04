using Microsoft.EntityFrameworkCore;
using Netflix.API.Models.Database;
using Netflix.API.Repositories.Configurations;

namespace Netflix.API.Repositories
{
    public class MovieContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }

        public MovieContext(DbContextOptions<MovieContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new MovieEntityTypeConfiguration()
                .Configure(modelBuilder.Entity<Movie>());
        }
    }
}