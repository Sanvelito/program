using program.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Refit;
using program.Services;
using System.Net.Security;
using System.Net.Http;
using CommunityToolkit.Mvvm.ComponentModel;
using program.Models;

namespace program.ViewModel
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
        public ICommand LoginCommand { get; }
        public LoginViewModel(IConnectivity connectivity)
        {
            
            this.connectivity = connectivity;

            //var handler = new HttpClientHandler();
            //handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

            //_ApiService = RestService.For<IApiService>(
            //    new HttpClient(handler)
            //    {
            //        BaseAddress = new Uri("https://10.0.2.2:7108")
            //    }
            //);
            // Инициализация Refit для работы с API
            _ApiService = RestService.For<IApiService>("http://10.0.2.2:5269");

            LoginCommand = new Command(async () => await LoginAsync());
        }

        //[ICommand]
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

                SecureStorage.SetAsync("access_token", response.accessToken);
                SecureStorage.SetAsync("refresh_token", response.refreshToken);
                await App.Current.MainPage.DisplayAlert("Alert", $"{response.status}", "OK");


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
    }
}
