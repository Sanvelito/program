using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Devices.Sensors;
using program.Models;
using program.Services;
using program.Views.Admin;
using program.Views.Manager;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace program.ViewModels.Manager
{
    public partial class ManagerServiceViewModel : ObservableObject
    {
        [ObservableProperty]
        ServiceDto serviceDto;

        [ObservableProperty]
        ObservableCollection<ServiceDto> services;

        private readonly IApiAuthService _ApiService;
        IConnectivity connectivity;
        public ManagerServiceViewModel(IConnectivity connectivity, IApiAuthService apiService) 
        {
            this.connectivity = connectivity;
            // Инициализация Refit для работы с API
            _ApiService = apiService;
        }

        [RelayCommand]
        public async Task GetCompany()
        {
            try
            {
                string name = await SecureStorage.GetAsync("manage_company");
                List<ServiceDto> services = await _ApiService.GetServicesByCompany(name);
                Services = new ObservableCollection<ServiceDto>(services);
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
            Services = null;
            await GetCompany();
        }
        [RelayCommand]
        public async Task AddNewService()
        {
            string name = await SecureStorage.GetAsync("manage_company");
            ServiceDto = new ServiceDto { CompanyName = name, ServiceName = string.Empty, GroupName = string.Empty, Description = string.Empty, Price = 0 };
            await Shell.Current.GoToAsync($"{nameof(DetailServicePage)}",
                    new Dictionary<string, object>
                    {
                        ["ServiceDto"] = ServiceDto
                    });
        }
        public async void ItemSelected(ServiceDto serviceDto)
        {
            ServiceDto = serviceDto;
            await Shell.Current.GoToAsync($"{nameof(DetailServicePage)}",
                    new Dictionary<string, object>
                    {
                        ["ServiceDto"] = ServiceDto
                    });
        }
    }
}
