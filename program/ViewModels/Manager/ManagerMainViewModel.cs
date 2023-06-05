using CommunityToolkit.Mvvm.Input;
using program.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace program.ViewModels.Manager
{
    public partial class ManagerMainViewModel
    {
        public ManagerMainViewModel()
        {
            
        }

        [RelayCommand]
        async Task Logout()
        {
            if (await App.Current.MainPage.DisplayAlert("Вы уверены?", "Вы выйдите с аккаунта.", "Да", "Нет"))
            {
                SecureStorage.RemoveAll();
                await Shell.Current.GoToAsync($"///{nameof(LoginPage)}");
            }
        }
    }
}
