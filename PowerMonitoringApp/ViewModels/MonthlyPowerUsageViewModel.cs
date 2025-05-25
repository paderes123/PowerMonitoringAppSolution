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
            Title = "Daily Power Consumption";
            var powerConsumptionData = new ObservableCollection<PowerConsumption>
            {
                new PowerConsumption { Day = "Day 1", Consumption = 4.5 },
                new PowerConsumption { Day = "Day 2", Consumption = 5.0 },
                new PowerConsumption { Day = "Day 3", Consumption = 4.2 },
                new PowerConsumption { Day = "Day 4", Consumption = 5.8 },
                new PowerConsumption { Day = "Day 5", Consumption = 6.1 },
                new PowerConsumption { Day = "Day 6", Consumption = 5.4 },
                new PowerConsumption { Day = "Day 7", Consumption = 4.7 },
                new PowerConsumption { Day = "Day 8", Consumption = 5.0 },
                new PowerConsumption { Day = "Day 9", Consumption = 6.5 },
                new PowerConsumption { Day = "Day 10", Consumption = 6.8 },
                new PowerConsumption { Day = "Day 11", Consumption = 5.9 },
                new PowerConsumption { Day = "Day 12", Consumption = 5.2 },
                new PowerConsumption { Day = "Day 13", Consumption = 4.1 },
                new PowerConsumption { Day = "Day 14", Consumption = 5.7 },
                new PowerConsumption { Day = "Day 15", Consumption = 6.3 },
                new PowerConsumption { Day = "Day 16", Consumption = 7.6 },
                new PowerConsumption { Day = "Day 17", Consumption = 6.4 },
                new PowerConsumption { Day = "Day 18", Consumption = 7.8 },
                new PowerConsumption { Day = "Day 19", Consumption = 6.0 },
                new PowerConsumption { Day = "Day 20", Consumption = 5.5 },
                new PowerConsumption { Day = "Day 21", Consumption = 5.2 },
                new PowerConsumption { Day = "Day 22", Consumption = 4.9 },
                new PowerConsumption { Day = "Day 23", Consumption = 4.7 },
                new PowerConsumption { Day = "Day 24", Consumption = 5.2 },
                new PowerConsumption { Day = "Day 25", Consumption = 5.8 },
                new PowerConsumption { Day = "Day 26", Consumption = 5.5 },
            };

            PowerData = powerConsumptionData;
        }
    }
}
