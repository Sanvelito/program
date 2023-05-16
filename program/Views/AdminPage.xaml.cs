using program.Helpers;
using program.ViewModels;

namespace program.Views;

public partial class AdminPage : ContentPage
{
	public AdminPage()
	{
		InitializeComponent();
        BindingContext = ServiceHelper.GetService<AdminViewModel>();
    }
}