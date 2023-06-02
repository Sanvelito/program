using program.Helpers;
using program.Models;
using program.ViewModels;
using program.Views.Admin;
using program.Views.User;

namespace program.Views;

public partial class LoadingPage : ContentPage
{

    //LoadingViewModel loadingViewModel;
    public LoadingPage()
	{
        InitializeComponent();
        BindingContext = ServiceHelper.GetService<LoadingViewModel>();

    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        LoginDto loginDto = await ServiceHelper.GetService<LoadingViewModel>().isAuthenticatedAsync();
        if (loginDto.status == "notAuth")
        {
            await Shell.Current.GoToAsync(nameof(LoginPage));
        }
        else if (loginDto.role == "admin")
        {
            await Shell.Current.GoToAsync($"///{nameof(AdminMainPage)}");
        }
        else if (loginDto.role == "user")
        {
            await Shell.Current.GoToAsync($"///{nameof(MainPage)}");
        }
    }

}