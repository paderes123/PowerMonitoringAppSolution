using CommunityToolkit.Mvvm.ComponentModel;
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
    public partial class LoginViewModel : BaseViewModel
    {
        private IAuthService _authService;

        [ObservableProperty] private string _email;

        [ObservableProperty] private string _password;
        public LoginViewModel(IAuthService authService)
        {
            Title = "Login";
            _authService = authService;
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
            var (isSuccess, message) = await _authService.TryLoginClientAsync(Email, Password);
            if (!isSuccess)
            {
                await Shell.Current.DisplayAlert("Login Failed", message, "OK");
            }
            else
            {
                await Shell.Current.GoToAsync($"//{nameof(PowerMeterPage)}");
            }
        }

        [RelayCommand]
        async Task GoToSignUpAsync()
        {
            await Shell.Current.GoToAsync($"//{nameof(SignUpPage)}");
        }
    }
}
