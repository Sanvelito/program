using program.ViewModels.Admin;

namespace program.Views.Admin;

public partial class AdminMainPage : ContentPage
{
	public AdminMainPage(AdminMainViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}