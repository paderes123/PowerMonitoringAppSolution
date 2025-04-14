using CommunityToolkit.Mvvm.Input;
using PowerMonitoringApp.Services.Interfaces;
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
        private IAuthService _authService;
        public SignUpViewModel(IAuthService authService)
        {
            _authService = authService;
        }

        [RelayCommand]
        async Task GoToHomePageAsync()
        {
            //_authService
            await Shell.Current.GoToAsync($"//{nameof(PowerMeterPage)}");
        }

        [RelayCommand]
        async Task GoToLoginAsync()
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }
}
