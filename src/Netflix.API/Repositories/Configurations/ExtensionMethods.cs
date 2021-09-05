using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Netflix.API.Services;

namespace Netflix.API.Repositories.Configurations
{
    public static class ExtensionMethods
    {
        public static void AddMovieCrudService(this IServiceCollection services, string connectionString)
        {
            var movieContextOptions = new DbContextOptionsBuilder<MovieContext>()
                .UseNpgsql(connectionString)
                .Options;

            var movieContext = new MovieContext(movieContextOptions);
            services.AddScoped<MovieContext>((sp) => movieContext);

            services.AddScoped<ICRUDService<MovieContext>>((sp) => new CRUDService<MovieContext>(movieContext));
        }

        public static void AddSerieCrudService(this IServiceCollection services, string connectionString)
        {
            var serieContextOptions = new DbContextOptionsBuilder<SerieContext>()
                .UseNpgsql(connectionString)
                .Options;

            var serieContext = new SerieContext(serieContextOptions);
            services.AddScoped<SerieContext>((sp) => serieContext);

            services.AddScoped<ICRUDService<SerieContext>>((sp) => new CRUDService<SerieContext>(serieContext));
        }
    }
}