using PowerMonitoringApp.ViewModels;

namespace PowerMonitoringApp.Views;

public partial class TodayPowerUsagePage : ContentPage
{
	public TodayPowerUsagePage(TodayPowerUsageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}