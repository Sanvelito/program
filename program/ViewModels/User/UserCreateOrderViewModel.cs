using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Graphics.Text;
using program.Models;
using program.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace program.ViewModels.User
{
    [QueryProperty(nameof(CompanyDto), nameof(CompanyDto))]
    public partial class UserCreateOrderViewModel : ObservableObject
    {
        [ObservableProperty]
        CompanyDto companyDto;

        [ObservableProperty]
        string title;

        [ObservableProperty]
        string username;

        [ObservableProperty]
        string firstName;

        [ObservableProperty]
        string lastName;

        [ObservableProperty]
        long phoneNumber;

        [ObservableProperty]
        DateTime selectedDate;

        [ObservableProperty]
        TimeSpan selectedTime;

        [ObservableProperty]
        string description;

        [ObservableProperty]
        string address;

        [ObservableProperty]
        string status;

        [ObservableProperty]
        string serviceName;

        //picker
        [ObservableProperty]
        List<string> keys;

        [ObservableProperty]
        List<string> values;

        [ObservableProperty]
        public DateTime deadLine;

        public DateTime SelectedDateTime
        {
            get { return new DateTime(SelectedDate.Year, SelectedDate.Month, SelectedDate.Day, SelectedTime.Hours, SelectedTime.Minutes, SelectedTime.Seconds); }
        }

        private readonly IApiService _ApiService;

        IConnectivity connectivity;

        public UserCreateOrderViewModel(IConnectivity connectivity, IApiService apiService)
        {
            this.connectivity = connectivity;
            // Инициализация Refit для работы с API
            _ApiService = apiService;
            DeadLine = DateTime.Now;
        }

        public void GetKeys()
        {
            Keys = CompanyDto.ServicesGroup.Keys.ToList();
        }
        public void GetValues(string key)
        {
            if (CompanyDto.ServicesGroup.TryGetValue(key, out var serviceList))
            {
                Values = serviceList.Select(item => item.Name).ToList();
            }
        }
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
        public async Task<bool> CreateOrder()
        {
            try
            {
                if (SelectedDateTime <= DateTime.Now || Address == null || ServiceName == null)
                {
                    await App.Current.MainPage.DisplayAlert("Упс!", "Введены некоректные данные", "OK");
                    return false;
                }
                if(string.IsNullOrEmpty(Description))
                {
                    Description = string.Empty;
                }
                CustomerServiceDto response = await _ApiService.AddCustomerService(new CustomerServiceDto
                {
                    Username = Username,
                    FirstName = FirstName,
                    LastName = LastName,
                    PhoneNumber = PhoneNumber,
                    CompanyName = CompanyDto.CompanyName,
                    ServiceName = ServiceName,
                    DeadLine = SelectedDateTime,
                    Description = Description,
                    Address = Address,
                    Status = "Ожидание ответа исполнителя"
                });
                await Shell.Current.GoToAsync("../..");
                await App.Current.MainPage.DisplayAlert("Yes!", "Заказ составлен", "OK");
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
