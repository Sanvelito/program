using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using program.Models;
using program.Views.User;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Android.Icu.Text.CaseMap;

namespace program.ViewModels.User
{
    public partial class UserCatalogDetailViewModel : ObservableObject
    {
        public UserCatalogDetailViewModel(CompanyDto compDto)
        {
            CompanyDto = compDto;
            Title = $"{companyDto.CompanyName}";
        }

        [ObservableProperty]
        private CompanyDto companyDto;

        [ObservableProperty]
        private string title;

        [RelayCommand]
        public async Task CreateOrder()
        {
            
        }
    }
}
