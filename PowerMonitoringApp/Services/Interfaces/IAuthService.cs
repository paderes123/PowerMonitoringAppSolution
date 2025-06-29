﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerMonitoringApp.Services.Interfaces
{
    public interface IAuthService
    {
        //Task<(string Uid, string IdToken)?> SignUpAsync(string email, string password);
        //Task<(string Uid, string IdToken)?> SignInAsync(string email, string password);
        //Task<(string Uid, string IdToken)?> SignInWithGoogleAsync(string googleIdToken);
        //Task<(string Uid, string IdToken)?> SignInWithFacebookAsync(string facebookAccessToken); // Added for Facebook
        //Task ResetPasswordAsync(string email);
        Task<(bool IsSuccess, string ErrorMessage)> TryLoginClientAsync(string email, string password);
        Task<(bool IsSuccess, string ErrorMessage)> TrySignUpClientAsync(string email, string password, string firstName, string lastName, string middleName, string address);
    }

}
