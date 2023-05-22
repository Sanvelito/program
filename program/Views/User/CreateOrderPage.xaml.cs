using program.Helpers;
using program.Models;
using program.Services;
using program.ViewModels.User;

namespace program.Views.User;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class CreateOrderPage : ContentPage
{
	public CreateOrderPage()
	{
		InitializeComponent();
	}
    public CreateOrderPage(CompanyDto companyDto)
    {
        InitializeComponent();
        BindingContext = new UserCreateOrderViewModel(companyDto);
        //BindingContext = new ServiceHelper.GetService<UserCreateOrderViewModel>(companyDto);
    }
}