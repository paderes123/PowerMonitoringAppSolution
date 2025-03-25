using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerMonitoringApp.Services.Interfaces
{
    public interface IPowerMeterService
    {
        Task<Models.PowerMeter?> GetLatestPowerMeterDataAsync();
    }
}
