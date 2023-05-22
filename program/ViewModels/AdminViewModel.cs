using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using program.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace program.ViewModels
{
    public partial class AdminViewModel : ObservableObject
    {
        public AdminViewModel()
        {
        }

        [RelayCommand]
        Task Back() => Shell.Current.GoToAsync("..");
        [RelayCommand]
        Task GoToUser() => Shell.Current.GoToAsync($"../{nameof(UserPage)}");

        [RelayCommand]
        async Task Logout()
        {
            if (await App.Current.MainPage.DisplayAlert("Are you sure?", "You will be logged out.", "Yes", "No"))
            {
                SecureStorage.RemoveAll();
                await Shell.Current.GoToAsync($"///{nameof(LoginPage)}");
            }
        }
    }
}
