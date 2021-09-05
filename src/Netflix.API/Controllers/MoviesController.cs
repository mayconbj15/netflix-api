using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Netflix.API.Utils.Constants;
using Netflix.API.Models;
using Netflix.API.Models.Database;
using Netflix.API.Repositories;
using Netflix.API.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace Netflix.API.Controllers
{
    [Route("/api/[controller]/[action]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ICRUDService<MovieContext> _crudService;

        public MoviesController(ICRUDService<MovieContext> crudService)
        {
            _crudService = crudService;
        }

        /// <summary>
        /// List all movies
        /// </summary>
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, ResponseDescriptions.MovieAllOk, typeof(IEnumerable<Movie>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, ResponseDescriptions.InternalServerError, typeof(ExceptionResponse))]
        public async Task<IActionResult> All()
        {
            return Ok(await _crudService.GetAll<Movie>());
        }

        /// <summary>
        /// Create a new movie
        /// </summary>
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, ResponseDescriptions.CreateCreated, typeof(Movie))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, ResponseDescriptions.CreateBadRequest)]
        public async Task<IActionResult> Create([FromBody] Movie movie)
        {
            if (ModelState.IsValid)
            {
                await _crudService.Create(movie);

                return Created($"api/movies/{movie.Id}", movie);
            }

            return BadRequest();
        }

        /// <summary>
        /// Get a movie by id
        /// </summary>
        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, ResponseDescriptions.ReadOk, typeof(Movie))]
        [SwaggerResponse((int)HttpStatusCode.NoContent, ResponseDescriptions.ReadNoContent)]
        public async Task<IActionResult> Read(int id)
        {
            var movie = await _crudService.GetObjectById<Movie>(id);

            return Ok(movie);
        }

        /// <summary>
        /// Update a movie by id
        /// </summary>
        [HttpPatch]
        [Route("{id:int}")]
        [SwaggerResponse((int)HttpStatusCode.OK, ResponseDescriptions.UpdateOk, typeof(Movie))]
        [SwaggerResponse((int)HttpStatusCode.UnprocessableEntity, ResponseDescriptions.UpdateUnprocessableEntity, typeof(Movie))]
        public async Task<IActionResult> Update(int id, [FromBody] Movie movie)
        {
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

        /// <summary>
        /// Delete a movie by id
        /// </summary>
        [HttpDelete]
        [Route("{id:int}")]
        [SwaggerResponse((int)HttpStatusCode.OK, ResponseDescriptions.DeleteOk, typeof(Movie))]
        [SwaggerResponse((int)HttpStatusCode.UnprocessableEntity, ResponseDescriptions.DeleteUnprocessableEntity, typeof(Movie))]
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