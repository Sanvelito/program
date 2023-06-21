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

namespace program.ViewModels.User
{
    public partial class UserChatViewModel : ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<CustomerServiceDto> services;

        private readonly IApiService _ApiService;
        public UserChatViewModel(IApiService apiService)
        {

            // Инициализация Refit для работы с API
            _ApiService = apiService;
        }
        [RelayCommand]
        public async Task<bool> GetMyOrders()
        {
            try
            {
                List<CustomerServiceDto> ServicesDto = await _ApiService.GetUserOrders();
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
    }
}
