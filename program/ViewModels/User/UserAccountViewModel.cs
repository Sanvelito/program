using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using program.Models;
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
        [ObservableProperty]
        string username;

        [ObservableProperty]
        string firstName;

        [ObservableProperty]
        string lastName;

        [ObservableProperty]
        long phoneNumber;

        [ObservableProperty]
        string password;

        private readonly IApiService _ApiService;
        public UserAccountViewModel(IApiService apiService)
        {

            // Инициализация Refit для работы с API
            _ApiService = apiService;
            
        }

        [RelayCommand]
        public async Task<bool> GetMyInfo()
        {
            try
            {
                var response = await _ApiService.GetMyInfo();

                Username = response.Username;
                FirstName = response.FirstName;
                LastName = response.LastName;
                PhoneNumber = response.PhoneNumber;

                return true;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", $"{ex}", "OK");
                Console.WriteLine(ex);
                return false;
            }
        }
        [RelayCommand]
        async Task<bool> UpdateUserInfo()
        {
            try
            {
                string numberString = PhoneNumber.ToString();
                if(numberString.Length != 12)
                {
                    await App.Current.MainPage.DisplayAlert("Упс!", $"Неверно введен номер!", "OK");
                    return false;
                }
                if (!string.IsNullOrEmpty(Password))
                {
                    if (Password.Length < 6)
                    {
                        await App.Current.MainPage.DisplayAlert("Упс!", $"Длинна пароля должна быть больше 5 символов!", "OK");
                        return false;
                    }
                }
                else
                {
                    Password = "";
                }
                
                if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName))
                {
                    await App.Current.MainPage.DisplayAlert("Упс!", "Одно из полей введено некоректно", "OK");
                    return false;
                }
                var response = await _ApiService.UpdateUserInfo(new UserInfoDto
                {
                    Username = "user",
                    FirstName = FirstName,
                    LastName = LastName,
                    PhoneNumber = PhoneNumber,
                    Password = Password,
                    Role = "user",
                    Manager = ""
                });
                await App.Current.MainPage.DisplayAlert("Готово!", "Данные обновлены!", "OK");
                return true;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", $"{ex}", "OK");
                Console.WriteLine(ex);
                return false;
            }
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
