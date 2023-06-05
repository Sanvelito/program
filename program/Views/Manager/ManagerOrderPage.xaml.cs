using program.ViewModels.Manager;

namespace program.Views.Manager;

public partial class ManagerOrderPage : ContentPage
{
	public ManagerOrderPage(ManagerOrderViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}