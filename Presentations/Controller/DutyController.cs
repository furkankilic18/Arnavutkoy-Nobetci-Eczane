using Entities.Model;
using Microsoft.AspNetCore.Mvc;
using Services.Contract;
using System;
using System.Threading.Tasks;

namespace Presentations.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class DutyController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public DutyController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDutys()
        {
            try
            {
                var entities = await _serviceManager.DutyService.GetAllDutiesAsync(trackChanges: false);
                return Ok(entities);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sunucu Hatası: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOneDutyById([FromRoute] int id)
        {
            try
            {
                var entity = await _serviceManager.DutyService.GetOneDutyAsync(id, trackChanges: false);
                if (entity is null)
                    return NotFound($"Verilen {id} numaralı idye ait bir mesai bulunamadı");
                
                return Ok(entity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sunucu Hatası: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateOneDuty([FromBody] Duty duty)
        {
            try
            {
                if (duty == null)
                    return BadRequest("Nöbet verisi boş olamaz.");

                await _serviceManager.DutyService.CreateOneDutyAsync(duty);
                return NoContent(); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sunucu Hatası: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOneDuty([FromRoute] int id, [FromBody] Duty duty)
        {
            try
            {
                if (duty == null)
                    return BadRequest("Nöbet (Duty) verisi boş olamaz.");

                await _serviceManager.DutyService.UpdateOneDutyAsync(id, duty);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sunucu Hatası: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOneDuty([FromRoute] int id)
        {
            try
            {
                await _serviceManager.DutyService.DeleteOneDutyAsync(id);
                return Ok(); // Silme başarılı
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sunucu Hatası: {ex.Message}");
            }
        }
    }
}