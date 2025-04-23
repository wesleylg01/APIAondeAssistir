using APIAondeAssistir.Application.Interfaces;
using APIAondeAssistir.Domain.Entities;
using APIAondeAssistir.Domain.Interfaces;

namespace APIAondeAssistir.Application.Services
{
    public class CampeonatoService : ICampeonatoService
    {
        private readonly ICampeonatoRepository _campeonatoRepository;
        public CampeonatoService(ICampeonatoRepository campeonatoRepository)
        {
            _campeonatoRepository = campeonatoRepository;
        }
        public async Task<List<Campeonato>> GetAllAsync()
        {
            return await _campeonatoRepository.GetAllAsync();
        }

        public async Task<Campeonato> GetById(int id)
        {
            return await _campeonatoRepository.GetById(id);
        }
        public async Task<bool> CreateAsync(Campeonato campeonato)
        {
            if (campeonato.Codigo < 1)
            {
                var codigo = await _campeonatoRepository.GetNewId();
                campeonato.Codigo = codigo;
            }

            return await _campeonatoRepository.CreateAsync(campeonato);            
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _campeonatoRepository.DeleteAsync(id);
        }

        public async Task<bool> UpdateAsync(Campeonato campeonato)
        {
            return await _campeonatoRepository.UpdateAsync(campeonato);
        }
    }
}
