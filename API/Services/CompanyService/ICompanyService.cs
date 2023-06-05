using API.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace API.Services.CompService
{
    public interface ICompanyService
    {
        Task<CompanyDto> GetCompany(int id);
        Task<List<CompanyDto>> GetAllCompanies();
        Task<string> UpdateCompany(CompanyDto companyDto);
        Task<string> AddNewCompany(CompanyDto companyDto);
        Task<string> DeleteCompany(int id);


        Task<CompanyDto> GetCompanyByName(string name);
        Task<List<ServiceDto>> GetServicesByCompany(string name);
        Task<string> AddNewService(ServiceDto serviceDto);
        Task<string> UpdateService(ServiceDto serviceDto);
        Task<string> DeleteService(string companyName, string serviceName);
        
    }
}
