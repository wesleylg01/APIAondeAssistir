using APIAondeAssistir.Domain.Entities;

namespace APIAondeAssistir.Domain.Interfaces
{
    public interface ITimeRepository
    {
        Task<List<Time>> GetAllAsync();
        Task<Time> GetById(int id);
        Task<bool> CreateAsync(Time time);
        Task<bool> UpdateAsync(Time time);
        Task<bool> DeleteAsync(int id);
        Task<int> GetNewId();
    }
}
