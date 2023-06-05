using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using program.Models;
using program.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace program.ViewModels.Admin
{
    [QueryProperty(nameof(UserInfoDto), nameof(UserInfoDto))]
    public partial class DetailControlManagerViewModel : ObservableObject
    {
        [ObservableProperty]
        UserInfoDto userInfoDto;

        [ObservableProperty]
        ObservableCollection<CompanyDto> companies;

        [ObservableProperty]
        CompanyDto companyDto;

        [ObservableProperty]
        string title;

        //button switch
        [ObservableProperty]
        bool addButtonVisible;
        [ObservableProperty]
        bool updateButtonVisible;
        [ObservableProperty]
        bool deleteButtonVisible;

        [ObservableProperty]
        bool entryIsEnabled;

        [ObservableProperty]
        bool passwordEntryVisable;
        //UserInfoDto data
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
        [ObservableProperty]
        string role;
        [ObservableProperty]
        string manager;

        //picker
        [ObservableProperty]
        List<string> keys;

        IApiAuthService _apiAuthService;
        IApiService _apiService;
        IConnectivity connectivity;
        public DetailControlManagerViewModel(IConnectivity connectivity, IApiService apiService, IApiAuthService apiAuthService)
        {
            this.connectivity = connectivity;
            _apiService = apiService;
            _apiAuthService = apiAuthService;
        }
        [RelayCommand]
        public async Task<bool> UpdateManager()
        {
            try
            {
                string numberString = PhoneNumber.ToString();
                if (numberString.Length != 12)
                {
                    await App.Current.MainPage.DisplayAlert("Упс!", $"Неверно введен номер!", "OK");
                    return false;
                }
                if (string.IsNullOrEmpty(Manager))
                {
                    await App.Current.MainPage.DisplayAlert("Упс!", $"Не выбрана компания!", "OK");
                    return false;
                }
                if (FirstName.Length <= 2 && LastName.Length <= 2)
                {
                    await App.Current.MainPage.DisplayAlert("Упс!", $"Некоректные данные!", "OK");
                    return false;
                }
                var info = await _apiService.UpdateManager(new UserInfoDto
                {
                    Username = UserInfoDto.Username,
                    FirstName = FirstName,
                    LastName = LastName,
                    PhoneNumber = PhoneNumber,
                    Manager = Manager

                });
                await Shell.Current.GoToAsync("..");
                await App.Current.MainPage.DisplayAlert("Alert", $"{info}", "OK");
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
        public async Task<bool> AddNewManager()
        {
            try
            {
                string numberString = PhoneNumber.ToString();
                if (numberString.Length != 12)
                {
                    await App.Current.MainPage.DisplayAlert("Упс!", $"Неверно введен номер!", "OK");
                    return false;
                }
                if (string.IsNullOrEmpty(Manager))
                {
                    await App.Current.MainPage.DisplayAlert("Упс!", $"Не выбрана компания!", "OK");
                    return false;
                }
                if (string.IsNullOrEmpty(Password) && Password.Length < 5) 
                {
                    await App.Current.MainPage.DisplayAlert("Упс!", $"Некоректный пароль!", "OK");
                    return false;
                }
                if (FirstName.Length <= 2 && LastName.Length <= 2)
                {
                    await App.Current.MainPage.DisplayAlert("Упс!", $"Некоректные данные!", "OK");
                    return false;
                }
                var info = await _apiService.AddNewManager(new UserInfoDto
                {
                    Username = Username,
                    FirstName = FirstName,
                    Password = Password,
                    LastName = LastName,
                    PhoneNumber = PhoneNumber,
                    Manager = Manager
                });
                await Shell.Current.GoToAsync("..");
                await App.Current.MainPage.DisplayAlert("Alert", $"{info}", "OK");
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
        public async Task<bool> DeleteManager()
        {
            try
            {
                var info = await _apiService.DeleteManager(new UserInfoDto { Username = UserInfoDto.Username});
                await Shell.Current.GoToAsync("..");
                await App.Current.MainPage.DisplayAlert("Alert", $"{info}", "OK");
                return true;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", $"{ex}", "OK");
                Console.WriteLine(ex);
                return false;
            }
        }
        public string Cheack()
        {
            if (UserInfoDto.Username == string.Empty)
            {
                Username = UserInfoDto.Username;
                FirstName = UserInfoDto.FirstName;
                LastName = UserInfoDto.LastName;
                PhoneNumber = UserInfoDto.PhoneNumber;
                Password = UserInfoDto.Password;
                Role = "Manager";
                Manager = UserInfoDto.Manager;

                EntryIsEnabled = true;
                PasswordEntryVisable = true;
                AddButtonVisible = true;
                UpdateButtonVisible = false;
                DeleteButtonVisible = false;

                return "new manager";
            }
            else
            {
                Username = UserInfoDto.Username;
                FirstName = UserInfoDto.FirstName;
                LastName = UserInfoDto.LastName;
                PhoneNumber = UserInfoDto.PhoneNumber;
                Role = "Manager";
                Manager = UserInfoDto.Manager;

                EntryIsEnabled = false;
                PasswordEntryVisable = false;
                AddButtonVisible = false;
                UpdateButtonVisible = true;
                DeleteButtonVisible = true;

                return UserInfoDto.Username;
            }
        }
        [RelayCommand]
        public async Task GetAllCompanies()
        {
            try
            {
                List<CompanyDto> comps = await _apiAuthService.GetAllCompanies();

                Keys = comps.Select(companyDto => companyDto.CompanyName).ToList();

            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", $"{ex}", "OK");
                // Обработка ошибки авторизации
                Console.WriteLine(ex);
            }
        }
    }
}
