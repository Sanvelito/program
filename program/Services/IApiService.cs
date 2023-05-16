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
        [Post("/api/Auth/check-auth")]
        Task<LoginDto> CheckAuth([Query] string request);

        [Post("/api/Auth/login")]
        Task<LoginDto> Login([Body] UserDto request);

        [Post("/api/Auth/register")]
        Task<RegisterDto> Register([Body] RegisterDto request);


    }
}
