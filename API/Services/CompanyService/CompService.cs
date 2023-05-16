using Microsoft.EntityFrameworkCore;

namespace API.Services.CompanyService
{
    public class CompService : ICompanyService
    {
        public DataContext _context { get; }
        public CompService(DataContext context)
        {
            _context = context;
        }
        public async Task<CompanyDto> GetCompany(int id)
        {
            var company = await _context.Companies.FirstOrDefaultAsync(c => c.Id == id);

            var CompanyDto = new CompanyDto();
            if (company is null)
                return null;

            CompanyDto.CompanyName = company.Name;
            var services = _context.CompanyServices.Include((cs) => cs.Service).Where((cs) => cs.CompanyId == company.Id);
            foreach (var service in services)
            {
                CompanyDto.Services.Add(new CompanyServiceDto {Description = service.Service.Description, Name = service.Service.Name, Price = service.Price});
            }
            return CompanyDto;
        }
    }
}
