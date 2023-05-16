using program.Helpers;
using program.ViewModels;

namespace program.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
		BindingContext = ServiceHelper.GetService<LoginViewModel>();
	}
    protected override bool OnBackButtonPressed()
    {
        Application.Current.Quit();
        return true;
    }
}