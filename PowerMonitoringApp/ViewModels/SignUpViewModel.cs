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
        [ObservableProperty] private string _otherErrorMessage;

        [ObservableProperty] private bool _isDisplayNameValid;
        [ObservableProperty] private bool _isEmailValid;
        [ObservableProperty] private bool _isNotValidSignUp;


        [RelayCommand]
        async Task TryToSignUpPageAsync()
        {
            IsBusy = true;
            if (!IsEmailValid)
            {
                IsBusy = false;
                return;
            }

            if (!IsDisplayNameValid)
            {
                IsBusy = false;
                return;
            }

            if (ConfirmPassword != Password)
            {
                IsNotValidSignUp = true;
                OtherErrorMessage = "The password and confirmation password do not match.";
                IsBusy = false;
                return;
            }

            var (isSuccess, message) = await _authService.TrySignUpClientAsync(Email, Password, DisplayName);
            if (!isSuccess)
            {
                IsNotValidSignUp = true;
                OtherErrorMessage = message;
                IsBusy = false;
                return;
            }
            IsBusy = false;
            IsNotValidSignUp = false;
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
