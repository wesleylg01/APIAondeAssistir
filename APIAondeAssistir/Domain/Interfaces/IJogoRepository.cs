using APIAondeAssistir.Domain.Entities;

namespace APIAondeAssistir.Domain.Interfaces
{
    public interface IJogoRepository
    {
        Task<List<Jogo>> GetAllAsync();
        Task<Jogo> GetById(int id);
        Task<bool> CreateAsync(Jogo jogo);
        Task<bool> UpdateAsync(Jogo jogo);
        Task<bool> DeleteAsync(int id);
        Task<int> GetNewId();
    }
}
