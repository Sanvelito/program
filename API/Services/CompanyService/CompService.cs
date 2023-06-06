using API.Models;
using API.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace API.Services.CompService
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
            //asd
            var company = (await _context.Companies.FirstOrDefaultAsync(c => c.Id == companyDto.Id))!;

            //asd
            var anotherCompany = (await _context.Companies.FirstOrDefaultAsync(c => c.Name == companyDto.CompanyName))!;

            if (company is null)
                return "Компания не найдена!";

            if (anotherCompany.Id != company.Id)
            {
                return "Такое название уже используется!";
            }

            company.Name = companyDto.CompanyName;
            company.PhoneNumber = companyDto.CompanyPhoneNumber;
            company.Email = companyDto.CompanyEmail;
            company.Image = companyDto.CompanyImage;

            await _context.SaveChangesAsync();

            return "Выполнено!";
        }
        public async Task<string> AddNewCompany(CompanyDto companyDto)
        {
            var company = (await _context.Companies.FirstOrDefaultAsync(c => c.Name == companyDto.CompanyName))!;
            if(company != null )
            {
                return "Компания не найдена!";
            }

            Company newComp = new Company();
            newComp.Name = companyDto.CompanyName;
            newComp.PhoneNumber = companyDto.CompanyPhoneNumber;
            newComp.Email = companyDto.CompanyEmail;
            newComp.Image = companyDto.CompanyImage;

            _context.Companies.Add(newComp);
            await _context.SaveChangesAsync();
            return "Выполнено!";
        }
        public async Task<string> DeleteCompany(int id)
        {
            var company = (await _context.Companies.FirstOrDefaultAsync(c => c.Id == id))!;
            if (company is null)
                return "Компания не найдена!";
            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
            return "Выполнено!";
        }
        public async Task<CompanyDto> GetCompanyByName(string name)
        {
            var company = (await _context.Companies.FirstOrDefaultAsync(c => c.Name == name))!;

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
        public async Task<List<ServiceDto>> GetServicesByCompany(string name)
        {
            var company = await _context.Companies.FirstOrDefaultAsync(c => c.Name == name);

            if (company == null)
                return null;

            var result = new List<ServiceDto>();

            var services = await _context.CompanyServices
                .Include(cs => cs.Service)
                .ThenInclude(s => s.ServiceGroup)
                .Where(cs => cs.CompanyId == company.Id)
                .ToListAsync();

            foreach (var companyService in services)
            {
                result.Add(new ServiceDto
                {
                    CompanyName = company.Name,
                    GroupName = companyService.Service.ServiceGroup.Name,
                    ServiceName = companyService.Service.Name,
                    Description = companyService.Service.Description,
                    Price = companyService.Price
                });
            }

            return result;
        }

        public async Task<string> AddNewService(ServiceDto serviceDto)
        {
            var company = await _context.Companies.FirstOrDefaultAsync(c => c.Name == serviceDto.CompanyName);

            if (company == null)
                return null;

            var serviceGroup = await _context.ServiceGroups.FirstOrDefaultAsync(sg => sg.Name == serviceDto.GroupName);

            if (serviceGroup == null)
                return null;

            var service = new Service
            {
                Name = serviceDto.ServiceName,
                Description = serviceDto.Description,
                ServiceGroup = serviceGroup
            };

            await _context.Services.AddAsync(service);
            await _context.SaveChangesAsync();

            var companyService = new CompanyService
            {
                Company = company,
                Service = service,
                Price = serviceDto.Price
            };

            await _context.CompanyServices.AddAsync(companyService);
            await _context.SaveChangesAsync();

            return "Услуга успешно добавлена!";
        }
        public async Task<string> UpdateService(ServiceDto serviceDto)
        {
            var company = await _context.Companies.FirstOrDefaultAsync(c => c.Name == serviceDto.CompanyName);

            if (company == null)
                return null;

            var serviceGroup = await _context.ServiceGroups.FirstOrDefaultAsync(sg => sg.Name == serviceDto.GroupName);

            if (serviceGroup == null)
                return null;

            var service = await _context.Services.FirstOrDefaultAsync(s => s.Name == serviceDto.ServiceName);

            if (service == null)
                return null;

            service.Description = serviceDto.Description;
            service.ServiceGroup = serviceGroup;

            var companyService = await _context.CompanyServices.FirstOrDefaultAsync(cs => cs.ServiceId == service.Id && cs.CompanyId == company.Id);

            if (companyService == null)
                return null;

            companyService.Price = serviceDto.Price;

            await _context.SaveChangesAsync();

            return "Услуга успешно обновлена!";
        }
        public async Task<string> DeleteService(string companyName, string serviceName)
        {
            var company = await _context.Companies.FirstOrDefaultAsync(c => c.Name == companyName);

            if (company == null)
                return null;

            var service = await _context.Services.FirstOrDefaultAsync(s => s.Name == serviceName);

            if (service == null)
                return null;

            var companyService = await _context.CompanyServices.FirstOrDefaultAsync(cs => cs.ServiceId == service.Id && cs.CompanyId == company.Id);

            if (companyService == null)
                return null;

            _context.CompanyServices.Remove(companyService);
            await _context.SaveChangesAsync();

            return "Услуга успешно удалена!";
        }
    }
}
