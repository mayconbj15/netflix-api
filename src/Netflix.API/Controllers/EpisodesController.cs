using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Netflix.API.Models.Database;
using Netflix.API.Repositories;
using Netflix.API.Services;

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

        [HttpGet]
        public async Task<IActionResult> All()
        {
            return Ok(await _crudService.GetAll<Episode>());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Episode episode)
        {
            if (ModelState.IsValid)
            {
                await _crudService.Create(episode);

                return Created($"api/seasons/{episode.Id}", episode);
            }

            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Read(int id)
        {
            var episode = await _crudService.GetObjectById<Episode>(id);

            return Ok(episode);
        }

        [HttpPatch]
        [Route("{id:int}")]
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

        [HttpDelete]
        [Route("{id:int}")]
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