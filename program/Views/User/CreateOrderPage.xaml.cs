using AndroidX.Lifecycle;
using program.Helpers;
using program.Models;
using program.ViewModels.User;
using static Android.Preferences.PreferenceActivity;

namespace program.Views.User;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class CreateOrderPage : ContentPage
{
    UserCreateOrderViewModel _viewModel;
    public CreateOrderPage(UserCreateOrderViewModel vm)
    {
        InitializeComponent();
        BindingContext = _viewModel = vm;

    }
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        Title = $"{_viewModel.CompanyDto.CompanyName}";
        _viewModel.GetMyInfo();
        _viewModel.GetKeys();
    }
    void PickerKeySelectedIndexChanged(object sender, EventArgs e)
    {
        if(categoryPicker.SelectedItem == null)
        {
            servicePicker.SelectedItem = null;
        }
        if (categoryPicker.SelectedItem != null)
        {
            _viewModel.GetValues((categoryPicker.SelectedItem).ToString());
        }
        
    }
    
    void PickerValueSelectedIndexChanged(object sender, EventArgs e)
    {
        if(servicePicker.SelectedItem == null)
        {
            _viewModel.ServiceName = null;
        }
        if(servicePicker.SelectedItem != null)
        {
            _viewModel.ServiceName = (servicePicker.SelectedItem).ToString();
        }
        
        
    }
}