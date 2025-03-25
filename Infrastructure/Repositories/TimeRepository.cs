using APIAondeAssistir.Domain.Entities;
using APIAondeAssistir.Domain.Interfaces;
using APIAondeAssistir.Infrastructure.Firebase;
using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.AspNetCore.Mvc;

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
                .OrderBy(t => t.Codigo)
                .ToList();

            return times;
        }

        public Task<Time> GetById(int id)
        {
            var times = GetAllAsync().Result;
            if (times.Count == 0)
            {
                return Task.FromResult<Time>(null);
            }

            return Task.FromResult(times.FirstOrDefault(t => t.Codigo == id));
        }

        public async Task<bool> CreateAsync (Time time)
        {
            var result = await _firebaseClient
                .Child("Time")
                .PostAsync(time);

            return (result != null);
        }

        public async Task<bool> UpdateAsync(Time time)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetNewId()
        {
            var times = await GetAllAsync();

            return times.Max(t => t.Codigo) + 1;
        }
    }
}
