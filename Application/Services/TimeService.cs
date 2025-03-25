using APIAondeAssistir.Application.Interfaces;
using APIAondeAssistir.Domain.Entities;
using APIAondeAssistir.Domain.Interfaces;

namespace APIAondeAssistir.Application.Services
{
    public class TimeService : ITimeService
    {
        private readonly ITimeRepository _timeRepository;
        public TimeService(ITimeRepository timeRepository)
        {
            _timeRepository = timeRepository;
        }

        public Task<List<Time>> GetAllAsync()
        {
            return _timeRepository.GetAllAsync();
        }
        public Task<Time> GetById(int id)
        {
            return _timeRepository.GetById(id);
        }
        public async Task<bool> CreateAsync(Time time)
        {
            //TO-DO): checar se o id do time já existe
            if (time.Codigo < 1)
            {
                time.Codigo = await _timeRepository.GetNewId();
            }
            var timeCreated = _timeRepository.CreateAsync(time);
            if (timeCreated != null)
            {
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false); 
        }
        public async Task<bool> UpdateAsync(Time time)
        {
            var timeUpdated = _timeRepository.UpdateAsync(time);
            if (timeUpdated != null)
            {
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false); 
        }
        public Task<bool> DeleteAsync(int id)
        {
            return _timeRepository.DeleteAsync(id);
        }
    }
}
