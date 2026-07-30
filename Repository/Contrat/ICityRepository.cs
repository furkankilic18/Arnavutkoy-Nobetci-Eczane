using Entities.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Contrat
{
    public interface ICityRepository : IRepositoryBase<City>
    {
        Task<IEnumerable<City>> GetAllCitiesAsync(bool trackChanges);
        Task<City> GetCityByIdAsync(int id, bool trackChanges);
        Task CreateCityAsync(City city);
        void UpdateCity(City city);
        void DeleteCity(City city);
    }
}
