using Microsoft.Extensions.DependencyInjection;
using PowerMonitoringApp.Views;
using Microsoft.Maui.Controls;

namespace PowerMonitoringApp
{
    public partial class App : Application
    {
        public App(IServiceProvider serviceProvider)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NMaF1cXGJCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdmWXxfcnRXQ2heV0BwW0I=");
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}