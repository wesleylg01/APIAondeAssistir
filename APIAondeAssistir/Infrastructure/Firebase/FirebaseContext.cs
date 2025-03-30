using Firebase.Database;

namespace APIAondeAssistir.Infrastructure.Firebase
{
    public class FirebaseContext
    {
        private readonly FirebaseClient _firebaseClient;

        public FirebaseContext(IConfiguration configuration)
        {
            var firebaseUrl = configuration["Firebase:DatabaseUrl"];
            _firebaseClient = new FirebaseClient(firebaseUrl);
        }

        public FirebaseClient Client => _firebaseClient;
    }
}
