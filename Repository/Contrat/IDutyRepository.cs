using Entities.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Contrat
{
    public interface IDutyRepository : IRepositoryBase<Duty>
    {
        Task<IEnumerable<Duty>> GetAllDutiesAsync(bool trackChanges);
        Task<Duty> GetDutyByIdAsync(int id, bool trackChanges);
        Task CreateDutyAsync(Duty duty);
        void UpdateDuty(Duty duty);
        void DeleteDuty(Duty duty);
    }
}
