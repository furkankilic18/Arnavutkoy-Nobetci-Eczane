using Entities.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Contrat
{
    public interface IPharmacyRepository : IRepositoryBase<Pharmacy>
    {
        Task<IEnumerable<Pharmacy>> GetAllPharmaciesAsync(bool trackChanges);
        Task<Pharmacy> GetPharmacyByIdAsync(int id, bool trackChanges);
        Task CreatePharmacyAsync(Pharmacy pharmacy);
        void UpdatePharmacy(Pharmacy pharmacy);
        void DeletePharmacy(Pharmacy pharmacy);
    }
}
