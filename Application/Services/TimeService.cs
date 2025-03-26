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
            // Se o código do time informado é valido
            if (time.Codigo < 1)
            {
                time.Codigo = await _timeRepository.GetNewId();
            }

            //verifica se o time já existe
            if (await _timeRepository.GetById(time.Codigo) != null)
            {
                return false;
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
