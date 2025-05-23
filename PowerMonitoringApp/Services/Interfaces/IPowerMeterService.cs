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

        // Method to add personal information of electric power consumers
        Task AddPersonalInfo(string uid, PersonalInfo personalInfo);

        Task<List<TodayPowerData>> GetTodayPowerData();
    }
}
