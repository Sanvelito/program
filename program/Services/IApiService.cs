using program.Model;
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
        [Post("/api/Auth/login")]
        Task<LoginDto> Login([Body] UserDto request);
    }
}
