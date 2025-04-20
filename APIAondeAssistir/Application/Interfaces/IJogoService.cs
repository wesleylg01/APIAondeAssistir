using APIAondeAssistir.Domain.Entities;

namespace APIAondeAssistir.Application.Interfaces
{
    public interface IJogoService
    {
        Task<List<Jogo>> GetAll();
        Task<Jogo> GetById(int id);
        Task<bool> CreateAsync(Jogo jogo);
        Task<bool> UpdateAsync(Jogo jogo);
        Task<bool> DeleteAsync(int id);
    }
}
