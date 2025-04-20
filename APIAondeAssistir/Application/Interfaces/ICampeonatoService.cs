using APIAondeAssistir.Domain.Entities;

namespace APIAondeAssistir.Application.Interfaces
{
    public interface ICampeonatoService
    {
        Task<List<Campeonato>> GetAllAsync();
        Task<Campeonato> GetById(int id);
        Task<bool> CreateAsync(Campeonato campeonato);
        Task<bool> UpdateAsync(Campeonato campeonato);
        Task<bool> DeleteAsync(int id);
    }
}
