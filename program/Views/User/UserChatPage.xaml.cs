using program.ViewModels.User;

namespace program.Views.User;

public partial class UserChatPage : ContentPage
{
    UserChatViewModel _viewModel;
    public UserChatPage(UserChatViewModel vm)
	{
		InitializeComponent();
		BindingContext = _viewModel = vm;
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.GetMyOrders();
    }
}