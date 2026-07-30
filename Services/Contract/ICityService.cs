using Entities.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Contract
{
     public interface ICityService
    {
        Task<IEnumerable<City>> GetAllCıtyAsync(bool trackChanges);
        Task<City> GetOneCityAsycn(int id, bool trakChanges);
        Task<City> CreateOneCityAsync(City city);
        Task DeleteOneCityAsync(int id);
        Task UpdateOneCityAsync(int id, City city);

    }
}
