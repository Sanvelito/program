using AndroidX.Lifecycle;
using program.Models;
using program.ViewModels.Admin;

namespace program.Views.Admin;

public partial class AdminManageCompanyPage : ContentPage
{
    AdminManageCompanyViewModel _ViewModel;
    public AdminManageCompanyPage(AdminManageCompanyViewModel vm)
	{
		InitializeComponent();
		BindingContext = _ViewModel = vm;
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _ViewModel.GetAllCompanies();
    }
    void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var company = e.SelectedItem as CompanyDto;
        if (company == null)
            return;

        _ViewModel.ItemSelected(company);
        ((ListView)sender).SelectedItem = null;
    }
}