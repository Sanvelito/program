using program.Models;
using program.ViewModels.Admin;

namespace program.Views.Admin;

public partial class AdminManagerControlPage : ContentPage
{
	AdminManagerControlViewModel _ViewModel;
	public AdminManagerControlPage(AdminManagerControlViewModel vm)
	{
		InitializeComponent();
		BindingContext = _ViewModel = vm;
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _ViewModel.GetAllManagers();
    }
    void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var company = e.SelectedItem as UserInfoDto;
        if (company == null)
            return;

        _ViewModel.ItemSelected(company);
        ((ListView)sender).SelectedItem = null;
    }
}