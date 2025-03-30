using APIAondeAssistir.Domain.Entities;

namespace APIAondeAssistir.Application.Interfaces
{
    public interface ITimeService
    {
        Task<List<Time>> GetAllAsync();
        Task<Time> GetById(int id);
        Task<bool> CreateAsync(Time time);
        Task<bool> UpdateAsync(Time time);
        Task<bool> DeleteAsync(int id);
    }
}
