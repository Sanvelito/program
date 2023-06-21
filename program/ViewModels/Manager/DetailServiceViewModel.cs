using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using program.Models;
using program.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Android.Util.EventLogTags;

namespace program.ViewModels.Manager
{
    [QueryProperty(nameof(ServiceDto), nameof(ServiceDto))]
    public partial class DetailServiceViewModel : ObservableObject
    {
        [ObservableProperty]
        ServiceDto serviceDto;

        [ObservableProperty]
        string title;

        //button switch
        [ObservableProperty]
        bool addButtonVisible;
        [ObservableProperty]
        bool updateButtonVisible;
        [ObservableProperty]
        bool deleteButtonVisible;

        //ServiceDto data
        [ObservableProperty]
        string companyName;
        [ObservableProperty]
        string groupName;
        [ObservableProperty]
        string serviceName;
        [ObservableProperty]
        string description;
        [ObservableProperty]
        double price;

        IApiAuthService _apiService;
        IConnectivity connectivity;
        public DetailServiceViewModel(IConnectivity connectivity, IApiAuthService apiService)
        {
            this.connectivity = connectivity;
            _apiService = apiService;
        }
        [RelayCommand]
        public async Task<bool> UpdateService()
        {
            try
            {
                if (string.IsNullOrEmpty(CompanyName) || string.IsNullOrEmpty(GroupName) || string.IsNullOrEmpty(ServiceName) || string.IsNullOrEmpty(Description) || Price < 0)
                {
                    await App.Current.MainPage.DisplayAlert("Упс!", $"{CompanyName} {GroupName} {ServiceName} {Description} {Price}", "OK");
                    await App.Current.MainPage.DisplayAlert("Упс!", $"Неверно введены данные!", "OK");
                    return false;
                }
                var newCompanyInfo = await _apiService.UpdateService(new ServiceDto
                {
                    CompanyName = CompanyName,
                    GroupName = GroupName,
                    ServiceName = ServiceName,
                    Description = Description,
                    Price = Price
                });
                await App.Current.MainPage.DisplayAlert("Внимание!", $"{newCompanyInfo}", "OK");
                return true;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Ошибка!", $"{ex}", "OK");
                Console.WriteLine(ex);
                return false;
            }
        }
        [RelayCommand]
        public async Task<bool> AddNewService()
        {
            try
            {
                if (string.IsNullOrEmpty(CompanyName) || string.IsNullOrEmpty(GroupName) || string.IsNullOrEmpty(ServiceName) || string.IsNullOrEmpty(Description) || Price < 0)
                {
                    await App.Current.MainPage.DisplayAlert("Упс!", $"Неверно введены данные!", "OK");
                    return false;
                }
                var info = await _apiService.AddNewService(new ServiceDto
                {
                    CompanyName = CompanyName,
                    GroupName = GroupName,
                    ServiceName = ServiceName,
                    Description = Description,
                    Price = Price
                });
                if(info == string.Empty)
                {
                    await App.Current.MainPage.DisplayAlert("Упс!", $"Категория не найдена", "OK");
                    return true;
                }
                else
                {
                    await Shell.Current.GoToAsync("..");
                    await App.Current.MainPage.DisplayAlert("Готово!", $"Новая услуга добавлена!", "OK");
                }
                return true;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Ошибка!", $"{ex}", "OK");
                Console.WriteLine(ex);
                return false;
            }
        }
        [RelayCommand]
        public async Task<bool> DeleteService()
        {
            try
            {
                var info = await _apiService.DeleteService(CompanyName, ServiceName);
                await Shell.Current.GoToAsync("..");
                await App.Current.MainPage.DisplayAlert("Внимание!", $"{info}", "OK");
                return true;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Ошибка!", $"{ex}", "OK");
                Console.WriteLine(ex);
                return false;
            }
        }
        public string Cheack()
        {
            if (ServiceDto.ServiceName == string.Empty)
            {

                CompanyName = ServiceDto.CompanyName;
                GroupName = ServiceDto.GroupName;
                ServiceName = ServiceDto.ServiceName;
                Description = ServiceDto.Description;
                Price = ServiceDto.Price;

                AddButtonVisible = true;
                UpdateButtonVisible = false;
                DeleteButtonVisible = false;

                return "Новая услуга";
            }
            else
            {
                CompanyName = ServiceDto.CompanyName;
                GroupName = ServiceDto.GroupName;
                ServiceName = ServiceDto.ServiceName;
                Description = ServiceDto.Description;
                Price = ServiceDto.Price;

                AddButtonVisible = false;
                UpdateButtonVisible = true;
                DeleteButtonVisible = true;

                return ServiceDto.ServiceName;
            }

        }
    }
}
