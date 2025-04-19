using APIAondeAssistir.Domain.Entities;
using APIAondeAssistir.Domain.Interfaces;
using APIAondeAssistir.Infrastructure.Firebase;
using Firebase.Database;
using Firebase.Database.Query;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

namespace APIAondeAssistir.Infrastructure.Repositories
{
    public class TransmissorRepository : ITransmissorRepository
    {
        private readonly FirebaseClient _firebaseClient;
        public TransmissorRepository(FirebaseContext context)
        {
            _firebaseClient = context.Client;
        }
        public async Task<List<Transmissor>> GetAllAsync()
        {
            var transmissor = (await _firebaseClient
                .Child("Transmissor")
                .OnceAsync<Transmissor>()).Select(item => new Transmissor
                {
                    Codigo = item.Object.Codigo,
                    Nome = item.Object.Nome,
                    ImageUrl = item.Object.ImageUrl,
                    Url = item.Object.Url,
                    Gratis = item.Object.Gratis
                })
                .OrderBy(t => t.Nome)
                .ToList();

            return transmissor;
        }

        public async Task<Transmissor> GetById(int id)
        {
            var transmissor = (await _firebaseClient
                .Child("Transmissor")
                .OnceAsync<Transmissor>()).Select(item => new Transmissor
                {
                    Codigo = item.Object.Codigo,
                    Nome = item.Object.Nome,
                    ImageUrl = item.Object.ImageUrl,
                    Url = item.Object.Url,
                    Gratis = item.Object.Gratis
                })
                .FirstOrDefault(t => t.Codigo == id);
            if(transmissor == null)
            {
                throw new KeyNotFoundException("Transmissor não encontrado.");
            }
            return transmissor;
        }

        public async Task<bool> CreateAsync(Transmissor transmissor)
        {
            if (transmissor == null)
            {
                return false;
            }
            var created = await _firebaseClient
                .Child("Transmissor")
                .PostAsync(transmissor);
            
            return created != null;
        }

        public async Task<bool> UpdateAsync(Transmissor transmissor)
        {
            var toUpdate = (await _firebaseClient
                .Child("Transmissor")
                .OnceAsync<Transmissor>())
                .FirstOrDefault(a => a.Object.Codigo == transmissor.Codigo);
            
            if (toUpdate == null)
            {
                return false;
            }

            await _firebaseClient
                .Child("Transmissor")
                .Child(toUpdate.Key)
                .PutAsync(toUpdate);
            
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            //obtem a chave do transmissor a ser deletado
            var toDelete = (await _firebaseClient
                .Child("Transmissor")
                .OnceAsync<Transmissor>())
                .FirstOrDefault(a => a.Object.Codigo == id);

            if (toDelete == null)
            {
                return false;
            }

            await _firebaseClient
                .Child("Transmissor")
                .Child(toDelete.Key)
                .DeleteAsync();

            return true;
        }

        public async Task<int> GetNewId()
        {
            var transmissores = await GetAllAsync();

            return transmissores.DefaultIfEmpty(new Transmissor { Codigo = 0 }).Max(t => t.Codigo) + 1;
        }
    }
}
