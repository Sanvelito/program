using program.ViewModels.Manager;

namespace program.Views.Manager;

public partial class ManagerCompanyPage : ContentPage
{
	public ManagerCompanyPage(ManagerCompanyViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}