using CommunityToolkit.Mvvm.Input;
using PowerMonitoringApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerMonitoringApp.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {
        public LoginViewModel()
        {
            Title = "Login";
        }

        [RelayCommand]
        async Task LoginAsync()
        {

        }

        [RelayCommand]
        async Task ForgotPasswordAsync()
        {

        }

        [RelayCommand]
        async Task ContinueWithGoogleAsync()
        {

        }

        [RelayCommand]
        async Task ContinueWithFacebookAsync()
        {

        }

        [RelayCommand]
        async Task GoToHomePageAsync()
        {
            await Shell.Current.GoToAsync($"//{nameof(PowerMeterPage)}");
        }

        [RelayCommand]
        async Task GoToSignUpAsync()
        {
            await Shell.Current.GoToAsync($"//{nameof(SignUpPage)}");
        }
    }
}
