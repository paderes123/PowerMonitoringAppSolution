using PowerMonitoringApp.ViewModels;

namespace PowerMonitoringApp.Views;

public partial class VoltageAndCurrentMeterPage : ContentPage
{
	public VoltageAndCurrentMeterPage(VoltageAndCurrentMeterViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}