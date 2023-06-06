using program.Helpers;
using program.ViewModels.User;

namespace program.Views.User;

public partial class UserAccountPage : ContentPage
{
    UserAccountViewModel _ViewModel;
    public UserAccountPage(UserAccountViewModel vm)
	{
		InitializeComponent();
        BindingContext = _ViewModel = vm;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _ViewModel.GetMyInfo();
    }

    private void GetInfoAbout(object sender, EventArgs e)
    {
        DisplayAlert("House Builder", "Данное мобильное приложение разработал Гапанович Геннадий Эдуардович\n"
            + "Учащийся 4 курса учебной группы 44ТП Минского Государственного Колледжа Цифровых Технологий. \n"
            + "Цель работы – создать программу для автоматизации оказания услуг в сфере ремонта. \n", "OK");
    }
}