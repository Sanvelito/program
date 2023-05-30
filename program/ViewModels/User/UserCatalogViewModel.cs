using Android.Net;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using program.Models;
using program.Services;
using program.Views.User;
using Refit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace program.ViewModels.User
{
    public partial class UserCatalogViewModel : ObservableObject
    {
        private readonly IApiService _ApiService;
        

        [ObservableProperty]
        ObservableCollection<CompanyDto> companies;

        [ObservableProperty]
        CompanyDto companyDto;

        IConnectivity connectivity;
        public UserCatalogViewModel(IConnectivity connectivity, IApiService apiService)
        {
            this.connectivity = connectivity;
            // Инициализация Refit для работы с API
            _ApiService = apiService;
            GetAllCompanies();
        }

        [RelayCommand]
        public async Task GetAllCompanies()
        {
            try
            {
                List<CompanyDto> comps = await _ApiService.GetAllCompanies();  
                Companies = new ObservableCollection<CompanyDto>(comps);
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", $"{ex}", "OK");
                // Обработка ошибки авторизации
                Console.WriteLine(ex);
            }
        }
        public async void ItemSelected(CompanyDto companyDto)
        {
            CompanyDto = companyDto;
            await Shell.Current.GoToAsync($"{nameof(UserCatalogDetailPage)}",
                    new Dictionary<string, object>
                    {
                        ["CompanyDto"] = CompanyDto
                    });
        }

    // Дополнительные действия, если необходимо
    // Сбросить выбор элемента в списке
    //((ListView)sender).SelectedItem = null;

}
}
