using PowerMonitoringApp.Models;
using PowerMonitoringApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerMonitoringApp.ViewModels
{
    public partial class TodayPowerUsageViewModel : BaseViewModel
    {
        private IPowerMeterService _powerMeterService;
        public ObservableCollection<TodayPowerData> TodayPowerData { get; set; }

        public TodayPowerUsageViewModel(IPowerMeterService powerMeterService)
        {
            Title = "Power Consumption Today";
            _powerMeterService = powerMeterService;

            var listtodaypowerData = _powerMeterService.GetTodayPowerData();
    //        var startDate = new DateTime(2025, 5, 23, 0, 0, 0);
    //        var interval = TimeSpan.FromMinutes(5);
    //        int totalItems = 288;

    //        // Extend or loop values to match 288 entries
    //        var baseValues = new double[]
    //        {
    //1.02, 0.98, 1.05, 0.99, 1.03, 1.01, 0.97, 1.04, 1.00, 0.98,
    //1.02, 1.01, 0.99, 1.03, 1.00, 0.97, 1.02, 1.01, 0.99, 1.04,
    //1.00, 0.98, 1.02, 1.01, 0.99, 1.03, 1.00, 0.97, 1.02, 1.01,
    //0.99, 1.04, 1.00, 0.98, 1.02, 1.01, 0.99, 1.03, 1.00, 0.97,
    //1.02, 1.01, 0.99, 1.04, 1.00, 0.98, 1.02, 1.01, 0.99, 1.03,
    //1.00, 0.97, 1.02, 1.01, 0.99, 1.04, 1.00, 0.98, 1.02, 1.01,
    //2.00, 2.03, 2.05
    //        };
    //        var values = new double[totalItems];
    //        for (int i = 0; i < totalItems; i++)
    //        {
    //            values[i] = baseValues[i % baseValues.Length]; // Repeat baseValues if not enough
    //        }

    //        TodayPowerData = new ObservableCollection<TodayPowerData>();

    //        for (int i = 0; i < totalItems; i++)
    //        {
    //            TodayPowerData.Add(new TodayPowerData
    //            {
    //                Date = startDate.AddMinutes(i * 5),
    //                Value = values[i]
    //            });
    //        }


        }
    }
}