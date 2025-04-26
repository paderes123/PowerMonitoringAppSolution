using CommunityToolkit.Mvvm.ComponentModel;
using PowerMonitoringApp.Models;
using PowerMonitoringApp.Services.Interfaces;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Timers;

namespace PowerMonitoringApp.ViewModels
{
    public partial class VoltageAndCurrentMeterViewModel : BaseViewModel
    {
        private readonly IPowerMeterService _powerMeterService;
        private readonly System.Timers.Timer _timer;
        private bool _isFetching;

        [ObservableProperty]
        private float _voltage;

        [ObservableProperty]
        private float _current; // Fixed naming consistency

        [ObservableProperty]
        private PowerMeter _powerMeter = new();

        public VoltageAndCurrentMeterViewModel(IPowerMeterService powerMeterService)
        {
            Title = "Voltage and Current";
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

        private async void OnTimedEvent(object? sender, ElapsedEventArgs e)
        {
            if (_isFetching) return;
            _isFetching = true;
            IsBusy = true; // Indicate fetching in progress

            try
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    if (PowerMeter != null)
                    {
                        Voltage = PowerMeter.Voltage;
                        Current = PowerMeter.Current;
                    }
                    else
                    {
                        Voltage = 0;
                        Current = 0;
                    }
                });
            }
            catch (Exception ex)
            {
                Trace.WriteLine($"Error fetching power meter data: {ex}");
            }
            finally
            {
                _isFetching = false;
                IsBusy = false; // Reset busy state
            }
        }

        private void OnPowerMeterDataChanged(object? sender, PowerMeter powerMeter)
        {
            PowerMeter = powerMeter;
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
    }
}
