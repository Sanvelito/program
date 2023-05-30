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
    [QueryProperty(nameof(CompanyDto), nameof(CompanyDto))]
    public partial class UserCatalogDetailViewModel : ObservableObject
    {
        [ObservableProperty]
        CompanyDto companyDto;

        [ObservableProperty]
        string title;

        IConnectivity connectivity;
        public UserCatalogDetailViewModel(IConnectivity connectivity)
        {
            this.connectivity = connectivity;
        }

        [RelayCommand]
        public async Task CreateOrder()
        {
            await Shell.Current.GoToAsync($"{nameof(CreateOrderPage)}",
                    new Dictionary<string, object>
                    {
                        ["CompanyDto"] = CompanyDto
                    });
        }
    }
}
