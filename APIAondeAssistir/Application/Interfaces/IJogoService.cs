using APIAondeAssistir.Domain.Entities;
using APIAondeAssistir.DTOs;

namespace APIAondeAssistir.Application.Interfaces
{
    public interface IJogoService
    {
        Task<List<Jogo>> GetAll();
        Task<Jogo> GetById(int id);
        Task<bool> CreateAsync(Jogo jogo);
        Task<bool> UpdateAsync(Jogo jogo);
        Task<bool> DeleteAsync(int id);
        Task<List<JogosRodadaDto>> GetByRodada(int rodada);
        Task<List<JogosRodadaDto>> GetJogosListByTime(int time);
        Task<JogoDetail> GetJogoDetails(int time);
        Task<bool> DeleteAnterioresAsync();
    }
}
