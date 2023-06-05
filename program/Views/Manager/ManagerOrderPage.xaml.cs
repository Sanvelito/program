using AndroidX.Lifecycle;
using program.Models;
using program.ViewModels.Manager;

namespace program.Views.Manager;

public partial class ManagerOrderPage : ContentPage
{
    ManagerOrderViewModel _viewModel;
    public ManagerOrderPage(ManagerOrderViewModel vm)
	{
		InitializeComponent();
		BindingContext = _viewModel = vm;
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.GetMyOrders();
    }
    void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var service = e.SelectedItem as CustomerServiceDto;
        if (service == null)
            return;

        _viewModel.ItemSelected(service);
    }
    void Clear(object sender, EventArgs args)
    {
        listview.SelectedItem = null;
    }
}