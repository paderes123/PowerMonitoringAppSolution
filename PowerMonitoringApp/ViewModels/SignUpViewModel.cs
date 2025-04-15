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
        [ObservableProperty] private string _confirmPassword;
        [ObservableProperty] private string _displayName;

        [ObservableProperty] private bool _isEmailValid;

        [RelayCommand]
        async Task TryToSignUpPageAsync()
        {
            IsBusy = true;
            if (!IsEmailValid)
            {
                await Shell.Current.DisplayAlert("Invalid Email", "Please enter a valid email.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(DisplayName))
            {
                await Shell.Current.DisplayAlert("Missing Information", "Please fill in the name", "OK");
                return;
            }

            if (ConfirmPassword != Password)
            {
                await Shell.Current.DisplayAlert("Password Mismatch", "The password and confirmation password do not match.", "OK");
                return;
            }

            var (isSuccess, message) = await _authService.TrySignUpClientAsync(Email, Password, DisplayName);
            if (!isSuccess)
            {
                await Shell.Current.DisplayAlert("Sign Up Failed", message, "OK");
                return;
            }
            IsBusy = false;

            await Shell.Current.DisplayAlert("Success", message, "OK");
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }


        [RelayCommand]
        async Task GoToLoginAsync()
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }
}
