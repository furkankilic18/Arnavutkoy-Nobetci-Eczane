using Repository.Contrat;
using Services.Contract;
using System.Net.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<ICityService> _cityService;
        private readonly Lazy<IDistrictService> _districtService;
        private readonly Lazy<IDutyService> _dutyService;
        private readonly Lazy<IPharmacyService> _pharmacyService;

        public ServiceManager(IRepositoryManager repositoryManager , HttpClient httpClient)
        {
            _cityService = new Lazy<ICityService>(() => new CityManager(repositoryManager));
            _districtService = new Lazy<IDistrictService>(() =>new DistrictManager(repositoryManager));
            _dutyService = new Lazy<IDutyService>(() => new DutyManager(repositoryManager));
            _pharmacyService = new Lazy<IPharmacyService>(() => new PharmacyManager(repositoryManager,httpClient));
            
        }
        public ICityService CityService => _cityService.Value;


        public IDistrictService DistrictService => _districtService.Value;

        public IDutyService DutyService => _dutyService.Value;

        public IPharmacyService PharmacyService => _pharmacyService.Value;
    }
}
