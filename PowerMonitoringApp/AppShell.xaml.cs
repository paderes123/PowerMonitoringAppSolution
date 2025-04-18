using Microsoft.Maui.Controls;
using PowerMonitoringApp.ViewModels;
using PowerMonitoringApp.Views;

namespace PowerMonitoringApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Manually retrieve the registered service
            ShellViewModel viewModel = IPlatformApplication.Current!.Services.GetRequiredService<ShellViewModel>();
            BindingContext = viewModel; 
        }
    }
}