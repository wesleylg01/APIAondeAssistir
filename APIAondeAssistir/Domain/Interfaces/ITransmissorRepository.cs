using APIAondeAssistir.Domain.Entities;

namespace APIAondeAssistir.Domain.Interfaces
{
    public interface ITransmissorRepository
    {
        Task<List<Transmissor>> GetAllAsync();
        Task<Transmissor> GetById(int id);
        Task<bool> CreateAsync(Transmissor transmissor);
        Task<bool> UpdateAsync(Transmissor transmissor);
        Task<bool> DeleteAsync(int id);
        Task<int> GetNewId();
    }
}
