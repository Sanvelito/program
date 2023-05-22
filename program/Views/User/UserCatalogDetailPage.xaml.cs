using program.Helpers;
using program.Models;
using program.ViewModels.User;

namespace program.Views.User;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class UserCatalogDetailPage : ContentPage
{
    public UserCatalogDetailPage()
    {
        InitializeComponent();
    }
    public UserCatalogDetailPage(CompanyDto companyDto)
    {
        InitializeComponent();
        BindingContext = new UserCatalogDetailViewModel(companyDto);
        CompanyDto = companyDto;
        //BindingContext = new ServiceHelper.GetService<UserCatalogDetailViewModel>(companyDto);
    }
    CompanyDto CompanyDto { get; set; }
    private async void NavCreateOrder(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CreateOrderPage(CompanyDto));
    }
}