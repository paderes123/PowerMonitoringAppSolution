using CommunityToolkit.Mvvm.Input;
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
        async Task SignUpAsync()
        {

        }
    }
}
