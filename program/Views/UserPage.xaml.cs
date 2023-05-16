using program.Helpers;
using program.ViewModels;

namespace program.Views;

public partial class UserPage : ContentPage
{
	public UserPage()
	{
		InitializeComponent();
        BindingContext = ServiceHelper.GetService<UserViewModel>();
    }
}