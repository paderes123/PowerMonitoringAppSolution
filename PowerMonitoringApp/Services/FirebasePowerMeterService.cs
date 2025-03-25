using Firebase.Database;
using Firebase.Database.Query;
using PowerMonitoringApp.Models;
using PowerMonitoringApp.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace PowerMonitoringApp.Services
{
    public class FirebasePowerMeterService : IPowerMeterService
    {
        private readonly FirebaseClient firebaseClient;

        public FirebasePowerMeterService()
        {
            firebaseClient = new FirebaseClient("https://scenic-scholar-380010-default-rtdb.asia-southeast1.firebasedatabase.app/");
        }

        public async Task<PowerMeter?> GetLatestPowerMeterDataAsync()
        {
            try
            {
                // Fetch the entire PowerMeter node as a single object
                var powerMeterData = await firebaseClient
                    .Child("PowerMeter")
                    .OnceSingleAsync<PowerMeter>();

                return powerMeterData; // Returns null if no data exists
            }
            catch (Exception ex)
            {
                // Log the error (implement logging as needed)
                Console.WriteLine($"Error fetching PowerMeter data: {ex.Message}");
                return null;
            }
        }
    }
}