using Firebase.Auth;

namespace PowerMonitoringApp.Services.Interfaces
{
    public class FirebaseAuthService : IAuthService
    {
        private FirebaseAuthClient _firebaseAuthClient;

        public FirebaseAuthService(FirebaseAuthClient firebaseAuthClient)
        {
            _firebaseAuthClient = firebaseAuthClient;
        }

        public async Task<bool> IsValidClientLoginAsync(string email, string password)
        {
            var userCredential = await _firebaseAuthClient.SignInWithEmailAndPasswordAsync(email, password);

            // user and auth properties
            var user = userCredential.User;
            var uid = user.Uid;
            var name = user.Info.DisplayName; // more properties are available in user.Info
            var refreshToken = user.Credential.RefreshToken; // more properties are available in user.Credential
            return true;
        }

        public async Task SignUpClientAsync(string email, string password, string displayName)
        {
            UserCredential? userCredential;
            try
            {
                userCredential = await _firebaseAuthClient.CreateUserWithEmailAndPasswordAsync(email, password, displayName);
            }
            catch (FirebaseAuthHttpException ex)
            {
                await Shell.Current.DisplayAlert("User Info", ex.Reason.ToString(), "OK");
            }
        }
    }
}