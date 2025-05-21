using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PowerMonitoringApp.Models;
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
        private IPowerMeterService _powerMeterService;
        public SignUpViewModel(IAuthService authService, IPowerMeterService powerMeterService)
        {
            _authService = authService;
            _powerMeterService = powerMeterService;
        }

        [ObservableProperty] private string _email;
        [ObservableProperty] private string _firstName;
        [ObservableProperty] private string _lastName;
        [ObservableProperty] private string _middleName;
        [ObservableProperty] private string _address;

        [ObservableProperty] private string _password;
        [ObservableProperty] private string _confirmPassword;
        [ObservableProperty] private string _otherErrorMessage;

        [ObservableProperty] private bool _isEmailValid;
        [ObservableProperty] private bool _isFirstNameValid;
        [ObservableProperty] private bool _isLastNameValid;
        [ObservableProperty] private bool _isMiddleNameValid;
        [ObservableProperty] private bool _isAddressValid;
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

            if (!IsFirstNameValid)
            {
                IsBusy = false;
                return;
            }

            if (!IsLastNameValid)
            {
                IsBusy = false;
                return;
            }

            if (!IsAddressValid)
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

            var (isSuccess, message) = await _authService.TrySignUpClientAsync(Email, Password, FirstName, LastName, MiddleName, Address);
            if (!isSuccess)
            {
                IsNotValidSignUp = true;
                OtherErrorMessage = message;
                IsBusy = false;
                return;
            }
            if (isSuccess)
            {
                var (isSucessLogin, _) = await _authService.TryLoginClientAsync(Email, Password);
                var uid = Preferences.Get("Uid", null);
                if (string.IsNullOrEmpty(uid))
                    return;
                if (isSucessLogin)
                {
                    var personalInfo = new PersonalInfo()
                    {
                        Email = Email,
                        FirstName = FirstName,
                        LastName = LastName,
                        MiddleName = MiddleName,
                        Address = Address
                    };
                    // why it pauses here it says null reference exception
                    await _powerMeterService.AddPersonalInfo(uid!, personalInfo);
                }
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
