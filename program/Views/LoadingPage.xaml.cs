using program.Helpers;
using program.Models;
using program.ViewModels;
using program.Views.Admin;
using program.Views.User;

namespace program.Views;

public partial class LoadingPage : ContentPage
{

    LoadingViewModel _ViewModel;
    public LoadingPage(LoadingViewModel vm)
	{
        InitializeComponent();
        BindingContext = _ViewModel = vm;

    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        _ViewModel.Navigate();
    }

}