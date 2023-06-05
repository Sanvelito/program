using program.ViewModels.Admin;

namespace program.Views.Admin;

public partial class DetailControlManagerPage : ContentPage
{
    DetailControlManagerViewModel _viewModel;
    public DetailControlManagerPage(DetailControlManagerViewModel vm)
	{
		InitializeComponent();
        BindingContext = _viewModel = vm;
    }
    //protected override void OnAppearing()
    //{
    //    base.OnAppearing();
    //    Title = $"{_viewModel.Cheack()}";
    //    _viewModel.GetAllCompanies();
    //}
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        Title = $"{_viewModel.Cheack()}";
        _viewModel.GetAllCompanies();
    }
    void PickerKeySelectedIndexChanged(object sender, EventArgs e)
    {
        if (companyPicker.SelectedItem == null)
        {
            _viewModel.Manager = null;
        }
        if (companyPicker.SelectedItem != null)
        {
            _viewModel.Manager = (companyPicker.SelectedItem).ToString();
        }
    }
}