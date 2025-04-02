using PowerMonitoringApp.ViewModels;

namespace PowerMonitoringApp.Views;

public partial class SignUpPage : ContentPage
{
	public SignUpPage(SignUpViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}