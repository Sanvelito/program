using Android.Net;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.IdentityModel.Tokens;
using program.Models;
using program.Services;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace program.ViewModels
{
    public partial class RegistrationViewModel : ObservableObject
    {
        private readonly IApiAuthService _ApiService;

        [ObservableProperty]
        string password;

        [ObservableProperty]
        string username;

        [ObservableProperty]
        string firstName;

        [ObservableProperty]
        string lastName;

        [ObservableProperty]
        long phoneNumber;

        public RegistrationViewModel(IApiAuthService apiService)
        {
            // Инициализация Refit для работы с API
            _ApiService = apiService;

        }

        [RelayCommand]
        public async Task<bool> RegisterAsync()
        {
            try
            {
                if (string.IsNullOrEmpty(this.Username))
                {
                    return false;
                }
                if (string.IsNullOrEmpty(this.Password))
                {
                    return false;
                }
                if (string.IsNullOrEmpty(this.FirstName))
                {
                    return false;
                }
                if (string.IsNullOrEmpty(this.LastName))
                {
                    return false;
                }
                string numberString = PhoneNumber.ToString();
                if (numberString.Length != 12)
                {
                    await App.Current.MainPage.DisplayAlert("Упс!", $"Неверно введен номер!", "OK");
                    return false;
                }
                if(Username.Length < 5)
                {
                    await App.Current.MainPage.DisplayAlert("Упс!", $"Длинна логина должна быть больше 4 символов!", "OK");
                    return false;
                }
                if (Password.Length < 6)
                {
                    await App.Current.MainPage.DisplayAlert("Упс!", $"Длинна пароля должна быть больше 5 символов!", "OK");
                    return false;
                }

                // Отправка запроса на сервер
                RegisterDto response = await _ApiService.Register(new RegisterDto
                {
                    Username = this.Username,
                    Password = this.Password,
                    FirstName = this.FirstName,
                    LastName = this.LastName,
                    PhoneNumber = this.PhoneNumber
                });
                await App.Current.MainPage.DisplayAlert("Alert", $"success", "OK");
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
