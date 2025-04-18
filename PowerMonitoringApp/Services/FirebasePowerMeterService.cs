using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using PowerMonitoringApp.Models;
using PowerMonitoringApp.Services.Interfaces;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PowerMonitoringApp.Services
{
    public class FirebasePowerMeterService : IPowerMeterService
    {
        private readonly FirebaseClient _firebaseClient; // set changes
        private readonly IAuthService _authService;
        public FirebasePowerMeterService(IAuthService authService)
        {
            _authService = authService;
            // Get the auth token registered from Preferences
            var authToken = Preferences.Get("authToken", null);
            _firebaseClient = new FirebaseClient("https://scenic-scholar-380010-default-rtdb.asia-southeast1.firebasedatabase.app/", new FirebaseOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(authToken)
            });
        }

        public async Task<PowerMeter?> GetLatestPowerMeterDataAsync()
        {
            try
            {
                // Get the Uid registered from Preferences
                var uid = Preferences.Get("Uid", null);
                // Fetch the entire PowerMeter node as a single object
                var powerMeterData = await _firebaseClient
                    .Child("Users")
                    .Child(uid)
                    .Child("PowerMeter")
                    .OnceSingleAsync<PowerMeter>();

                return powerMeterData; // Returns null if no data exists
            }
            catch (Exception ex)
            {
                // Log the error (implement logging as needed)
                Trace.WriteLine($"Error fetching PowerMeter data: {ex.Message}");
                return null;
            }
        }
    }
}