using Entities.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Contrat
{
    public interface IDistrictRepository : IRepositoryBase<District>
    {
        Task<IEnumerable<District>> GetAllDistrictsAsync(bool trackChanges);
        Task<District> GetDistrictByIdAsync(int id, bool trackChanges);
        Task CreateDistrictAsync(District district);
        void UpdateDistrict(District district);
        void DeleteDistrict(District district);
    }
}
