using APIAondeAssistir.Application.Interfaces;
using APIAondeAssistir.Domain.Entities;
using APIAondeAssistir.Domain.Interfaces;

namespace APIAondeAssistir.Application.Services
{
    public class JogoService : IJogoService
    {
        private readonly IJogoRepository _jogoRepository;
        public JogoService(IJogoRepository jogoRepository)
        {
            _jogoRepository = jogoRepository;
        }

        public async Task<List<Jogo>> GetAll()
        {
            return await _jogoRepository.GetAllAsync();
        }

        public async Task<Jogo> GetById(int id)
        {
            return await _jogoRepository.GetById(id);
        }

        public async Task<bool> CreateAsync(Jogo jogo)
        {
            if (jogo.Codigo < 1)
                jogo.Codigo = await _jogoRepository.GetNewId();

            return await _jogoRepository.CreateAsync(jogo);
        }

        public async Task<bool> UpdateAsync(Jogo jogo)
        {
            return await _jogoRepository.UpdateAsync(jogo);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _jogoRepository.DeleteAsync(id);
        }
    }
}
