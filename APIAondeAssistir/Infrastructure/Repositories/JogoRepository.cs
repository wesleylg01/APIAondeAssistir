using APIAondeAssistir.Domain.Entities;
using APIAondeAssistir.Domain.Enums;
using APIAondeAssistir.Domain.Interfaces;
using APIAondeAssistir.Infrastructure.Firebase;
using Firebase.Database;
using Firebase.Database.Query;
using System.Linq;

namespace APIAondeAssistir.Infrastructure.Repositories
{
    public class JogoRepository : IJogoRepository
    {
        private readonly FirebaseClient _firebaseClient;

        public JogoRepository(FirebaseContext context)
        {
            _firebaseClient = context.Client;
        }

        public async Task<List<Jogo>> GetAllAsync()
        {
            var jogo = (await _firebaseClient
                .Child("Jogo")
                .OnceAsync<Jogo>()).Select(item => new Jogo
                {
                    Codigo = item.Object.Codigo,
                    CodigoTime1 = item.Object.CodigoTime1,
                    CodigoTime2 = item.Object.CodigoTime2,
                    DataDia = item.Object.DataDia,
                    CodigoCampeonato = item.Object.CodigoCampeonato,
                    isVisible = item.Object.isVisible,
                    CodigoTransmissors = item.Object.CodigoTransmissors,
                    JogoUrl = item.Object.JogoUrl,
                    Rodada = item.Object.Rodada,
                })
                .OrderBy(t => t.DataDia)
                .ToList();

            return jogo;
        }

        public async Task<Jogo> GetById(int id)
        {
            var jogo = (await _firebaseClient
                .Child("Jogo")
                .OnceAsync<Jogo>())
                .FirstOrDefault(t => t.Object.Codigo == id)?.Object;

            if (jogo == null)
            {
                throw new KeyNotFoundException(CampeonatoErrors.CampeonatoNaoEncontrado.GetMessage());
            }

            return jogo;
        }

        public async Task<bool> CreateAsync(Jogo jogo)
        {
            if (jogo != null)
            {
                return false;
            }

            var created = await _firebaseClient
                .Child("Jogo")
                .PostAsync(jogo);

            return created != null;
        }

        public async Task<bool> UpdateAsync(Jogo jogo)
        {
            var toUpdate = (await _firebaseClient
                .Child("Jogo")
                .OnceAsync<Jogo>())
                .FirstOrDefault(a => a.Object.Codigo == jogo.Codigo);

            if (toUpdate == null)
            {
                return false;
            }

            await _firebaseClient
                .Child("Jogo")
                .Child(toUpdate.Key)
                .PutAsync(jogo);

            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            //obtem a chave do jogo a ser deletado
            var toDelete = (await _firebaseClient
                .Child("Jogo")
                .OnceAsync<Jogo>())
                .FirstOrDefault(a => a.Object.Codigo == id);

            if (toDelete == null)
            {
                return false;
            }

            await _firebaseClient
                .Child("Jogo")
                .Child(toDelete.Key)
                .DeleteAsync();

            return true;
        }

        public async Task<int> GetNewId()
        {
            var campeonatos = await GetAllAsync();

            return campeonatos.DefaultIfEmpty(new Jogo { Codigo = 0 }).Max(t => t.Codigo) + 1;
        }
    }
}
