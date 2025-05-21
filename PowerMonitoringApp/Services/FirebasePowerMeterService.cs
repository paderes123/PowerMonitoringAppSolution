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

        public void StopListeningForPowerMeterUpdates()
        {
            // Dispose of the subscription if it exists
            _powerMeterSubscription?.Dispose();
            _powerMeterSubscription = null;
        }
    }
}