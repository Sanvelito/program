using program.Helpers;
using program.ViewModels.User;

namespace program.Views.User;

public partial class UserAccountPage : ContentPage
{
    UserAccountViewModel _ViewModel;
    public UserAccountPage(UserAccountViewModel vm)
	{
		InitializeComponent();
        BindingContext = _ViewModel = vm;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _ViewModel.GetMyInfo();
    }
}