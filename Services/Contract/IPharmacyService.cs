using Entities.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Contract
{
    public interface IPharmacyService
    {
        //Dış API den veri senkronizasyonu
        Task SyncDutyPharmaciesAsync(string city, string district);

        //Temel CRUD işlemleri ilerde lazım olma ihtialine karşı
        Task<IEnumerable<Pharmacy>> GetAllPharmaciesAsync(bool trackChanges);
        Task<Pharmacy> GetPharmaciesAsync(int id, bool trackChanges);
        Task<Pharmacy> CreatePharmacyAsync(Pharmacy pharmacy);
        Task UpdatePharmacyAsync(int id, Pharmacy pharmacy);
        Task DeletePharmacyAsync(int id);
    }
}
