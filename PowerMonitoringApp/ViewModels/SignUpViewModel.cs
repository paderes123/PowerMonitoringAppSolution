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
    public partial class SignUpViewModel : BaseViewModel
    {
        private IAuthService _authService;
        public SignUpViewModel(IAuthService authService)
        {
            _authService = authService;
        }

        [ObservableProperty] private string _email;
        [ObservableProperty] private string _password;
        [ObservableProperty] private string _displayName;

        [RelayCommand]
        async Task TryToSignUpPageAsync()
        {
            var(isSuccess, message) = await _authService.TrySignUpClientAsync(Email, Password, DisplayName);
            if (!isSuccess)
            {
                await Shell.Current.DisplayAlert("Sign up Failed", message, "OK");
            }
            else
            {
                await Shell.Current.DisplayAlert("Successfully Signed up", message, "OK");
                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            }
        }

        [RelayCommand]
        async Task GoToLoginAsync()
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }
}
