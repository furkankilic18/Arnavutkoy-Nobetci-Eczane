using Repository.Contrat;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.EF_Core
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _context;

        // Lazy loading nesneleri: Sadece çağrıldıklarında hafızada (RAM) yer kaplarlar.
        private readonly Lazy<ICityRepository> _cityRepository;
        private readonly Lazy<IDistrictRepository> _districtRepository;
        private readonly Lazy<IPharmacyRepository> _pharmacyRepository;
        private readonly Lazy<IDutyRepository> _dutyRepository;

        public RepositoryManager(RepositoryContext context)
        {
            _context = context;

            _cityRepository = new Lazy<ICityRepository>(() => new CityRepository(_context));
            _districtRepository = new Lazy<IDistrictRepository>(() => new DistrictRepository(_context));
            _pharmacyRepository = new Lazy<IPharmacyRepository>(() => new PharmacyRepository(_context));
            _dutyRepository = new Lazy<IDutyRepository>(() => new DutyRepository(_context));
        }

        public ICityRepository City => _cityRepository.Value;
        public IDistrictRepository District => _districtRepository.Value;
        public IPharmacyRepository Pharmacy => _pharmacyRepository.Value;
        public IDutyRepository Duty => _dutyRepository.Value;

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
