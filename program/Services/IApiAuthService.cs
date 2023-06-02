using program.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace program.Services
{
    public interface IApiAuthService
    {
        [Post("/api/Auth/refresh-token")]
        Task<string> RefreshToken([Query] string request);

        [Post("/api/Auth/check-auth")]
        Task<LoginDto> CheckAuth([Query] string request);
        [Post("/api/Auth/login")]
        Task<LoginDto> Login([Body] UserDto request);
        [Post("/api/Auth/register")]
        Task<RegisterDto> Register([Body] RegisterDto request);

        [Put("/api/Company")]
        Task<string> UpdateCompany([Body] CompanyDto requst);
        [Post("/api/Company")]
        Task<string> AddNewCompany([Body] CompanyDto requst);
        [Delete("/api/Company/{id}")]
        Task<string> DeleteCompany(int id);
    }
}
