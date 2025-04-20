using APIAondeAssistir.Domain.Entities;

namespace APIAondeAssistir.Domain.Interfaces
{
    public interface ICampeonatoRepository
    {
        Task<List<Campeonato>> GetAllAsync();
        Task<Campeonato> GetById(int id);
        Task<bool> CreateAsync(Campeonato campeonato);
        Task<bool> UpdateAsync(Campeonato campeonato);
        Task<bool> DeleteAsync(int id);
        Task<int> GetNewId();
    }
}
