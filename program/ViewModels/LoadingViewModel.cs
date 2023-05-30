using program.Models;
using program.Services;
using program.Views;
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
        
        public async Task<LoginDto> isAuthenticatedAsync()
        {
            //await Task.Delay(2000);

            string refreshTokenInSecure = await SecureStorage.GetAsync("refresh_token");
            try
            {
                if (string.IsNullOrEmpty(refreshTokenInSecure)) 
                { 
                    return new LoginDto { status = "notAuth" }; 
                }

                LoginDto response = await _ApiService.CheckAuth(refreshTokenInSecure);

                await SecureStorage.SetAsync("access_token", response.accessToken);
                await SecureStorage.SetAsync("refresh_token", response.refreshToken);

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
    }
}
