using CommunityToolkit.Mvvm.ComponentModel;
using PowerMonitoringApp.Models;
using PowerMonitoringApp.Services.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PowerMonitoringApp.ViewModels
{
    public partial class PowerMeterViewModel : BaseViewModel, IDisposable
    {
        private readonly System.Timers.Timer _timer;
        private int _seconds;
        private bool _isFetching; // Prevent overlapping requests
        private readonly IPowerMeterService _powerMeterService;
        [ObservableProperty] private PowerMeter _powerMeter = new();

        // Observable properties for direct UI binding
        [ObservableProperty] private string _powerAndTimeLabel = "Loading...";

        public ObservableCollection<PowerData> LiveData { get; } = new();

        public PowerMeterViewModel(IPowerMeterService powerMeterService)
        {
            Title = "Power Meter";
            _powerMeterService = powerMeterService;

            _timer = new System.Timers.Timer(1000); // Update every second
            _timer.Elapsed += OnTimedEvent;
            _timer.Start();


            // Initialization for PowerMeterService Realtime data
            // Subscribe to real-time updates
            _powerMeterService.PowerMeterDataChanged += OnPowerMeterDataChanged;
            // Start listening for updates
            _powerMeterService.StartListeningForPowerMeterUpdates();
        }

        private async void OnTimedEvent(object? sender, System.Timers.ElapsedEventArgs e)
        {
            if (_isFetching) return; // Skip if still processing previous fetch
            _isFetching = true;

            try
            {
                _seconds++;
                string time = TimeSpan.FromSeconds(_seconds).ToString(@"hh\:mm\:ss");

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    LiveData.Add(new PowerData { Value = _seconds, Size = PowerMeter.Power });
                    if (LiveData.Count > 20) LiveData.RemoveAt(0);
                    PowerAndTimeLabel = $"Latest Power: {PowerMeter.Power:F2} Watts | Time: {time}" +
                        $"\nPower Factor: {PowerMeter.PowerFactor:F2} | Energy: {PowerMeter.Energy:F2} kWh | Frequency: {PowerMeter.Frequency:F2} Hz";
                });
            }
            catch (Exception ex)
            {
                MainThread.BeginInvokeOnMainThread(() =>
                    PowerAndTimeLabel = $"Error: {ex.Message} | Time: {TimeSpan.FromSeconds(_seconds).ToString(@"hh\:mm\:ss")}");
            }
            finally
            {
                _isFetching = false;
                //_powerMeter = new PowerMeter(); // reset the value of PowerMeter
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _timer.Stop();
                _timer.Elapsed -= OnTimedEvent;
                _powerMeterService.StopListeningForPowerMeterUpdates();
                _powerMeterService.PowerMeterDataChanged -= OnPowerMeterDataChanged;
            }
            base.Dispose(disposing);
        }

        private void OnPowerMeterDataChanged(object? sender, PowerMeter powerMeter)
        {
            PowerMeter = powerMeter;
        }
    }
}