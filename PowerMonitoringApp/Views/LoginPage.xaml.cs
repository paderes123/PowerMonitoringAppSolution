namespace PowerMonitoringApp.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.DisplayAlert("Alert", "This is a Shell Alert!", "OK");
    }
}