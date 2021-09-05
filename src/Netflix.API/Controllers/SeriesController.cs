using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Netflix.API.Models;
using Netflix.API.Models.Database;
using Netflix.API.Repositories;
using Netflix.API.Services;
using Netflix.API.Utils.Constants;
using Swashbuckle.AspNetCore.Annotations;

namespace Netflix.API.Controllers
{
    [Route("/api/[controller]/[action]")]
    [ApiController]
    public class SeriesController : ControllerBase
    {
        private readonly ICRUDService<SerieContext> _crudService;

        public SeriesController(ICRUDService<SerieContext> crudService)
        {
            _crudService = crudService;
        }

        /// <summary>
        /// List all series
        /// </summary>
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, ResponseDescriptions.SerieAllOk, typeof(IEnumerable<Serie>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, ResponseDescriptions.InternalServerError, typeof(ExceptionResponse))]
        public async Task<IActionResult> All()
        {
            return Ok(await _crudService.GetAll<Serie>());
        }

        /// <summary>
        /// Create a new serie
        /// </summary>
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, ResponseDescriptions.CreateCreated, typeof(Serie))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, ResponseDescriptions.CreateBadRequest)]
        public async Task<IActionResult> Create([FromBody] Serie serie)
        {
            if (ModelState.IsValid)
            {
                await _crudService.Create(serie);

                return Created($"api/series/{serie.Id}", serie);
            }

            return BadRequest();
        }

        /// <summary>
        /// Get a serie by id
        /// </summary>
        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, ResponseDescriptions.ReadOk, typeof(Serie))]
        [SwaggerResponse((int)HttpStatusCode.NoContent, ResponseDescriptions.ReadNoContent)]
        public async Task<IActionResult> Read(int id)
        {
            var serie = await _crudService.GetObjectById<Serie>(id);

            return Ok(serie);
        }

        /// <summary>
        /// Update a serie by id
        /// </summary>
        [HttpPatch]
        [Route("{id:int}")]
        [SwaggerResponse((int)HttpStatusCode.OK, ResponseDescriptions.UpdateOk, typeof(Serie))]
        [SwaggerResponse((int)HttpStatusCode.UnprocessableEntity, ResponseDescriptions.UpdateUnprocessableEntity, typeof(Serie))]
        public async Task<IActionResult> Update(int id, [FromBody] Serie serie)
        {
            var dbSerie = await _crudService.GetObjectById<Serie>(id);

            if (dbSerie != default(Serie))
            {
                await _crudService.Update(serie);

                return Ok(serie);
            }
            else
            {
                return UnprocessableEntity();
            }
        }

        /// <summary>
        /// Delete a serie by id
        /// </summary>
        [HttpDelete]
        [Route("{id:int}")]
        [SwaggerResponse((int)HttpStatusCode.OK, ResponseDescriptions.DeleteOk, typeof(Serie))]
        [SwaggerResponse((int)HttpStatusCode.UnprocessableEntity, ResponseDescriptions.DeleteUnprocessableEntity, typeof(Serie))]
        public async Task<IActionResult> Delete(int id)
        {
            var serie = await _crudService.GetObjectById<Serie>(id);

            if (serie != default(Serie))
            {
                await _crudService.Delete(serie);

                return Ok(serie);
            }
            else
            {
                return UnprocessableEntity();
            }
        }
    }
}