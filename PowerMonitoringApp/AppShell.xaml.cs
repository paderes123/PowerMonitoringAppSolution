using Microsoft.Maui.Controls;
using PowerMonitoringApp.ViewModels;
using PowerMonitoringApp.Views;

namespace PowerMonitoringApp
{
    public partial class AppShell : Shell
    {
        public AppShell(ShellViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}