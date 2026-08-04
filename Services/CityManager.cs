using Entities.Model;
using Repository.Contrat;
using Services.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class CityManager : ICityService
    {
        private readonly IRepositoryManager _repositoryManager;

        public CityManager(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<City> CreateOneCityAsync(City city)
        {
          await _repositoryManager.City.CreateCityAsync(city);
            await _repositoryManager.SaveAsync();

            return city;
        }

        public async Task DeleteOneCityAsync(int id)
        {
            var entitis = await _repositoryManager.City.GetCityByIdAsync(id, trackChanges: false);
            if (entitis is null)
                throw new Exception($"Verilen {id} numaralı Id'ye sahip şehir bulunamadı.Silme işlemi iptal edildi");

            _repositoryManager.City.DeleteCity(entitis);
            await _repositoryManager.SaveAsync();
                    
        }

        public async Task<IEnumerable<City>> GetAllCıtyAsync(bool trackChanges)
        {
           var entities =  await _repositoryManager.City.GetAllCitiesAsync(trackChanges);
            return entities;
        }

        public async Task<City> GetOneCityAsycn(int id, bool trakChanges)
        {
            var entity = await _repositoryManager.City.GetCityByIdAsync(id,trakChanges);
            if (entity is null)
                throw new Exception($"Verilen {id} numaralı Id'ye sahip şehir bulunamadı.");
            return entity;
        }

        public async Task UpdateOneCityAsync(int id, City city)
        {
            if (city is null) throw new ArgumentNullException(nameof(city), "Güncellemek istenen şehir boş olamaz.");

            var entity = await _repositoryManager.City.GetCityByIdAsync(id, trackChanges: true);
            if (entity is null) throw new Exception($"Verilen {id} numaralı Id'ye sahip şehir bulunamadı.");

            entity.Name = city.Name;

            _repositoryManager.City.UpdateCity(entity);
            await _repositoryManager.SaveAsync();
        }
    }
}
