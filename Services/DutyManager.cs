using Entities.Model;
using Repository.Contrat;
using Services.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class DutyManager : IDutyService
    {
        private readonly IRepositoryManager _repositoryManager;

        public DutyManager(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<Duty> CreateOneDutyAsync(Duty duty)
        {
            await _repositoryManager.Duty.CreateDutyAsync(duty);
            await _repositoryManager.SaveAsync();
            return duty;
        }

        public async Task DeleteOneDutyAsync(int id)
        {
            var entity = await _repositoryManager.Duty.GetDutyByIdAsync(id, trackChanges: false);
            if (entity is null)
                throw new Exception($"Verilen {id} numaralı Id'ye sahip nöbet bulunamadı. Silme işlemi iptal edildi.");

            _repositoryManager.Duty.DeleteDuty(entity);
            await _repositoryManager.SaveAsync();
        }

        public async Task<IEnumerable<Duty>> GetAllDutiesAsync(bool trackChanges)
        {
            var entities = await _repositoryManager.Duty.GetAllDutiesAsync(trackChanges);
            return entities;
        }

        public async Task<Duty> GetOneDutyAsync(int id, bool trackChanges)
        {
            var entity = await _repositoryManager.Duty.GetDutyByIdAsync(id, trackChanges);
            if (entity is null)
                throw new Exception($"Verilen {id} numaralı Id'ye sahip nöbet bulunamadı.");
            return entity;
        }

        public async Task UpdateOneDutyAsync(int id, Duty duty)
        {
            if (duty is null)
                throw new ArgumentNullException(nameof(duty), "Güncellemek istenen nöbet boş olamaz. İşlem iptal edildi.");

            var entity = await _repositoryManager.Duty.GetDutyByIdAsync(id, trackChanges: true);
            if (entity is null)
                throw new Exception($"Verilen {id} numaralı Id'ye sahip nöbet bulunamadı.");

            _repositoryManager.Duty.UpdateDuty(entity);
            await _repositoryManager.SaveAsync();
        }
    }
}
