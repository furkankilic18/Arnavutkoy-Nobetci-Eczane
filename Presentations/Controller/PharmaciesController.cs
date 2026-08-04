using Entities.Model;
using Microsoft.AspNetCore.Mvc;
using Services.Contract;
using System;
using System.Threading.Tasks;

namespace Presentations.Controller
{
    [ApiController]
    [Route("api/pharmacies")]
    public class PharmaciesController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public PharmaciesController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost("async")]
        public async Task<IActionResult> SyncPharmacy([FromQuery] string city, [FromQuery] string district)
        {
            try
            {
                await _serviceManager.PharmacyService.SyncDutyPharmaciesAsync(city, district);
                return Ok(new { message = $"{city} ili {district} ilçesi için eczane verileri başarıyla senkronize edildi." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sunucu hatası: {ex.Message}");
            }
        }


        [HttpGet("todays-duties")]
        public async Task<IActionResult> GetTodaysDuties([FromQuery] string city, [FromQuery] string district)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(city) || string.IsNullOrWhiteSpace(district))
                    return BadRequest("Şehir (city) ve İlçe (district) parametreleri zorunludur.");

                
                var pharmacies = await _serviceManager.PharmacyService.GetTodaysDutyPharmaciesAsync(city, district, trackChanges: false);

                return Ok(pharmacies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sunucu Hatası: {ex.Message}");
            }
        }




        [HttpGet]
        public async Task<IActionResult> GetAllPharmacies()
        {
            try
            {
                var entities = await _serviceManager.PharmacyService.GetAllPharmaciesAsync(trackChanges: false);
                return Ok(entities);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sunucu Hatası: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOnePharmacyById([FromRoute] int id)
        {
            try
            {
                var entity = await _serviceManager.PharmacyService.GetPharmaciesAsync(id, trackChanges: false);

                if (entity is null)
                    return NotFound($"Verilen {id} numaralı idye sahip bir eczane bulunamadı.");
                return Ok(entity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sunucu hatası: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateOnePharmacy([FromBody] Pharmacy pharmacy)
        {
            try
            {
                if (pharmacy == null)
                    return BadRequest("Eczane verisi boş olamaz.");

                await _serviceManager.PharmacyService.CreatePharmacyAsync(pharmacy);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sunucu Hatası: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOnePharmacy([FromRoute] int id, [FromBody] Pharmacy pharmacy)
        {
            try
            {
                if (pharmacy == null)
                    return BadRequest("Eczane verisi boş olamaz.");

                await _serviceManager.PharmacyService.UpdatePharmacyAsync(id, pharmacy);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sunucu Hatası: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOnePharmacy([FromRoute] int id)
        {
            try
            {
                await _serviceManager.PharmacyService.DeletePharmacyAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sunucu Hatası: {ex.Message}");
            }
        }
    }
}