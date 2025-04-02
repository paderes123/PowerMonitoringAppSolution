using PowerMonitoringApp.ViewModels;

namespace PowerMonitoringApp.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
	}
}