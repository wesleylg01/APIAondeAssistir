using APIAondeAssistir.Application.Interfaces;
using APIAondeAssistir.Domain.Entities;
using APIAondeAssistir.Domain.Enums;
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
            var time = await _timeRepository.GetById(id);
            if ((time == null) || (time.Codigo == 0) || (time.Nome == null))
            {
                throw new KeyNotFoundException(TimeErros.TimeNaoEncontrado.GetMessage());
            }
            return time;
        }
        public async Task<bool> CreateAsync(Time time)
        {
            if (time.Codigo < 1)
            {
                time.Codigo = await _timeRepository.GetNewId();
            }

            return await _timeRepository.CreateAsync(time);  
        }
        public async Task<bool> UpdateAsync(Time time)
        {
            return await _timeRepository.UpdateAsync(time);
        }
        public Task<bool> DeleteAsync(int id)
        {
            return _timeRepository.DeleteAsync(id);
        }
    }
}
