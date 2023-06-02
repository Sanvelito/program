using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using program.Models;
using program.Services;
using program.Views.Admin;
using program.Views.User;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace program.ViewModels.Admin
{
    public partial class AdminManageCompanyViewModel : ObservableObject
    {
        private readonly IApiService _ApiService;


        [ObservableProperty]
        ObservableCollection<CompanyDto> companies;

        [ObservableProperty]
        CompanyDto companyDto;

        IConnectivity connectivity;
        public AdminManageCompanyViewModel(IConnectivity connectivity, IApiService apiService)
        {
            this.connectivity = connectivity;
            // Инициализация Refit для работы с API
            _ApiService = apiService;
            //GetAllCompanies();
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
        [RelayCommand]
        public async Task Refresh()
        {
            Companies = null;
            await GetAllCompanies();
        }
        [RelayCommand]
        public async Task AddNewCompany()
        {
            CompanyDto = new CompanyDto { CompanyEmail = string.Empty, CompanyName = string.Empty, CompanyPhoneNumber = 0, CompanyImage = new byte[0] };
            await Shell.Current.GoToAsync($"{nameof(DetailManageCompanyPage)}",
                    new Dictionary<string, object>
                    {
                        ["CompanyDto"] = CompanyDto
                    });
        }
        public async void ItemSelected(CompanyDto companyDto)
        {
            CompanyDto = companyDto;
            await Shell.Current.GoToAsync($"{nameof(DetailManageCompanyPage)}",
                    new Dictionary<string, object>
                    {
                        ["CompanyDto"] = CompanyDto
                    });
        }
    }
}
