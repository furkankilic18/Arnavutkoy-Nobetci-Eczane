using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Contrat
{
    public interface IRepositoryManager
    {
        ICityRepository City { get; }
        IDistrictRepository District { get; }
        IPharmacyRepository Pharmacy { get; }
        IDutyRepository Duty { get; }

        Task SaveAsync();
    }
}
