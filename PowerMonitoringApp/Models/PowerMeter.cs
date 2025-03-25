using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerMonitoringApp.Models
{
    public class PowerMeter : ObservableObject
    {
        public float Voltage { get; set; }
        public float Current { get; set; }
        public float Power { get; set; }
        public float Energy { get; set; }
        public float Frequency { get; set; }
        public float PowerFactor { get; set; }
    }
}
