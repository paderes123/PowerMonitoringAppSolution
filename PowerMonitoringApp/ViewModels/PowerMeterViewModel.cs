//using CommunityToolkit.Mvvm.ComponentModel;
//using PowerMonitoringApp.Services.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.ComponentModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Timers;

//namespace PowerMonitoringApp.ViewModels
//{
//    public partial class PowerMeterViewModel : BaseViewModel
//    {
//        private readonly System.Timers.Timer _timer;
//        private int _seconds;
//        private double _power = 0;
//        private string _time;
//        private double _powerFactor = 0;
//        private double _energy = 0;
//        private double _frequency = 0;
//        private readonly IPowerMeterService _powerMeterService;

//        public ObservableCollection<PowerData> LiveData { get; set; } = [];

//        [ObservableProperty]
//        private string _powerAndTimeLabel;

//        public PowerMeterViewModel(IPowerMeterService powerMeterService)
//        {
//            Title = "Power Meter";
//            _powerMeterService = powerMeterService;

//            _timer = new System.Timers.Timer(1000); // Update every second
//            _timer.Elapsed += OnTimedEvent;
//            _timer.Start();
//        }

//        private async void OnTimedEvent(object? sender, ElapsedEventArgs e)
//        {
//            Models.PowerMeter? powerMeter = await _powerMeterService.GetLatestPowerMeterDataAsync();
//            _seconds++;
//            _time = TimeSpan.FromSeconds(_seconds).ToString(@"hh\:mm\:ss");

//            if (powerMeter != null)
//            {
//                _power = powerMeter.Power;
//                _powerFactor = powerMeter.PowerFactor;
//                _energy = powerMeter.Energy;
//                _frequency = powerMeter.Frequency;
//            }

//            PowerAndTimeLabel = $"Latest Power: {_power:F2} Watts | Time: {_time:F2}" +
//                $"\nPower Factor: {_powerFactor:F2} | Energy: {_energy:F2} kWh | Frequency: {_frequency:F2} Hz";
//            // Ensure the update happens on the main thread
//            MainThread.BeginInvokeOnMainThread(() =>
//            {
//                LiveData.Add(new PowerData { Value = _seconds, Size = _power });
//                // Keep only the last 10 data points
//                if (LiveData.Count > 20)
//                    LiveData.RemoveAt(0);
//                OnPropertyChanged(nameof(LiveData));
//            });
//        }

//        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
//        {
//            base.OnPropertyChanged(e);
//        }
//    }

//    public class PowerData
//    {
//        public int Value { get; set; }
//        public double Size { get; set; }
//    }
//}


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