using Entities.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Contract
{
    public interface IDistrictService
    {
        Task<IEnumerable<District>> GetAllDistrictsAsync(bool trackChanges);
        Task<District> GetOneDistrictAsync(int id, bool trackChanges);
        Task<District> CreateOneDistrictAsync(District district);
        Task DeleteOneDistrictAsync(int id);
        Task UpdateOneDistrictAsync(int id, District district);
    }
}
