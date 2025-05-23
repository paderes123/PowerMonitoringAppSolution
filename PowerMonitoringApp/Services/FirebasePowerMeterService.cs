using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.Maui.Storage;
using Newtonsoft.Json.Linq;
using PowerMonitoringApp.Models;
using PowerMonitoringApp.Services.Interfaces;
using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace PowerMonitoringApp.Services
{
    public class FirebasePowerMeterService : IPowerMeterService
    {
        private FirebaseClient _firebaseClient = null!; 
        private IDisposable? _powerMeterSubscription; // To manage the subscription lifecycle
        public event EventHandler<PowerMeter>? PowerMeterDataChanged; // Event for real-time updates
        public FirebasePowerMeterService()
        {
            InitializeFirebaseClient();
        }

        private void InitializeFirebaseClient()
        {
            // Get the auth token registered from Preferences
            var authToken = Preferences.Get("AuthToken", null);
            _firebaseClient = new FirebaseClient("https://scenic-scholar-380010-default-rtdb.asia-southeast1.firebasedatabase.app/", new FirebaseOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(authToken)
            });
        }

        public void StartListeningForPowerMeterUpdates()
        {
            // Stop any existing subscription to avoid duplicates
            StopListeningForPowerMeterUpdates();

            // Get the Uid from Preferences
            var uid = Preferences.Get("Uid", null);
            if (string.IsNullOrEmpty(uid))
                return;

            // Subscribe to real-time updates for the PowerMeter node
            _powerMeterSubscription = _firebaseClient
            .Child("ElectricPowerConsumers")
            .Child(uid)
            .Child("LatestPowerMeter")
            .AsObservable<PowerMeter>()
            .Subscribe(
                p =>
                {
                    PowerMeter powerMeterData = p.Object; 
                    if (powerMeterData != null)
                    {
                        PowerMeterDataChanged?.Invoke(this, powerMeterData);
                    }
                }
            );
        }

        public async Task AddPersonalInfo(string uid, PersonalInfo personalInfo)
        {
            if (string.IsNullOrEmpty(uid))
                return;

            await _firebaseClient
              .Child("ElectricPowerConsumers")
              .Child(uid)
              .Child("PersonalInfo")
              .PutAsync(personalInfo);
        }

        public async Task<List<TodayPowerData>> GetTodayPowerData()
        {
            // Get the Uid from Preferences
            var uid = Preferences.Get("Uid", null);

            var powerDataList = new List<TodayPowerData>();

            // Fetch data for the specific UID
            var consumer = await _firebaseClient
                .Child("ElectricPowerConsumers")
                .Child(uid)
                .OnceSingleAsync<dynamic>();

            if (consumer != null)
            {
                var powerMeterHistory = consumer?.PowerMeterHistory;

                if (powerMeterHistory != null)
                {
                    // Convert to JObject for processing
                    JObject historyObj = JObject.FromObject(powerMeterHistory);

                    // Iterate through the DateTimeAndPower entries
                    foreach (var entry in historyObj["DateTimeAndPower"])
                    {
                        var dataPoint = entry.First;
                        powerDataList.Add(new TodayPowerData
                        {
                            DateTimeRecorded = DateTime.Parse(dataPoint["DateTimeRecorded"].ToString()),
                            Power = Convert.ToDouble(dataPoint["Power"])
                        });
                    }
                }
            }
            return powerDataList;
        }

        public void StopListeningForPowerMeterUpdates()
        {
            // Dispose of the subscription if it exists
            _powerMeterSubscription?.Dispose();
            _powerMeterSubscription = null;
        }
    }
}