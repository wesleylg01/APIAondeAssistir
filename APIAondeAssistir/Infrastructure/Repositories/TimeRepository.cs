using APIAondeAssistir.Domain.Entities;
using APIAondeAssistir.Domain.Enums;
using APIAondeAssistir.Domain.Interfaces;
using APIAondeAssistir.Infrastructure.Firebase;
using Firebase.Database;
using Firebase.Database.Query;

namespace APIAondeAssistir.Infrastructure.Repositories
{
    public class TimeRepository: ITimeRepository
    {
        private readonly FirebaseClient _firebaseClient;

        public TimeRepository(FirebaseContext context)
        {
            _firebaseClient = context.Client;
        }
        public async Task<List<Time>> GetAllAsync()
        {
            var times = (await _firebaseClient
                .Child("Time")
                .OnceAsync<Time>()).Select(item => new Time
                {
                    Codigo = item.Object.Codigo,
                    Nome = item.Object.Nome,
                    EscudoUrl = item.Object.EscudoUrl
                })
                .OrderBy(t => t.Nome)
                .ToList();

            return times;
        }
        public async Task<Time> GetById(int id)
        {
            var time = (await _firebaseClient
                .Child("Time")
                .OnceAsync<Time>())
                .FirstOrDefault(t => t.Object.Codigo == id)?.Object;

            if (time == null)
            {
                throw new KeyNotFoundException(TransmissorErrors.TransmissorNaoEncontrado.GetMessage());
            }

            return time;
        }

        public async Task<bool> CreateAsync (Time time)
        {
            if (time != null)
            {
                return false;
            }

            var created = await _firebaseClient
                .Child("Time")
                .PostAsync(time);

            return created != null;
        }
        public async Task<bool> UpdateAsync(Time time)
        {
            var toUpdate = (await _firebaseClient
                .Child("Time")
                .OnceAsync<Time>())
                .FirstOrDefault(a => a.Object.Codigo == time.Codigo);

            if (toUpdate == null)
            {
                return false; 
            }

            await _firebaseClient
                .Child("Time")
                .Child(toUpdate.Key)
                .PutAsync(time);

            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            //obtem a time a ser deletado
            var toDelete = (await _firebaseClient
                .Child("Time")
                .OnceAsync<Time>())
                .FirstOrDefault(a => a.Object.Codigo == id);

            if (toDelete == null)
            {
                return false;
            }

            await _firebaseClient
                .Child("Time")
                .Child(toDelete.Key)
                .DeleteAsync();

            return true;
        }
        public async Task<int> GetNewId()
        {
            var times = await GetAllAsync();

            return times.DefaultIfEmpty(new Time { Codigo = 0 }).Max(t => t.Codigo) + 1;
        }
    }
}
