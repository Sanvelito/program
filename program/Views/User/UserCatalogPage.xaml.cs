using program.Helpers;
using program.Models;
using program.Services;
using program.ViewModels;
using program.ViewModels.User;

namespace program.Views.User;

public partial class UserCatalogPage : ContentPage
{
    private readonly UserCatalogViewModel _viewModel;
	public UserCatalogPage(UserCatalogViewModel vm)
	{
		InitializeComponent();
        BindingContext = _viewModel = vm;
    }
    //protected override async void OnAppearing()
    //{
    //    base.OnAppearing();

    //    await _viewModel.GetAllCompanies();
    //    //var companies = await ServiceHelper.GetService<UserCatalogViewModel>().GetAllCompanies();
        
    //}
    async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var company = e.SelectedItem as CompanyDto;
        if (company == null)
            return;

        //await Navigation.
        await Navigation.PushAsync(new UserCatalogDetailPage(company));

        ((ListView)sender).SelectedItem = null;
    }
}