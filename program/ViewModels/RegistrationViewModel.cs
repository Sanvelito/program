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
        private readonly IApiService _ApiService;

        [ObservableProperty]
        string password;

        [ObservableProperty]
        string username;

        [ObservableProperty]
        string firstName;

        [ObservableProperty]
        string lastName;

        [ObservableProperty]
        int phoneNumber;

        public RegistrationViewModel(IApiService apiService)
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
                if (this.PhoneNumber == 0)
                {
                    return false;
                }
                // Отправка запроса на сервер
                RegisterDto response = await _ApiService.Register(new RegisterDto
                {
                    Username = this.Username,
                    Password = this.Password,
                    FirstName = this.FirstName,
                    LastName = this.lastName,
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
