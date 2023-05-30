using program.Helpers;
using program.ViewModels.User;

namespace program.Views.User;

public partial class UserAccountPage : ContentPage
{
	public UserAccountPage(UserAccountViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}