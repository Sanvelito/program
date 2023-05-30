using API.Models.Dto;
using API.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace API.Services.CustService
{
    public class CustService : ICustomerService
    {
        public DataContext _context { get; }
        public CustService(DataContext context)
        {
            _context = context;
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
                Description = dto.Description,
                Address = dto.Address,
                Status = dto.Status
            };

            // Добавляем новый объект CustomerService в контекст и сохраняем изменения
            _context.CustomerServices.Add(customerService);
            await _context.SaveChangesAsync();

            // Обновляем Id у объекта dto
            //dto.Id = customerService.Id;

            return dto;
        }
    }
}
