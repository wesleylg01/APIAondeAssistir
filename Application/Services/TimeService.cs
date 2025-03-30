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
        public async Task<Time> GetById(int id)
        {
            return await _timeRepository.GetById(id);
        }
        public async Task<bool> CreateAsync(Time time)
        {
            /* verifica se o código do time informado é valido
             * se não for gera, um válido */
            if (time.Codigo < 1)
            {
                time.Codigo = await _timeRepository.GetNewId();
            }

            return await _timeRepository.CreateAsync(time);  
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
