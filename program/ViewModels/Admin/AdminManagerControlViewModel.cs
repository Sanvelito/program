using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using program.Models;
using program.Services;
using program.Views.Admin;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace program.ViewModels.Admin
{
    public partial class AdminManagerControlViewModel : ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<UserInfoDto> managers;

        [ObservableProperty]
        UserInfoDto userInfoDto;

        private readonly IApiService _ApiService;
        IConnectivity connectivity;
        public AdminManagerControlViewModel(IConnectivity connectivity, IApiService apiService)
        {
            this.connectivity = connectivity;
            // Инициализация Refit для работы с API
            _ApiService = apiService;
        }
        [RelayCommand]
        public async Task GetAllManagers()
        {
            try
            {
                List<UserInfoDto> managers = await _ApiService.GetAllManagers();
                Managers = new ObservableCollection<UserInfoDto>(managers);
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Ошибка!", $"{ex}", "OK");
                // Обработка ошибки авторизации
                Console.WriteLine(ex);
            }
        }
        [RelayCommand]
        public async Task Refresh()
        {
            Managers = null;
            await GetAllManagers();
        }
        [RelayCommand]
        public async Task AddNewManager()
        {
            UserInfoDto = new UserInfoDto { Username = string.Empty, FirstName = string.Empty, LastName = string.Empty, PhoneNumber = 0, Role = "manager", Manager = string.Empty, Password = string.Empty};
            await Shell.Current.GoToAsync($"{nameof(DetailControlManagerPage)}",
                    new Dictionary<string, object>
                    {
                        ["UserInfoDto"] = UserInfoDto
                    });
        }
        public async void ItemSelected(UserInfoDto userInfoDto)
        {
            UserInfoDto = userInfoDto;
            await Shell.Current.GoToAsync($"{nameof(DetailControlManagerPage)}",
                    new Dictionary<string, object>
                    {
                        ["UserInfoDto"] = UserInfoDto
                    });
        }
    }
}
