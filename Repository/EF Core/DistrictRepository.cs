using Entities.Model;
using Repository.Contrat;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Repository.EF_Core
{
    public class DistrictRepository : RepositoryBase<District>, IDistrictRepository
    {
        public DistrictRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task<IEnumerable<District>> GetAllDistrictsAsync(bool trackChanges) =>
            await FindAll(trackChanges).ToListAsync();

        public async Task<District> GetDistrictByIdAsync(int id, bool trackChanges) =>
            await FindByCondition(d => d.Id == id, trackChanges).FirstOrDefaultAsync();

        public async Task CreateDistrictAsync(District district) => await CreateAsync(district);

        public void UpdateDistrict(District district) => Update(district);

        public void DeleteDistrict(District district) => Delete(district);
    }
}
