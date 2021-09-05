using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Netflix.API.Models.Database;
using Netflix.API.Repositories;
using Netflix.API.Services;

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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Season season)
        {
            if (ModelState.IsValid)
            {
                await _crudService.Create(season);

                return Created($"api/season/{season.Id}", season);
            }

            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Read(int id)
        {
            var season = await _crudService.GetObjectById<Season>(id);

            return Ok(season);
        }

        [HttpPatch]
        [Route("{id:int}")]
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

        [HttpDelete]
        [Route("{id:int}")]
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