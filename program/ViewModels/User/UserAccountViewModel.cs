using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using program.Services;
using program.Views;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace program.ViewModels.User
{
    public partial class UserAccountViewModel : ObservableObject
    {
        private readonly IApiService _ApiService;
        public UserAccountViewModel(IApiService apiService)
        {

            // Инициализация Refit для работы с API
            _ApiService = apiService;

        }

        [RelayCommand]
        public async Task<bool> LoginAsync()
        {
            try
            {
                //LoginDto response = await _ApiService.Login(new UserDto
                //{
                //    Username = this.Username,
                //    Password = this.Password
                //});

                return true;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", $"{ex}", "OK");
                // Обработка ошибки авторизации
                Console.WriteLine(ex);
                return false;
            }
        }
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
