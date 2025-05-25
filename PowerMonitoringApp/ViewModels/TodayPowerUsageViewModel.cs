using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls.Platform.Compatibility;
using PowerMonitoringApp.Models;
using PowerMonitoringApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Threading.Tasks;
using System.Text;
using System.Threading.Tasks;

namespace PowerMonitoringApp.ViewModels
{
    public partial class TodayPowerUsageViewModel : BaseViewModel
    {
        private readonly IPowerMeterService _powerMeterService;

        [ObservableProperty]
        private ObservableCollection<TodayPowerData> _todayPowerData = new();

        public TodayPowerUsageViewModel(IPowerMeterService powerMeterService)
        {
            Title = "Power Consumption Today";
            _powerMeterService = powerMeterService;
            TodayPowerData = new ObservableCollection<TodayPowerData>();
            _ = LoadData(); // fire and forget
        }

        [RelayCommand]
        public async Task LoadData()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            var data = await _powerMeterService.GetTodayPowerData();

            TodayPowerData.Clear();
            foreach (var item in data)
            {
                TodayPowerData.Add(item);
            }
            IsBusy = false;
        }
    }

}