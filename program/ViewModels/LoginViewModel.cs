using program.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Refit;
using program.Services;
using System.Net.Security;
using System.Net.Http;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using program.Views;
using program.Views.User;

namespace program.ViewModels
{
    //[INotifyPropertyChanged]
    public partial class LoginViewModel : ObservableObject
    {
        private readonly IApiService _ApiService;

        [ObservableProperty]
        string password;

        [ObservableProperty]
        string username;

        IConnectivity connectivity;
        public LoginViewModel(IConnectivity connectivity)
        {
            
            this.connectivity = connectivity;

            // Инициализация Refit для работы с API
            _ApiService = RestService.For<IApiService>("http://10.0.2.2:5269");

        }

        [RelayCommand]
        public async Task<bool> LoginAsync()
        {
            try
            {
                // Отправка запроса на сервер для получения токена
                LoginDto response = await _ApiService.Login(new UserDto
                {
                    Username = this.Username,
                    Password = this.Password
                });

                // Сохранение токена в безопасном хранилище
                await SecureStorage.SetAsync("access_token", response.accessToken);
                await SecureStorage.SetAsync("refresh_token", response.refreshToken);
                await SecureStorage.SetAsync("hasAuth", "true");


                await App.Current.MainPage.DisplayAlert("Alert", $"{response.status}", "OK");
                if(response.role == "admin")
                {
                    await Shell.Current.GoToAsync($"///{nameof(AdminPage)}");

                }
                if (response.role == "user")
                {
                    await Shell.Current.GoToAsync($"///{nameof(MainPage)}");
                }

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
        Task Navigate() => Shell.Current.GoToAsync(nameof(RegistrationPage));

    }
}
