using PowerMonitoringApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerMonitoringApp.ViewModels
{
    public partial class MonthlyPowerUsageViewModel : BaseViewModel
    {
        public ObservableCollection<PowerConsumption> PowerData { get; set; }

        public MonthlyPowerUsageViewModel()
        {
            var powerConsumptionData = new ObservableCollection<PowerConsumption>
            {
                new PowerConsumption { Day = "Day 1", Consumption = 25.5 },
                new PowerConsumption { Day = "Day 2", Consumption = 1000.0 },
                new PowerConsumption { Day = "Day 3", Consumption = 30.2 },
                new PowerConsumption { Day = "Day 4", Consumption = 27.8 },
                new PowerConsumption { Day = "Day 5", Consumption = 32.1 },
                new PowerConsumption { Day = "Day 6", Consumption = 29.4 },
                new PowerConsumption { Day = "Day 7", Consumption = 26.7 },
                new PowerConsumption { Day = "Day 8", Consumption = 31.0 },
                new PowerConsumption { Day = "Day 9", Consumption = 33.5 },
                new PowerConsumption { Day = "Day 10", Consumption = 30.8 },
                new PowerConsumption { Day = "Day 11", Consumption = 28.9 },
                new PowerConsumption { Day = "Day 12", Consumption = 34.2 },
                new PowerConsumption { Day = "Day 13", Consumption = 36.1 },
                new PowerConsumption { Day = "Day 14", Consumption = 32.7 },
                new PowerConsumption { Day = "Day 15", Consumption = 29.3 },
                new PowerConsumption { Day = "Day 16", Consumption = 27.6 },
                new PowerConsumption { Day = "Day 17", Consumption = 31.4 },
                new PowerConsumption { Day = "Day 18", Consumption = 33.8 },
                new PowerConsumption { Day = "Day 19", Consumption = 35.0 },
                new PowerConsumption { Day = "Day 20", Consumption = 30.5 },
                new PowerConsumption { Day = "Day 21", Consumption = 28.2 },
                new PowerConsumption { Day = "Day 22", Consumption = 26.9 },
                new PowerConsumption { Day = "Day 23", Consumption = 29.7 },
                new PowerConsumption { Day = "Day 24", Consumption = 31.2 },
                new PowerConsumption { Day = "Day 25", Consumption = 34.8 },
                new PowerConsumption { Day = "Day 26", Consumption = 36.5 },
                new PowerConsumption { Day = "Day 27", Consumption = 33.3 },
                new PowerConsumption { Day = "Day 28", Consumption = 30.1 },
                new PowerConsumption { Day = "Day 29", Consumption = 27.4 },
                new PowerConsumption { Day = "Day 30", Consumption = 25.8 }
            };

            PowerData = powerConsumptionData;
        }
    }
}
