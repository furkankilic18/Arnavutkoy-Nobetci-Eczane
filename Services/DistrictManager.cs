using Entities.Model;
using Repository.Contrat;
using Services.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class DistrictManager : IDistrictService
    {
        private readonly IRepositoryManager _repositoryManager;

        public DistrictManager(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<District> CreateOneDistrictAsync(District district)
        {
            await _repositoryManager.District.CreateDistrictAsync(district);
            await _repositoryManager.SaveAsync();
            return district;
        }

        public async Task DeleteOneDistrictAsync(int id)
        {
            var entity = await _repositoryManager.District.GetDistrictByIdAsync(id, trackChanges: false);
            if (entity is null)
                throw new Exception($"Verilen {id} numaralı Id'ye sahip ilçe bulunamadı. Silme işlemi iptal edildi.");

            _repositoryManager.District.DeleteDistrict(entity);
            await _repositoryManager.SaveAsync();
        }

        public async Task<IEnumerable<District>> GetAllDistrictsAsync(bool trackChanges)
        {
            var entities = await _repositoryManager.District.GetAllDistrictsAsync(trackChanges);
            return entities;
        }

        public async Task<District> GetOneDistrictAsync(int id, bool trackChanges)
        {
            var entity = await _repositoryManager.District.GetDistrictByIdAsync(id, trackChanges);
            if (entity is null)
                throw new Exception($"Verilen {id} numaralı Id'ye sahip ilçe bulunamadı.");
            return entity;
        }

        public async Task UpdateOneDistrictAsync(int id, District district)
        {
            if (district is null) throw new ArgumentNullException(nameof(district), "Güncellemek istenen ilçe boş olamaz.");

            var entity = await _repositoryManager.District.GetDistrictByIdAsync(id, trackChanges: true);
            if (entity is null) throw new Exception($"Verilen {id} numaralı Id'ye sahip ilçe bulunamadı.");

            entity.Name = district.Name;
            entity.CityId = district.CityId;

            _repositoryManager.District.UpdateDistrict(entity);
            await _repositoryManager.SaveAsync();
        }
    }
}
