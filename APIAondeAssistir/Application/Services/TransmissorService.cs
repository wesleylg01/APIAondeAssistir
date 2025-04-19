using APIAondeAssistir.Application.Interfaces;
using APIAondeAssistir.Domain.Entities;
using APIAondeAssistir.Domain.Interfaces;

namespace APIAondeAssistir.Application.Services
{
    public class TransmissorService : ITransmissorService
    {
        private readonly ITransmissorRepository _transmissorRepository;
        public TransmissorService(ITransmissorRepository transmissorRepository)
        {
            _transmissorRepository = transmissorRepository;
        }
        public Task<List<Transmissor>> GetAllAsync()
        {
            return _transmissorRepository.GetAllAsync();
        }
        public async Task<Transmissor> GetById(int id)
        {
            var transmissor = await _transmissorRepository.GetById(id);
            if ((transmissor == null) || (transmissor.Codigo == 0) || (transmissor.Nome == null))
            {
                throw new KeyNotFoundException("Transmissor não encontrado.");
            }
            return transmissor;
        }

        public Task<bool> CreateAsync(Transmissor transmissor)
        {
            if (transmissor.Codigo < 1)
            {
                transmissor.Codigo = _transmissorRepository.GetNewId().Result;
            }
            return _transmissorRepository.CreateAsync(transmissor);
        }

        public Task<bool> UpdateAsync(Transmissor transmissor)
        {
            return _transmissorRepository.UpdateAsync(transmissor);
        }

        public Task<bool> DeleteAsync(int id)
        {
            return _transmissorRepository.DeleteAsync(id);
        }
    }
}
