using Entities.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Contract
{
    public interface IDutyService
    {
        Task<IEnumerable<Duty>> GetAllDutiesAsync(bool trackChanges);
        Task<Duty> GetOneDutyAsync(int id, bool trackChanges);
        Task<Duty> CreateOneDutyAsync(Duty duty);
        Task DeleteOneDutyAsync(int id);
        Task UpdateOneDutyAsync(int id, Duty duty);
    }
}
