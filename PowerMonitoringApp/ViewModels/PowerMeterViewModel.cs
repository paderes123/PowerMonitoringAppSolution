using CommunityToolkit.Mvvm.ComponentModel;
using PowerMonitoringApp.Models;
using PowerMonitoringApp.Services.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PowerMonitoringApp.ViewModels
{
    public partial class PowerMeterViewModel : BaseViewModel, IDisposable
    {
        private readonly System.Timers.Timer _timer;
        private int _seconds;
        private bool _isFetching; // Prevent overlapping requests
        private readonly IPowerMeterService _powerMeterService;

        // Observable properties for direct UI binding
        [ObservableProperty] private double _power = 0;
        [ObservableProperty] private double _powerFactor = 0;
        [ObservableProperty] private double _energy = 0;
        [ObservableProperty] private double _frequency = 0;
        [ObservableProperty] private string _powerAndTimeLabel = "Loading...";

        public ObservableCollection<PowerData> LiveData { get; } = new();

        public PowerMeterViewModel(IPowerMeterService powerMeterService)
        {
            Title = "Power Meter";
            _powerMeterService = powerMeterService;

            _timer = new System.Timers.Timer(1000); // Update every second
            _timer.Elapsed += OnTimedEvent;
            _timer.Start();
        }

        private async void OnTimedEvent(object? sender, System.Timers.ElapsedEventArgs e)
        {
            if (_isFetching) return; // Skip if still processing previous fetch
            _isFetching = true;

            try
            {
                PowerMeter? powerMeter = await _powerMeterService.GetLatestPowerMeterDataAsync();
                _seconds++;
                string time = TimeSpan.FromSeconds(_seconds).ToString(@"hh\:mm\:ss");

                if (powerMeter != null)
                {
                    Power = powerMeter.Power;
                    PowerFactor = powerMeter.PowerFactor;
                    Energy = powerMeter.Energy;
                    Frequency = powerMeter.Frequency;

                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        LiveData.Add(new PowerData { Value = _seconds, Size = Power });
                        if (LiveData.Count > 20) LiveData.RemoveAt(0);
                        PowerAndTimeLabel = $"Latest Power: {Power:F2} Watts | Time: {time}" +
                            $"\nPower Factor: {PowerFactor:F2} | Energy: {Energy:F2} kWh | Frequency: {Frequency:F2} Hz";
                    });
                }
                else
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                        PowerAndTimeLabel = $"Data unavailable | Time: {time}");
                }
            }
            catch (Exception ex)
            {
                MainThread.BeginInvokeOnMainThread(() =>
                    PowerAndTimeLabel = $"Error: {ex.Message} | Time: {TimeSpan.FromSeconds(_seconds).ToString(@"hh\:mm\:ss")}");
            }
            finally
            {
                _isFetching = false;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _timer.Stop();
                _timer.Elapsed -= OnTimedEvent;
            }
            base.Dispose(disposing);
        }
    }

    public class PowerData
    {
        public int Value { get; set; }
        public double Size { get; set; }
    }
}