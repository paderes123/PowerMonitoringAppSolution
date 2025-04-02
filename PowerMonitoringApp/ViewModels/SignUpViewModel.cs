using CommunityToolkit.Mvvm.Input;
using PowerMonitoringApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerMonitoringApp.ViewModels
{
    public partial class SignUpViewModel : BaseViewModel
    {
        [RelayCommand]
        async Task GoToHomePageAsync()
        {
            await Shell.Current.GoToAsync($"//{nameof(PowerMeterPage)}");
        }

        [RelayCommand]
        async Task GoToLoginAsync()
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }
}
