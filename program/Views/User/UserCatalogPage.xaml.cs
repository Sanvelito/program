using program.Helpers;
using program.Models;
using program.Services;
using program.ViewModels;
using program.ViewModels.User;

namespace program.Views.User;

public partial class UserCatalogPage : ContentPage
{
    private readonly UserCatalogViewModel _ViewModel;
    public UserCatalogPage(UserCatalogViewModel vm)
	{
		InitializeComponent();
        BindingContext = _ViewModel =vm;
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