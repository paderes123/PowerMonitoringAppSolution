using PowerMonitoringApp.ViewModels;

namespace PowerMonitoringApp.Views;

public partial class PowerMeterPage : ContentPage
{
	public PowerMeterPage(PowerMeterViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}