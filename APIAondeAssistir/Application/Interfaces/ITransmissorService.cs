using APIAondeAssistir.Domain.Entities;

namespace APIAondeAssistir.Application.Interfaces
{
    public interface ITransmissorService
    {
        Task<List<Transmissor>> GetAllAsync();
        Task<Transmissor> GetById(int id);
        Task<bool> CreateAsync(Transmissor transmissor);
        Task<bool> UpdateAsync(Transmissor transmissor);
        Task<bool> DeleteAsync(int id);
    }
}
