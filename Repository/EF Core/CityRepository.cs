using Entities.Model;
using Repository.Contrat;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.EF_Core
{
    public class CityRepository : RepositoryBase<City>, ICityRepository
    {
        public CityRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task<IEnumerable<City>> GetAllCitiesAsync(bool trackChanges) =>
            await FindAll(trackChanges).ToListAsync();

        public async Task<City> GetCityByIdAsync(int id, bool trackChanges) =>
            await FindByCondition(c => c.Id == id, trackChanges).FirstOrDefaultAsync();

        public async Task CreateCityAsync(City city) => await CreateAsync(city);

        public void UpdateCity(City city) => Update(city);

        public void DeleteCity(City city) => Delete(city);
    }
}
