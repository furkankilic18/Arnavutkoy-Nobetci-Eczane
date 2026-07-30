using Entities.Model;
using Microsoft.EntityFrameworkCore;
using Repository.Contrat;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.EF_Core
{
    public class DutyRepository : RepositoryBase<Duty>, IDutyRepository
    {
        public DutyRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Duty>> GetAllDutiesAsync(bool trackChanges) =>
            await FindAll(trackChanges).ToListAsync();

        public async Task<Duty> GetDutyByIdAsync(int id, bool trackChanges) =>
            await FindByCondition(d => d.Id == id, trackChanges).FirstOrDefaultAsync();

        public async Task CreateDutyAsync(Duty duty) => await CreateAsync(duty);

        public void UpdateDuty(Duty duty) => Update(duty);

        public void DeleteDuty(Duty duty) => Delete(duty);
    }
}
