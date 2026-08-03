using Entities.Model;
using Microsoft.AspNetCore.Mvc;
using Services.Contract;
using System;
using System.Threading.Tasks;

namespace Presentations.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class DistrictController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public DistrictController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOneDistrictById([FromRoute] int id)
        {
            try
            {
                var entity = await _serviceManager.DistrictService.GetOneDistrictAsync(id, trackChanges: false);

                if (entity is null)
                    return NotFound($"Verdiğiniz {id} değerine sahip bir ilçe bulunamadı.");

                return Ok(entity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sunucu Hatası: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDistricts()
        {
            try
            {
                var entities = await _serviceManager.DistrictService.GetAllDistrictsAsync(trackChanges: false);
                return Ok(entities);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sunucu Hatası: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOneDistrict([FromRoute] int id, [FromBody] District district)
        {
            try
            {
                if (district is null)
                    return BadRequest("Güncellenmek istenen ilçe bilgisi boş olamaz.");

                var entity = await _serviceManager.DistrictService.GetOneDistrictAsync(id, trackChanges: true);

                if (entity is null)
                    return NotFound($"Verdiğiniz {id} değerine sahip bir ilçe bulunamadı.");

                await _serviceManager.DistrictService.UpdateOneDistrictAsync(id, district);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sunucu Hatası: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateOneDistrict([FromBody] District district)
        {
            try
            {
                if (district is null)
                    return BadRequest("Eklenecek ilçe bilgisi boş olamaz.");

                await _serviceManager.DistrictService.CreateOneDistrictAsync(district);

                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sunucu Hatası: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOneDistrict([FromRoute] int id)
        {
            try
            {
                var entity = await _serviceManager.DistrictService.GetOneDistrictAsync(id, trackChanges: false);

                if (entity is null)
                    return NotFound($"Verdiğiniz {id} değerine sahip bir ilçe bulunamadı.");

                await _serviceManager.DistrictService.DeleteOneDistrictAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sunucu Hatası: {ex.Message}");
            }
        }
    }
}