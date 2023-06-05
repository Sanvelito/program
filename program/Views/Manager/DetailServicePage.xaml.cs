using AndroidX.Lifecycle;
using program.ViewModels.Manager;

namespace program.Views.Manager;

public partial class DetailServicePage : ContentPage
{
	DetailServiceViewModel _ViewModel;
    public DetailServicePage(DetailServiceViewModel vm)
	{
		InitializeComponent();
		BindingContext = _ViewModel = vm;
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
        Title = $"{_ViewModel.Cheack()}";
    }
}