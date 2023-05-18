using CommunityToolkit.Mvvm.ComponentModel;
using program.Models;
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
            Title = $"{companyDto.CompanyName} Details";
        }

        [ObservableProperty]
        private CompanyDto companyDto;

        [ObservableProperty]
        private string title;
    }
}
