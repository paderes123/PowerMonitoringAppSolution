
namespace PowerMonitoringApp.Services.Interfaces
{
    public class FirebaseAuthService : IAuthService
    {
        public async Task<bool> IsAuthenticatedAsync()
        {
            await Task.Delay(1000);
            return false;
        }
    }
}