using Entities.Model;
using Microsoft.EntityFrameworkCore;
using Repository.Contrat;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.EF_Core
{
    public class PharmacyRepository : RepositoryBase<Pharmacy>, IPharmacyRepository
    {
        public PharmacyRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Pharmacy>> GetAllPharmaciesAsync(bool trackChanges) =>
            await FindAll(trackChanges).ToListAsync();

        public async Task<Pharmacy> GetPharmacyByIdAsync(int id, bool trackChanges) =>
            await FindByCondition(p => p.Id == id, trackChanges).FirstOrDefaultAsync();

        public async Task CreatePharmacyAsync(Pharmacy pharmacy) => await CreateAsync(pharmacy);

        public void UpdatePharmacy(Pharmacy pharmacy) => Update(pharmacy);

        public void DeletePharmacy(Pharmacy pharmacy) => Delete(pharmacy);
    }
}
