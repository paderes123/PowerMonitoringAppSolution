using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerMonitoringApp.Models
{
    public class TodayPowerData
    {
        public DateTime DateTimeRecorded { get; set; }
        public double Power { get; set; }
    }
}
