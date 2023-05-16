using program.Helpers;
using program.ViewModels;

namespace program.Views;

public partial class RegistrationPage : ContentPage
{
	public RegistrationPage()
	{
		InitializeComponent();
        BindingContext = ServiceHelper.GetService<RegistrationViewModel>();
    }
}