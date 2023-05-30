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

        [Post("/api/Auth/register")]
        Task<RegisterDto> Register([Body] RegisterDto request);

        [Get("/api/Company")]
        Task<List<CompanyDto>> GetAllCompanies();

        [Headers("Authorization: Bearer")]
        [Post("/api/Customer")]
        Task<CustomerServiceDto> AddCustomerService([Body] CustomerServiceDto request);

        [Headers("Authorization: Bearer")]
        [Get("/api/User")]
        Task<UserInfoDto> GetMyInfo();

        [Headers("Authorization: Bearer")]
        [Put("/api/User")]
        Task<UserInfoDto> UpdateUserInfo([Body] UserInfoDto request);
    }
}
