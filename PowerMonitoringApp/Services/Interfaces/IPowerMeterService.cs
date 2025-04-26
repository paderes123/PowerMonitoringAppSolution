using PowerMonitoringApp.Models;

namespace PowerMonitoringApp.Services.Interfaces
{
    public interface IPowerMeterService
    {
        // Event to notify subscribers of PowerMeter data changes
        event EventHandler<PowerMeter>? PowerMeterDataChanged;

        // Method to start listening for real-time updates
        void StartListeningForPowerMeterUpdates();

        // Method to stop listening for updates
        void StopListeningForPowerMeterUpdates();
    }
}
