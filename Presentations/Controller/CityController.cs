using Entities.Model;
using Microsoft.AspNetCore.Mvc;
using Services.Contract;
using System;
using System.Threading.Tasks;

namespace Presentations.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public CityController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet("{id:int}")] 
        public async Task<IActionResult> GetOneCityById([FromRoute] int id)
        {
            try
            {
                var entity = await _serviceManager.CityService.GetOneCityAsycn(id, trakChanges: false);

                if (entity is null)
                    return NotFound($"Verdiğiniz {id} değerine sahip bir şehir bulunamadı.");

                return Ok(entity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sunucu Hatası: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCities()
        {
            try
            {
                var entities = await _serviceManager.CityService.GetAllCıtyAsync(trackChanges: false); 
                return Ok(entities);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sunucu Hatası: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOneCity([FromRoute] int id, [FromBody] City city)
        {
            try
            {
                if (city is null)
                    return BadRequest("Güncellenmek istenen şehir bilgisi boş olamaz.");

                var entity = await _serviceManager.CityService.GetOneCityAsycn(id, trakChanges: true);

                if (entity is null)
                    return NotFound($"Verdiğiniz {id} değerine sahip bir şehir bulunamadı."); 

                await _serviceManager.CityService.UpdateOneCityAsync(id, city);
                return NoContent(); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sunucu Hatası: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateOneCity([FromBody] City city)
        {
            try
            {
                if (city is null)
                    return BadRequest("Eklenecek şehir bilgisi boş olamaz."); 

                await _serviceManager.CityService.CreateOneCityAsync(city);

                
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sunucu Hatası: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOneCity([FromRoute] int id)
        {
            try
            {
                var entity = await _serviceManager.CityService.GetOneCityAsycn(id, trakChanges: false);

                if (entity is null)
                    return NotFound($"Verdiğiniz {id} değerine sahip bir şehir bulunamadı."); 

                await _serviceManager.CityService.DeleteOneCityAsync(id);
                return NoContent(); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sunucu Hatası: {ex.Message}");
            }
        }
    }
}