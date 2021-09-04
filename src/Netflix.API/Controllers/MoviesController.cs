using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Netflix.API.Models.Database;
using Netflix.API.Repositories;
using Netflix.API.Services;

namespace Netflix.API.Controllers
{
    [Route("/api/[controller]/[action]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly CRUDService<MovieContext> _crudService;

        public MoviesController(CRUDService<MovieContext> crudService)
        {
            _crudService = crudService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Movie movie)
        {
            if (ModelState.IsValid)
            {
                await _crudService.Create(movie);

                return Created($"api/movies/{movie.Id}", movie);
            }

            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Read(int id)
        {
            //GetObjectById
            var movie = await _crudService.GetObjectById<Movie>(id);

            return Ok(movie);
        }

        [HttpPatch]
        [Route("/{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Movie movie)
        {
            //GetObjectById
            var dbMoviel = await _crudService.GetObjectById<Movie>(id);

            if (dbMoviel != default(Movie))
            {
                await _crudService.Update(movie);

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
            var movie = await _crudService.GetObjectById<Movie>(id);

            if (movie != default(Movie))
            {
                await _crudService.Delete(movie);

                return Ok(movie);
            }
            else
            {
                return UnprocessableEntity();
            }
        }
    }
}