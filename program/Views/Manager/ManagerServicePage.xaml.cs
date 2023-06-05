using program.Models;
using program.ViewModels.Manager;

namespace program.Views.Manager;

public partial class ManagerServicePage : ContentPage
{
    ManagerServiceViewModel _ViewModel;
    public ManagerServicePage(ManagerServiceViewModel vm)
	{
		InitializeComponent();
        BindingContext = _ViewModel = vm;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _ViewModel.GetCompany();
    }
    void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var service = e.SelectedItem as ServiceDto;
        if (service == null)
            return;

        _ViewModel.ItemSelected(service);
        ((ListView)sender).SelectedItem = null;
    }
}