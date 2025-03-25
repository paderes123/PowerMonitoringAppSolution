//using CommunityToolkit.Mvvm.ComponentModel;
//using PowerMonitoringApp.Models;
//using PowerMonitoringApp.Services.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace PowerMonitoringApp.ViewModels
//{
//    public partial class VoltageAndCurrentMeterViewModel : BaseViewModel
//    {
//        private IPowerMeterService _powerMeterService;
//        private readonly System.Timers.Timer _timer;
//        private int _seconds;
//        private bool _isFetching; // Prevent overlapping requests

//        [ObservableProperty]
//        private float _voltage;

//        [ObservableProperty]
//        private float current;
//        public VoltageAndCurrentMeterViewModel(IPowerMeterService powerMeterService)
//        {
//            Title = "Voltage and Current";
//            _powerMeterService = powerMeterService;

//            _timer = new System.Timers.Timer(1000); // Update every second
//            _timer.Elapsed += OnTimedEvent;
//            _timer.Start();
//        }

//        private async void OnTimedEvent(object? sender, System.Timers.ElapsedEventArgs e)
//        {
//            if (_isFetching) return; // Skip if still processing previous fetch
//            _isFetching = true;

//            try
//            {
//                PowerMeter? powerMeter = await _powerMeterService.GetLatestPowerMeterDataAsync();
//                _seconds++;
//                string time = TimeSpan.FromSeconds(_seconds).ToString(@"hh\:mm\:ss");

//                if (powerMeter != null)
//                {
//                    MainThread.BeginInvokeOnMainThread(() =>
//                    {
//                        Voltage = powerMeter.Voltage;
//                        Current = powerMeter.Current;
//                    });
//                }
//                else
//                {
//                    MainThread.BeginInvokeOnMainThread(() =>
//                    {
//                        Voltage = 0;
//                        Current = 0;
//                    });
//                }
//            }
//            catch (Exception ex)
//            {
//                // add necessary code if possible
//            }
//            finally
//            {
//                _isFetching = false;
//            }
//        }

//        public override void Dispose()
//        {
//            _timer.Stop();
//            _timer.Elapsed -= OnTimedEvent;
//            base.Dispose();
//        }
//    }
//}

using CommunityToolkit.Mvvm.ComponentModel;
using PowerMonitoringApp.Models;
using PowerMonitoringApp.Services.Interfaces;
using System;
using System.Threading.Tasks;
using System.Timers;

namespace PowerMonitoringApp.ViewModels
{
    public partial class VoltageAndCurrentMeterViewModel : BaseViewModel
    {
        private readonly IPowerMeterService _powerMeterService;
        private readonly System.Timers.Timer _timer;
        private int _seconds;
        private bool _isFetching;

        [ObservableProperty]
        private float _voltage;

        [ObservableProperty]
        private float _current; // Fixed naming consistency

        [ObservableProperty]
        private string _status = "Loading..."; // Added for error/data feedback

        public VoltageAndCurrentMeterViewModel(IPowerMeterService powerMeterService)
        {
            Title = "Voltage and Current";
            _powerMeterService = powerMeterService;

            _timer = new System.Timers.Timer(1000); // Update every second
            _timer.Elapsed += OnTimedEvent;
            _timer.Start();
        }

        private async void OnTimedEvent(object? sender, ElapsedEventArgs e)
        {
            if (_isFetching) return;
            _isFetching = true;
            IsBusy = true; // Indicate fetching in progress

            try
            {
                PowerMeter? powerMeter = await _powerMeterService.GetLatestPowerMeterDataAsync();
                _seconds++;
                string time = TimeSpan.FromSeconds(_seconds).ToString(@"hh\:mm\:ss");

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    if (powerMeter != null)
                    {
                        Voltage = powerMeter.Voltage;
                        Current = powerMeter.Current;
                        Status = $"Updated at {time}";
                    }
                    else
                    {
                        Voltage = 0;
                        Current = 0;
                        Status = $"Data unavailable at {time}";
                    }
                });
            }
            catch (Exception ex)
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Voltage = 0;
                    Current = 0;
                    Status = $"Error: {ex.Message} at {TimeSpan.FromSeconds(_seconds).ToString(@"hh\:mm\:ss")}";
                });
                // Optional: Log the error to a file or service
                Console.WriteLine($"Error fetching power meter data: {ex}");
            }
            finally
            {
                _isFetching = false;
                IsBusy = false; // Reset busy state
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
}
