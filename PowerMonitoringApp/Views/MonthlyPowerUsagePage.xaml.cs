using PowerMonitoringApp.ViewModels;

namespace PowerMonitoringApp.Views;

public partial class MonthlyPowerUsagePage : ContentPage
{
	public MonthlyPowerUsagePage(MonthlyPowerUsageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}