using APIAondeAssistir.Domain.Entities;
using APIAondeAssistir.Domain.Enums;
using APIAondeAssistir.Domain.Interfaces;
using APIAondeAssistir.Infrastructure.Firebase;
using Firebase.Database;
using Firebase.Database.Query;
using System.Linq;

namespace APIAondeAssistir.Infrastructure.Repositories
{
    public class CampeonatoRepository : ICampeonatoRepository
    {
        private readonly FirebaseClient _firebaseClient;
        public CampeonatoRepository(FirebaseContext context)
        {
            _firebaseClient = context.Client;
        }
        public async Task<List<Campeonato>> GetAllAsync()
        {
            var campeonatos = (await _firebaseClient
                .Child("Campeonato")
                .OnceAsync<Campeonato>()).Select(item => new Campeonato
                {
                    Codigo = item.Object.Codigo,
                    Nome = item.Object.Nome,
                    LogoUrl = item.Object.LogoUrl
                })
                .OrderBy(t => t.Nome)
                .ToList();

            return campeonatos;
        }

        public async Task<Campeonato> GetById(int id)
        {
            var campeonato = (await _firebaseClient
                .Child("Campeonato")
                .OnceAsync<Campeonato>())
                .FirstOrDefault(t => t.Object.Codigo == id)?.Object;

            if (campeonato == null)
            {
                throw new KeyNotFoundException(CampeonatoErrors.CampeonatoNaoEncontrado.GetMessage());
            }

            return campeonato;
        }
        public async Task<bool> CreateAsync(Campeonato campeonato)
        {
            if (campeonato != null)
            {
                return false;
            }

            var created = await _firebaseClient
                .Child("Campeonato")
                .PostAsync(campeonato);

            return created != null;
        }

        public async Task<bool> UpdateAsync(Campeonato campeonato)
        {
            var toUpdate = (await _firebaseClient
                .Child("Campeonato")
                .OnceAsync<Campeonato>())
                .FirstOrDefault(a => a.Object.Codigo == campeonato.Codigo);

            if (toUpdate == null)
            {
                return false;
            }

            await _firebaseClient
                .Child("Campeonato")
                .Child(toUpdate.Key)
                .PutAsync(campeonato);

            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            //obtem a chave do campeonato a ser deletado
            var toDelete = (await _firebaseClient
                .Child("Campeonato")
                .OnceAsync<Campeonato>())
                .FirstOrDefault(a => a.Object.Codigo == id);

            if (toDelete == null)
            {
                return false;
            }
            
            await _firebaseClient
                .Child("Campeonato")
                .Child(toDelete.Key)
                .DeleteAsync();
            
            return true;
        }

        public async Task<int> GetNewId()
        {
            var campeonatos = await GetAllAsync();

            return campeonatos.DefaultIfEmpty(new Campeonato { Codigo = 0 }).Max(t => t.Codigo) + 1;
        }
    }
}
