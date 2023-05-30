using program.Helpers;
using program.Models;
using program.ViewModels.User;

namespace program.Views.User;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class UserCatalogDetailPage : ContentPage
{
    UserCatalogDetailViewModel _viewModel;
    public UserCatalogDetailPage(UserCatalogDetailViewModel vm)
    {
        InitializeComponent();
        BindingContext = _viewModel = vm;
        
    }
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        Title = $"{_viewModel.CompanyDto.CompanyName}";
    }
}