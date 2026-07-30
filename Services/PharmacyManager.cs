using Entities.Model;
using Microsoft.EntityFrameworkCore;
using Repository.Contrat;
using Services.Contract;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services
{
    public class PharmacyManager : IPharmacyService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly HttpClient _httpClient;

        public PharmacyManager(IRepositoryManager repositoryManager, HttpClient httpClient)
        {
            _repositoryManager = repositoryManager;
            _httpClient = httpClient;
        }

        
        public async Task SyncDutyPharmaciesAsync(string city, string district)
        {
            // 1. COLLECT API'DEN VERİ ÇEKME
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.collectapi.com/health/dutyPharmacy?ilce={district}&il={city}");

            request.Headers.Add("authorization", "apikey 4joAhGdnxlWmA0vOrtJnJR:3xk6YYwXNLdD6aV8hvRf3N");
            request.Headers.Add("content-type", "application/json");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();

            // 2. JSON VERİSİNİ DTO'YA ÇEVİRME (DESERIALIZATION)
            var apiResult = JsonSerializer.Deserialize<CollectApiResultDto>(jsonString);

            if (apiResult == null || !apiResult.Success || apiResult.Result == null)
            {
                throw new Exception("API'den veri çekilemedi veya sonuç başarısız.");
            }

            // 3. UPSERT ALGORİTMASI (İl ve İlçe Kontrolü)
            var cityEntity = await _repositoryManager.City.FindByCondition(c => c.Name == city, trackChanges: true).FirstOrDefaultAsync();
            if (cityEntity == null)
            {
                cityEntity = new City { Name = city };
                await _repositoryManager.City.CreateCityAsync(cityEntity);
                await _repositoryManager.SaveAsync();
            }

            var districtEntity = await _repositoryManager.District.FindByCondition(d => d.Name == district && d.CityId == cityEntity.Id, trackChanges: true).FirstOrDefaultAsync();
            if (districtEntity == null)
            {
                districtEntity = new District { Name = district, CityId = cityEntity.Id };
                await _repositoryManager.District.CreateDistrictAsync(districtEntity);
                await _repositoryManager.SaveAsync();
            }

            // 4. ECZANE VE NÖBET KAYITLARININ İŞLENMESİ
            DateTime startTime = DateTime.Today.AddHours(18);
            DateTime endTime = DateTime.Today.AddDays(1).AddHours(8).AddMinutes(30);

            foreach (var apiPharmacy in apiResult.Result)
            {
                var pharmacyEntity = await _repositoryManager.Pharmacy
                    .FindByCondition(p => p.Name == apiPharmacy.Name && p.DistrictId == districtEntity.Id, trackChanges: true)
                    .FirstOrDefaultAsync();

                if (pharmacyEntity == null)
                {
                    pharmacyEntity = new Pharmacy
                    {
                        Name = apiPharmacy.Name,
                        Address = apiPharmacy.Address,
                        Phone = apiPharmacy.Phone,
                        Location = apiPharmacy.Loc,
                        DistrictId = districtEntity.Id
                    };
                    await _repositoryManager.Pharmacy.CreatePharmacyAsync(pharmacyEntity);
                    await _repositoryManager.SaveAsync();
                }

                // 5. NÖBET KAYDINI EKLE
                var dutyExists = await _repositoryManager.Duty
                    .FindByCondition(d => d.PharmacyId == pharmacyEntity.Id && d.StartTime == startTime, trackChanges: false)
                    .AnyAsync();

                if (!dutyExists)
                {
                    var newDuty = new Duty
                    {
                        PharmacyId = pharmacyEntity.Id,
                        StartTime = startTime,
                        EndTime = endTime
                    };
                    await _repositoryManager.Duty.CreateDutyAsync(newDuty);
                }
            }

            await _repositoryManager.SaveAsync();
        }

        

        public async Task<Pharmacy> CreatePharmacyAsync(Pharmacy pharmacy)
        {
            await _repositoryManager.Pharmacy.CreatePharmacyAsync(pharmacy);
            await _repositoryManager.SaveAsync();
            return pharmacy;
        }

        public async Task DeletePharmacyAsync(int id)
        {
            var entity = await _repositoryManager.Pharmacy.GetPharmacyByIdAsync(id, trackChanges: false);

            if (entity == null)
                throw new Exception($"Id'si {id} olan eczane bulunamadı. Silme işlemi iptal edildi.");

            _repositoryManager.Pharmacy.DeletePharmacy(entity);
            await _repositoryManager.SaveAsync();
        }

        public async Task<IEnumerable<Pharmacy>> GetAllPharmaciesAsync(bool trackChanges)
        {
            return await _repositoryManager.Pharmacy.GetAllPharmaciesAsync(trackChanges);
        }

        public async Task<Pharmacy> GetPharmaciesAsync(int id, bool trackChanges)
        {
            var pharmacy = await _repositoryManager.Pharmacy.GetPharmacyByIdAsync(id, trackChanges);

            if (pharmacy == null)
                throw new Exception($"Id'si {id} olan eczane bulunamadı.");

            return pharmacy;
        }

        public async Task UpdatePharmacyAsync(int id, Pharmacy pharmacy)
        {
            var entity = await _repositoryManager.Pharmacy.GetPharmacyByIdAsync(id, trackChanges: true);

            if (entity == null)
                throw new Exception($"Id'si {id} olan eczane bulunamadı. Güncelleme yapılamaz.");

            entity.Name = pharmacy.Name;
            entity.Address = pharmacy.Address;
            entity.Phone = pharmacy.Phone;
            entity.Location = pharmacy.Location;
            entity.DistrictId = pharmacy.DistrictId;

            _repositoryManager.Pharmacy.UpdatePharmacy(entity);
            await _repositoryManager.SaveAsync();
        }
    }
}