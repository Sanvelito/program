using API.Models.Dto;
using API.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace API.Services.CustService
{
    public class CustService : ICustomerService
    {
        public IHttpContextAccessor _httpContextAccessor { get; }
        public DataContext _context { get; }
        public CustService(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<CustomerServiceDto> GetCustomerOrder(int id)
        {
            var customerService = await _context.CustomerServices
                .Include(cs => cs.User)
                .Include(cs => cs.CompanyService)
                .ThenInclude(cs => cs.Service)
                .FirstOrDefaultAsync(cs => cs.Id == id);

            var result = new CustomerServiceDto
            {
                //Id = customerService.Id,
                Username = customerService.User.Username,
                CompanyName = customerService.CompanyName,
                CreatedDate = customerService.CreatedDate,
                ServiceName = customerService.CompanyService.Service.Name,
                DeadLine = customerService.DeadLine,
                Description = customerService.Description,
                Address = customerService.Address,
                Status = customerService.Status
            };

            return result;
        }

        public async Task<List<CustomerServiceDto>> GetCustomerOrders()
        {
            var customerServices = await _context.CustomerServices
                .Include(cs => cs.User)
                .Include(cs => cs.CompanyService)
                .ThenInclude(cs => cs.Service)
                .ToListAsync();

            var result = customerServices.Select(cs => new CustomerServiceDto
            {
                Id = cs.Id,
                Username = cs.User.Username,
                CompanyName = cs.CompanyName,
                CreatedDate = cs.CreatedDate,
                ServiceName = cs.CompanyService.Service.Name,
                DeadLine = cs.DeadLine,
                Description = cs.Description,
                Address = cs.Address,
                Status = cs.Status
            }).ToList();

            return result;
        }

        public async Task<CustomerServiceDto> AddCustomerService(CustomerServiceDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == dto.Username);

            var service = _context.CompanyServices
                .Include(cs => cs.Company)
                .Include(cs => cs.Service)
                .ThenInclude(s => s.ServiceGroup)
                    .FirstOrDefault(cs => cs.Service.Name == dto.ServiceName);

            var company = _context.Companies.FirstOrDefault(cs => cs.Name == dto.CompanyName);

            // Создаем новый объект CustomerService и присваиваем ему значения из dto
            var customerService = new CustomerService
            {
                UserId = user.Id,
                CompanyServiceId = service.Id,
                CompanyName = company?.Name,
                CreatedDate = DateTime.Now,
                DeadLine = dto.DeadLine,
                Description = dto.Description,
                Address = dto.Address,
                Status = dto.Status
            };

            // Добавляем новый объект CustomerService в контекст и сохраняем изменения
            _context.CustomerServices.Add(customerService);
            await _context.SaveChangesAsync();

            return dto;
        }
        public async Task<List<CustomerServiceDto>> GetUserOrders()
        {
            var Username = string.Empty;
            if (_httpContextAccessor != null)
            {
                Username = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            }
            var user = await _context.Users.FirstOrDefaultAsync(p => p.Username == Username);

            var customerServices = await _context.CustomerServices
                .Include(cs => cs.User)
                .Include(cs => cs.CompanyService)
                .ThenInclude(cs => cs.Service).Where(cs => cs.User.Username == user.Username)
                .ToListAsync();

            var result = customerServices.Select(cs => new CustomerServiceDto
            {
                Id = cs.Id,
                Username = cs.User.Username,
                FirstName = cs.User.FirstName,
                LastName = cs.User.LastName,
                PhoneNumber = cs.User.PhoneNumber,
                CompanyName = cs.CompanyName,
                CreatedDate = cs.CreatedDate,
                ServiceName = cs.CompanyService.Service.Name,
                DeadLine = cs.DeadLine,
                Description = cs.Description,
                Address = cs.Address,
                Status = cs.Status
            }).ToList();

            return result;
        }
        public async Task<List<CustomerServiceDto>> GetCompanyOrders(string name)
        {

            var customerServices = await _context.CustomerServices
            .Include(cs => cs.User)
            .Include(cs => cs.CompanyService)
            .ThenInclude(cs => cs.Service)
            .Where(cs => cs.CompanyName == name)
            .ToListAsync();

            var result = customerServices.Select(cs => new CustomerServiceDto
            {
                Id = cs.Id,
                Username = cs.User.Username,
                FirstName = cs.User.FirstName,
                LastName = cs.User.LastName,
                PhoneNumber = cs.User.PhoneNumber,
                CompanyName = cs.CompanyName,
                CreatedDate = cs.CreatedDate,
                ServiceName = cs.CompanyService.Service.Name,
                DeadLine = cs.DeadLine,
                Description = cs.Description,
                Address = cs.Address,
                Status = cs.Status
            }).ToList();

            return result;
        }
        public async Task<string> UpdateOrder(CustomerServiceDto customerServiceDto)
        {
            var customerService = await _context.CustomerServices.FirstOrDefaultAsync(cs => cs.Id == customerServiceDto.Id);

            if (customerService == null)
                return null;

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == customerServiceDto.Username);
            var companyService = await _context.CompanyServices.FirstOrDefaultAsync(cs => cs.Service.Name == customerServiceDto.ServiceName);

            if (user == null || companyService == null)
                return null;

            customerService.User = user;
            customerService.CompanyName = customerServiceDto.CompanyName;
            customerService.CompanyService = companyService;
            customerService.CreatedDate = customerServiceDto.CreatedDate;
            customerService.DeadLine = customerServiceDto.DeadLine;
            customerService.Description = customerServiceDto.Description;
            customerService.Address = customerServiceDto.Address;
            customerService.Status = customerServiceDto.Status;

            await _context.SaveChangesAsync();

            return "Выполнено!";
        }

    }
}
