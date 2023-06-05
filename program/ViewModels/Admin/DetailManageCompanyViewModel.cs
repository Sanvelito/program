using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using program.Models;
using program.Services;
using program.Views.Admin;
using program.Views.User;
using program.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using program.Helpers;
using System.Text.RegularExpressions;
using CommunityToolkit.Maui.Converters;

namespace program.ViewModels.Admin
{
    [QueryProperty(nameof(CompanyDto), nameof(CompanyDto))]
    public partial class DetailManageCompanyViewModel : ObservableObject
    {
        [ObservableProperty]
        CompanyDto companyDto;

        [ObservableProperty]
        string title;

        //button switch
        [ObservableProperty]
        bool addButtonVisible;
        [ObservableProperty]
        bool updateButtonVisible;
        [ObservableProperty]
        bool deleteButtonVisible;
        
        //CompanyDto data
        [ObservableProperty]
        string companyName;
        [ObservableProperty]
        string companyEmail;
        [ObservableProperty]
        long companyPhoneNumber;
        [ObservableProperty]
        byte[] newImage;

        IApiAuthService _apiService;
        IConnectivity connectivity;
        public DetailManageCompanyViewModel(IConnectivity connectivity, IApiAuthService apiService) 
        {
            this.connectivity = connectivity;
            _apiService = apiService;
            
        }

        [RelayCommand]
        public async Task<bool> SelectImage()
        {
            FileResult photo = await MediaPicker.PickPhotoAsync();

            if (photo == null)
                return false;
            byte[] imageData;
            string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            using Stream stream = await photo.OpenReadAsync();
            using (FileStream localFileStream = File.OpenWrite(localFilePath))
            {

                await stream.CopyToAsync(localFileStream);
            }
            imageData = File.ReadAllBytes(localFilePath);

            ImageResizer ir = new ImageResizer();

            byte[] resizedImage = await ir.ResizeImage(imageData, 100, 100);

            NewImage = resizedImage;
            
            return true;
        }

        [RelayCommand]
        public async Task<bool> UpdateCompany()
        {
            try
            {
                string numberString = CompanyPhoneNumber.ToString();
                if (numberString.Length != 12)
                {
                    await App.Current.MainPage.DisplayAlert("Упс!", $"Неверно введен номер!", "OK");
                    return false;
                }
                if (IsValidEmail(CompanyEmail) == false)
                {
                    await App.Current.MainPage.DisplayAlert("Упс!", $"Неверно введен email!", "OK");
                    return false;
                }
                if (string.IsNullOrEmpty(CompanyName) || string.IsNullOrEmpty(CompanyEmail))
                {
                    await App.Current.MainPage.DisplayAlert("Упс!", $"Неверно введены данные!", "OK");
                    return false;
                }
                var newCompanyInfo = await _apiService.UpdateCompany(new CompanyDto
                {
                    Id = CompanyDto.Id,
                    CompanyName = CompanyName,
                    CompanyEmail = CompanyEmail,
                    CompanyPhoneNumber = CompanyPhoneNumber,
                    CompanyImage = NewImage
                }) ;
                await App.Current.MainPage.DisplayAlert("Alert", $"{newCompanyInfo}", "OK");
                return true;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", $"{ex}", "OK");
                Console.WriteLine(ex);
                return false;
            }
        }
        [RelayCommand]
        public async Task<bool> AddNewCompany()
        {
            try
            {
                string numberString = CompanyPhoneNumber.ToString();
                if (numberString.Length != 12)
                {
                    await App.Current.MainPage.DisplayAlert("Упс!", $"Неверно введен номер!", "OK");
                    return false;
                }
                if(IsValidEmail(CompanyEmail) == false)
                {
                    await App.Current.MainPage.DisplayAlert("Упс!", $"Неверно введен email!", "OK");
                    return false;
                }
                if (string.IsNullOrEmpty(CompanyName) || string.IsNullOrEmpty(CompanyEmail))
                {
                    await App.Current.MainPage.DisplayAlert("Упс!", $"Неверно введены данные!", "OK");
                    return false;
                }
                var info = await _apiService.AddNewCompany(new CompanyDto
                {
                    CompanyName = CompanyName,
                    CompanyEmail = CompanyEmail,
                    CompanyPhoneNumber = CompanyPhoneNumber,
                    CompanyImage = NewImage
                });
                await App.Current.MainPage.DisplayAlert("Alert", $"{info}", "OK");
                return true;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", $"{ex}", "OK");
                Console.WriteLine(ex);
                return false;
            }
        }
        [RelayCommand]
        public async Task<bool> DeleteCompany()
        {
            try
            {
                var info = await _apiService.DeleteCompany(CompanyDto.Id);
                await App.Current.MainPage.DisplayAlert("Alert", $"{info}", "OK");
                return true;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", $"{ex}", "OK");
                Console.WriteLine(ex);
                return false;
            }
        }
        public string Cheack()
        {
            if (CompanyDto.CompanyName == string.Empty)
            {
                NewImage = new byte[0];

                CompanyName = CompanyDto.CompanyName;
                CompanyEmail = CompanyDto.CompanyEmail;
                CompanyPhoneNumber = CompanyDto.CompanyPhoneNumber;

                AddButtonVisible = true;
                UpdateButtonVisible = false;
                DeleteButtonVisible = false;

                return "new company";
            }
            else
            {
                NewImage = new byte[0];
                NewImage = CompanyDto.CompanyImage;
                CompanyName = CompanyDto.CompanyName;
                CompanyEmail = CompanyDto.CompanyEmail;
                CompanyPhoneNumber = CompanyDto.CompanyPhoneNumber;
                AddButtonVisible = false;
                UpdateButtonVisible = true;
                DeleteButtonVisible = true;

                return CompanyDto.CompanyName;
            }
            
        }
        public bool IsValidEmail(string email)
        {
            string pattern = "[.\\-_a-z0-9]+@([a-z0-9][\\-a-z0-9]+\\.)+[a-z]{2,6}";
            Match isMatch = Regex.Match(email, pattern, RegexOptions.IgnoreCase);
            return isMatch.Success;
        }
    }
}
