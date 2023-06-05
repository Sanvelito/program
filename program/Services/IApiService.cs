using program.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace program.Services
{
    public interface IApiService
    {
        //[Post("/api/Auth/check-auth")]
        //Task<LoginDto> CheckAuth([Query] string request);

        //[Post("/api/Auth/login")]
        //Task<LoginDto> Login([Body] UserDto request);

        //[Post("/api/Auth/register")]
        //Task<RegisterDto> Register([Body] RegisterDto request);

        [Headers("Authorization: Bearer")]
        [Post("/api/Customer")]
        Task<CustomerServiceDto> AddCustomerService([Body] CustomerServiceDto request);

        [Headers("Authorization: Bearer")]
        [Get("/api/User")]
        Task<UserInfoDto> GetMyInfo();

        [Headers("Authorization: Bearer")]
        [Post("/api/User/update-myinfo")]
        Task<string> UpdateUserInfo([Body] UserInfoDto request);

        [Headers("Authorization: Bearer")]
        [Get("/api/Customer")]
        Task<List<CustomerServiceDto>> GetUserOrders();

        //admin manager control
        [Headers("Authorization: Bearer")]
        [Get("/api/User/get-all-managers")]
        Task<List<UserInfoDto>> GetAllManagers();
        [Headers("Authorization: Bearer")]
        [Post("/api/User/add-new-manager")]
        Task<string> AddNewManager([Body] UserInfoDto request);
        [Headers("Authorization: Bearer")]
        [Post("/api/User/update-manager")]
        Task<string> UpdateManager([Body] UserInfoDto request);
        [Headers("Authorization: Bearer")]
        [Delete("/api/User/delete-manager")]
        Task<string> DeleteManager([Body] UserInfoDto request);

        //manager
        [Headers("Authorization: Bearer")]
        [Get("/api/Customer/get-company-orders")]
        Task<List<CustomerServiceDto>> GetCompanyOrders([Query] string name);

        [Headers("Authorization: Bearer")]
        [Post("/api/Customer/update-order")]
        Task<string> UpdateOrder([Body] CustomerServiceDto request);
    }
}
