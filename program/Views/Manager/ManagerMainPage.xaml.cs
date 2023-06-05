using program.ViewModels.Manager;

namespace program.Views.Manager;

public partial class ManagerMainPage : ContentPage
{
	public ManagerMainPage(ManagerMainViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}