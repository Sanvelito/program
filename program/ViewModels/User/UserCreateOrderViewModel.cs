using CommunityToolkit.Mvvm.ComponentModel;
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
    public partial class UserCreateOrderViewModel : ObservableObject
    {
        private readonly IApiService _ApiService;

        public UserCreateOrderViewModel(CompanyDto compDto)
        {
            CompanyDto = compDto;
        }
        public UserCreateOrderViewModel(IApiService apiService, CompanyDto compDto)
        {
            CompanyDto = compDto;
            // Инициализация Refit для работы с API
            _ApiService = apiService;

        }

        [ObservableProperty]
        private CompanyDto companyDto;

    }
}
