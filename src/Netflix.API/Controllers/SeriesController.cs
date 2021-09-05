using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Netflix.API.Models.Database;
using Netflix.API.Repositories;
using Netflix.API.Services;

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

        [HttpGet]
        public async Task<IActionResult> All()
        {
            return Ok(await _crudService.GetAll<Serie>());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Serie serie)
        {
            if (ModelState.IsValid)
            {
                await _crudService.Create(serie);

                return Created($"api/series/{serie.Id}", serie);
            }

            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Read(int id)
        {
            var serie = await _crudService.GetObjectById<Serie>(id);

            return Ok(serie);
        }

        [HttpPatch]
        [Route("{id:int}")]
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

        [HttpDelete]
        [Route("{id:int}")]
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