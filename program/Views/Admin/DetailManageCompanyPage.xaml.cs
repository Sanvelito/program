using AndroidX.Lifecycle;
using Microsoft.Maui.Networking;
using program.ViewModels.Admin;

namespace program.Views.Admin;

public partial class DetailManageCompanyPage : ContentPage
{
    DetailManageCompanyViewModel _viewModel;
    public DetailManageCompanyPage(DetailManageCompanyViewModel vm)
	{
		InitializeComponent();
		BindingContext = _viewModel = vm;
	}
    //protected override void OnNavigatedTo(NavigatedToEventArgs args)
    //{
    //    base.OnNavigatedTo(args);
    //    Title = $"{_viewModel.Cheack()}";
    //}
    protected override void OnAppearing()
    {
        base.OnAppearing();
        Title = $"{_viewModel.Cheack()}";
    }
}