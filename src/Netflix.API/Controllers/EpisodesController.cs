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
    public class EpisodesController : ControllerBase
    {
        private readonly ICRUDService<SerieContext> _crudService;

        public EpisodesController(ICRUDService<SerieContext> crudService)
        {
            _crudService = crudService;
        }

        /// <summary>
        /// List all episodies
        /// </summary>
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, ResponseDescriptions.EpisodieAllOk, typeof(IEnumerable<Episode>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, ResponseDescriptions.InternalServerError, typeof(ExceptionResponse))]
        public async Task<IActionResult> All()
        {
            return Ok(await _crudService.GetAll<Episode>());
        }

        /// <summary>
        /// Create a new episode
        /// </summary>
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, ResponseDescriptions.CreateCreated, typeof(Episode))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, ResponseDescriptions.CreateBadRequest)]
        public async Task<IActionResult> Create([FromBody] Episode episode)
        {
            if (ModelState.IsValid)
            {
                await _crudService.Create(episode);

                return Created($"api/seasons/{episode.Id}", episode);
            }

            return BadRequest();
        }

        /// <summary>
        /// Get a episode by id
        /// </summary>
        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, ResponseDescriptions.ReadOk, typeof(Episode))]
        [SwaggerResponse((int)HttpStatusCode.NoContent, ResponseDescriptions.ReadNoContent)]
        public async Task<IActionResult> Read(int id)
        {
            var episode = await _crudService.GetObjectById<Episode>(id);

            return Ok(episode);
        }

        /// <summary>
        /// Update a episode by id
        /// </summary>
        [HttpPatch]
        [Route("{id:int}")]
        [SwaggerResponse((int)HttpStatusCode.OK, ResponseDescriptions.UpdateOk, typeof(Episode))]
        [SwaggerResponse((int)HttpStatusCode.UnprocessableEntity, ResponseDescriptions.UpdateUnprocessableEntity, typeof(Episode))]
        public async Task<IActionResult> Update(int id, [FromBody] Episode episode)
        {
            var dbEpisode = await _crudService.GetObjectById<Episode>(id);

            if (dbEpisode != default(Episode))
            {
                await _crudService.Update(episode);

                return Ok(episode);
            }
            else
            {
                return UnprocessableEntity();
            }
        }

        /// <summary>
        /// Delete a episode by id
        /// </summary>
        [HttpDelete]
        [Route("{id:int}")]
        [SwaggerResponse((int)HttpStatusCode.OK, ResponseDescriptions.DeleteOk, typeof(Episode))]
        [SwaggerResponse((int)HttpStatusCode.UnprocessableEntity, ResponseDescriptions.DeleteUnprocessableEntity, typeof(Episode))]
        public async Task<IActionResult> Delete(int id)
        {
            var episode = await _crudService.GetObjectById<Episode>(id);

            if (episode != default(Episode))
            {
                await _crudService.Delete(episode);

                return Ok(episode);
            }
            else
            {
                return UnprocessableEntity();
            }
        }
    }
}