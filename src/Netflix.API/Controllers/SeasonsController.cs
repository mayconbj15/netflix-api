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
    public class SeasonsController : ControllerBase
    {
        private readonly ICRUDService<SerieContext> _crudService;

        public SeasonsController(ICRUDService<SerieContext> crudService)
        {
            _crudService = crudService;
        }

        /// <summary>
        /// List all seasons
        /// </summary>
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, ResponseDescriptions.SeasonAllOk, typeof(IEnumerable<Season>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, ResponseDescriptions.InternalServerError, typeof(ExceptionResponse))]
        public async Task<IActionResult> All()
        {
            return Ok(await _crudService.GetAll<Season>());
        }

        /// <summary>
        /// Create a new episode
        /// </summary>
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, ResponseDescriptions.CreateCreated, typeof(Season))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, ResponseDescriptions.CreateBadRequest)]
        public async Task<IActionResult> Create([FromBody] Season season)
        {
            if (ModelState.IsValid)
            {
                await _crudService.Create(season);

                return Created($"api/seasons/{season.Id}", season);
            }

            return BadRequest();
        }

        /// <summary>
        /// Get a episode by id
        /// </summary>
        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, ResponseDescriptions.ReadOk, typeof(Season))]
        [SwaggerResponse((int)HttpStatusCode.NoContent, ResponseDescriptions.ReadNoContent)]
        public async Task<IActionResult> Read(int id)
        {
            var season = await _crudService.GetObjectById<Season>(id);

            return Ok(season);
        }

        /// <summary>
        /// Update a episode by id
        /// </summary>
        [HttpPatch]
        [Route("{id:int}")]
        [SwaggerResponse((int)HttpStatusCode.OK, ResponseDescriptions.UpdateOk, typeof(Season))]
        [SwaggerResponse((int)HttpStatusCode.UnprocessableEntity, ResponseDescriptions.UpdateUnprocessableEntity, typeof(Season))]
        public async Task<IActionResult> Update(int id, [FromBody] Season season)
        {
            var dbSeason = await _crudService.GetObjectById<Season>(id);

            if (dbSeason != default(Season))
            {
                await _crudService.Update(season);

                return Ok(season);
            }
            else
            {
                return UnprocessableEntity();
            }
        }

        /// <summary>
        /// Delete a season by id
        /// </summary>
        [HttpDelete]
        [Route("{id:int}")]
        [SwaggerResponse((int)HttpStatusCode.OK, ResponseDescriptions.DeleteOk, typeof(Season))]
        [SwaggerResponse((int)HttpStatusCode.UnprocessableEntity, ResponseDescriptions.DeleteUnprocessableEntity, typeof(Season))]
        public async Task<IActionResult> Delete(int id)
        {
            var season = await _crudService.GetObjectById<Season>(id);

            if (season != default(Season))
            {
                await _crudService.Delete(season);

                return Ok(season);
            }
            else
            {
                return UnprocessableEntity();
            }
        }
    }
}