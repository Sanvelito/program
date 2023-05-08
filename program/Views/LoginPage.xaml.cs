using program.Helpers;
using program.ViewModel;

namespace program.View;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
		BindingContext = ServiceHelper.GetService<LoginViewModel>();
	}
}