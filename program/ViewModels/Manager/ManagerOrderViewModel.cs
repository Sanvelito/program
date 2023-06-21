using Android.Locations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using program.Models;
using program.Services;
using program.Views;
using program.Views.Manager;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Android.Util.EventLogTags;

namespace program.ViewModels.Manager
{
    public partial class ManagerOrderViewModel : ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<CustomerServiceDto> services;

        [ObservableProperty]
        CustomerServiceDto customerServiceDto;
        private readonly IApiService _ApiService;
        public ManagerOrderViewModel(IApiService apiService)
        {

            // Инициализация Refit для работы с API
            _ApiService = apiService;
        }
        [RelayCommand]
        public async Task<bool> GetMyOrders()
        {
            try
            {
                string name = await SecureStorage.GetAsync("manage_company");

                List<CustomerServiceDto> ServicesDto = await _ApiService.GetCompanyOrders(name);
                Services = new ObservableCollection<CustomerServiceDto>(ServicesDto);

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
        public async Task<bool> AcceptOrder()
        {
            try
            {

                string info = await _ApiService.UpdateOrder(new CustomerServiceDto
                {
                    Id = CustomerServiceDto.Id,
                    Username = CustomerServiceDto.Username,
                    FirstName = CustomerServiceDto.FirstName,
                    LastName = CustomerServiceDto.LastName,
                    PhoneNumber = CustomerServiceDto.PhoneNumber,
                    CompanyName = CustomerServiceDto.CompanyName,
                    CreatedDate = CustomerServiceDto.CreatedDate,
                    ServiceName = CustomerServiceDto.ServiceName,
                    DeadLine = CustomerServiceDto.DeadLine,
                    Description = CustomerServiceDto.Description,
                    Address = CustomerServiceDto.Address,
                    Status = "Принят в работу",
                });
        
    
                await App.Current.MainPage.DisplayAlert("Готово!", $"Заказ принят в работу!", "OK");
                RefreshSelected();
                Refresh();
                return true;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Ошибка!", $"Выберете заказ!", "OK");
                Console.WriteLine(ex);
                return false;
            }
        }
        [RelayCommand]
        public async Task<bool> DeclineOrder()
        {
            try
            {

                string info = await _ApiService.UpdateOrder(new CustomerServiceDto
                {
                    Id = CustomerServiceDto.Id,
                    Username = CustomerServiceDto.Username,
                    FirstName = CustomerServiceDto.FirstName,
                    LastName = CustomerServiceDto.LastName,
                    PhoneNumber = CustomerServiceDto.PhoneNumber,
                    CompanyName = CustomerServiceDto.CompanyName,
                    CreatedDate = CustomerServiceDto.CreatedDate,
                    ServiceName = CustomerServiceDto.ServiceName,
                    DeadLine = CustomerServiceDto.DeadLine,
                    Description = CustomerServiceDto.Description,
                    Address = CustomerServiceDto.Address,
                    Status = "Отклонен исполнителем",
                });


                await App.Current.MainPage.DisplayAlert("Готово!", $"Заказ отклонен!", "OK");
                RefreshSelected();
                Refresh();
                return true;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Ошибка!", $"Выберете заказ!", "OK");
                Console.WriteLine(ex);
                return false;
            }
        }
        [RelayCommand]
        public async Task<bool> CompliteOrder()
        {
            try
            {

                
                string info = await _ApiService.UpdateOrder(new CustomerServiceDto
                {
                    Id = CustomerServiceDto.Id,
                    Username = CustomerServiceDto.Username,
                    FirstName = CustomerServiceDto.FirstName,
                    LastName = CustomerServiceDto.LastName,
                    PhoneNumber = CustomerServiceDto.PhoneNumber,
                    CompanyName = CustomerServiceDto.CompanyName,
                    CreatedDate = CustomerServiceDto.CreatedDate,
                    ServiceName = CustomerServiceDto.ServiceName,
                    DeadLine = CustomerServiceDto.DeadLine,
                    Description = CustomerServiceDto.Description,
                    Address = CustomerServiceDto.Address,
                    Status = "Выполнен",
                });


                await App.Current.MainPage.DisplayAlert("Готово!", $"Заказ помечен выполненым!", "OK");
                RefreshSelected();
                Refresh();
                return true;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Ошибка!", $"Выберете заказ!", "OK");
                Console.WriteLine(ex);
                return false;
            }
        }
        public void ItemSelected(CustomerServiceDto customerServiceDto)
        {
            CustomerServiceDto = null;
            CustomerServiceDto = customerServiceDto;
        }
        public void RefreshSelected()
        {
            CustomerServiceDto = null;
        }
        public async Task Refresh()
        {
            Services = null;
            await GetMyOrders();
        }
    }
}
