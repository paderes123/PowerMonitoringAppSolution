using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerMonitoringApp.ViewModels
{
    public partial class ShellViewModel : BaseViewModel
    {
        [ObservableProperty] private string _displayName;
        [ObservableProperty] private string _email;

        public void InitializeUserProfile()
        {
            DisplayName = Preferences.Get("DisplayName", "");
            Email = Preferences.Get("Email", "");
        }
    }
}
