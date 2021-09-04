using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Netflix.API.Models.Database;
using Netflix.API.Repositories;

namespace Netflix.API.Controllers
{
    [Route("/api/[controller]/[action]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MovieContext _movieContext;

        public MoviesController(MovieContext movieContext)
        {
            _movieContext = movieContext;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Movie movie)
        {
            if (ModelState.IsValid)
            {
                await _movieContext.AddAsync(movie);
                await _movieContext.SaveChangesAsync();

                return Created($"api/movies/{movie.Id}", movie);
            }

            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Read(int id)
        {
            var movie = await _movieContext.Movies
                .FirstOrDefaultAsync(m => m.Id == id);

            return Ok(movie);
        }

        [HttpPatch]
        [Route("/{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Movie movie)
        {
            var dbMoviel = await _movieContext.Movies
                .FirstOrDefaultAsync(m => m.Id == id);

            if (dbMoviel != default(Movie))
            {
                _movieContext.Update(movie);
                await _movieContext.SaveChangesAsync();

                return Ok(movie);
            }
            else
            {
                return UnprocessableEntity();
            }
        }

        [HttpDelete]
        [Route("/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var movie = await _movieContext.Movies
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie != default(Movie))
            {
                _movieContext.Remove(movie);
                await _movieContext.SaveChangesAsync();

                return Ok(movie);
            }
            else
            {
                return UnprocessableEntity();
            }
        }
    }
}