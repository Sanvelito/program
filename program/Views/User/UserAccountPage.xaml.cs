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
        DisplayAlert("House Builder", "������ ��������� ���������� ���������� ��������� �������� ����������\n"
            + "�������� 4 ����� ������� ������ 44�� �������� ���������������� �������� �������� ����������. \n"
            + "���� ������ � ������� ��������� ��� ������������� �������� ����� � ����� �������. \n", "OK");
    }
}