using API.Models;
using API.Models.Dto;
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
            var company = (await _context.Companies.FirstOrDefaultAsync(c => c.Id == id))!;

            var result = new CompanyDto
            {
                Id = company.Id,
                CompanyName = company.Name,
                CompanyPhoneNumber = company.PhoneNumber,
                CompanyEmail = company.Email,
                CompanyImage = company.Image
            };

            var serivces = _context.CompanyServices
                .Include(s => s.Company)
                .Include(s => s.Service)
                .ThenInclude(s => s.ServiceGroup).Where(cs => cs.CompanyId == company.Id);

            foreach (var companyService in serivces)
            {
                if (!result.ServicesGroup.ContainsKey(companyService.Service.ServiceGroup.Name))
                    result.ServicesGroup.Add(
                        companyService.Service.ServiceGroup.Name,
                        new List<CompanyServiceDto>()
                    );

                result.ServicesGroup[companyService.Service.ServiceGroup.Name].Add(
                    new CompanyServiceDto
                    {
                        Name = companyService.Service.Name,
                        Description = companyService.Service.Description,
                        Price = companyService.Price
                    }
                );
            }

            return result;
        }
        public async Task<List<CompanyDto>> GetAllCompanies()
        {
            var companies = await _context.Companies.ToListAsync();
            var result = new List<CompanyDto>();

            foreach (var company in companies)
            {
                var services = _context.CompanyServices
                    .Include(cs => cs.Company)
                    .Include(cs => cs.Service)
                    .ThenInclude(s => s.ServiceGroup)
                    .Where(cs => cs.CompanyId == company.Id)
                    .ToList();

                var companyDto = new CompanyDto
                {
                    Id = company.Id,
                    CompanyName = company.Name,
                    CompanyPhoneNumber = company.PhoneNumber,
                    CompanyEmail = company.Email,
                    CompanyImage = company.Image,
                    ServicesGroup = new Dictionary<string, List<CompanyServiceDto>>()
                };

                foreach (var companyService in services)
                {
                    var serviceGroup = companyService.Service.ServiceGroup.Name;

                    if (!companyDto.ServicesGroup.ContainsKey(serviceGroup))
                    {
                        companyDto.ServicesGroup[serviceGroup] = new List<CompanyServiceDto>();
                    }

                    companyDto.ServicesGroup[serviceGroup].Add(new CompanyServiceDto
                    {
                        Name = companyService.Service.Name,
                        Description = companyService.Service.Description,
                        Price = companyService.Price
                    });
                }

                result.Add(companyDto);
            }

            return result;
        }
        public async Task<string> UpdateCompany(CompanyDto companyDto)
        {
            var company = (await _context.Companies.FirstOrDefaultAsync(c => c.Id == companyDto.Id))!;

            var anotherCompany = (await _context.Companies.FirstOrDefaultAsync(c => c.Name == companyDto.CompanyName))!;

            if (company is null)
                return "Company not found";

            if (anotherCompany != null)
            {
                return "Company name used";
            }

            company.Name = companyDto.CompanyName;
            company.PhoneNumber = companyDto.CompanyPhoneNumber;
            company.Email = companyDto.CompanyEmail;
            company.Image = companyDto.CompanyImage;

            await _context.SaveChangesAsync();

            return "Success";
        }
        public async Task<string> AddNewCompany(CompanyDto companyDto)
        {
            var company = (await _context.Companies.FirstOrDefaultAsync(c => c.Name == companyDto.CompanyName))!;
            if(company != null )
            {
                return "Company name used";
            }

            Company newComp = new Company();
            newComp.Name = companyDto.CompanyName;
            newComp.PhoneNumber = companyDto.CompanyPhoneNumber;
            newComp.Email = companyDto.CompanyEmail;
            newComp.Image = companyDto.CompanyImage;

            _context.Companies.Add(newComp);
            await _context.SaveChangesAsync();
            return "Success";
        }
        public async Task<string> DeleteCompany(int id)
        {
            var company = (await _context.Companies.FirstOrDefaultAsync(c => c.Id == id))!;
            if (company is null)
                return "Company not found";
            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
            return "Success";
        }
    }
}
