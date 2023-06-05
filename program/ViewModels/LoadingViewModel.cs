using program.Models;
using program.Services;
using program.Views;
using program.Views.Admin;
using program.Views.Manager;
using program.Views.User;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace program.ViewModels
{
    public partial class LoadingViewModel
    {
        private readonly IApiAuthService _ApiService;
        public LoadingViewModel(IApiAuthService apiService)
        {
            _ApiService = apiService;
        }

        public async Task<LoginDto> IsAuthenticatedAsync()
        {
            //await Task.Delay(2000);

            string refreshTokenInSecure = await SecureStorage.GetAsync("refresh_token");
            try
            {
                if (string.IsNullOrEmpty(refreshTokenInSecure)) 
                { 
                    return new LoginDto { role = "notAuth" }; 
                }

                LoginDto response = await _ApiService.CheckAuth(refreshTokenInSecure);

                await SecureStorage.SetAsync("access_token", response.accessToken);
                await SecureStorage.SetAsync("refresh_token", response.refreshToken);
                await SecureStorage.SetAsync("manage_company", response.status);
                return response;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", $"{ex}", "OK");
                // Обработка ошибки авторизации
                Console.WriteLine(ex);
                return new LoginDto { status = "notAuth" };
            }
            //return false;
        }
        public async Task Navigate()
        {
            LoginDto loginDto = await IsAuthenticatedAsync();

            if (loginDto.role == "notAuth")
            {
                await Shell.Current.GoToAsync(nameof(LoginPage));
            }
            else if (loginDto.role == "admin")
            {
                await Shell.Current.GoToAsync($"///{nameof(AdminMainPage)}");
            }
            else if (loginDto.role == "user")
            {
                await Shell.Current.GoToAsync($"///{nameof(MainPage)}");
            }
            else if (loginDto.role == "manager")
            {
                await Shell.Current.GoToAsync($"///{nameof(ManagerMainPage)}");
            }
        }
    }
}
