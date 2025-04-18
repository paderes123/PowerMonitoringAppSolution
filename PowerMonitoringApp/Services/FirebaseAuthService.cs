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

        public async Task<(bool IsSuccess, string ErrorMessage)> TryLoginClientAsync(string email, string password)
        {
            try
            {
                var userCredential = await _firebaseAuthClient.SignInWithEmailAndPasswordAsync(email, password);

                if (userCredential?.User == null)
                {       
                    return (false, "Invalid user credentials.");
                }
                Preferences.Set("Uid", userCredential.User.Uid);
                Preferences.Set("authToken", await userCredential.User.GetIdTokenAsync());
                return (true, string.Empty);
            }
            catch (FirebaseAuthException ex)
            {
                return (false, $"Login failed: {ex.Reason}");
            }
            catch (Exception ex)
            {
                return (false, $"Unexpected error: {ex.Message}");
            }       
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> TrySignUpClientAsync(string email, string password, string displayName)
        {
            UserCredential? userCredential;
            try
            {
                userCredential = await _firebaseAuthClient.CreateUserWithEmailAndPasswordAsync(email, password, displayName);
                return (true, string.Empty);
            }
            catch (FirebaseAuthHttpException ex)
            {
                return (false, ex.Reason.ToString());
            }
        }
    }
}