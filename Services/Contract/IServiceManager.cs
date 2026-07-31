using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Contract
{
    public interface IServiceManager
    {
        ICityService CityService { get; }
        IDistrictService DistrictService { get; }
        IDutyService DutyService { get; }
        IPharmacyService PharmacyService { get; }
    }
}
