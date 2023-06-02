using API.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace API.Services.CompanyService
{
    public interface ICompanyService
    {
        Task<CompanyDto> GetCompany(int id);
        Task<List<CompanyDto>> GetAllCompanies();
        Task<string> UpdateCompany(CompanyDto companyDto);
        Task<string> AddNewCompany(CompanyDto companyDto);
        Task<string> DeleteCompany(int id);
    }
}
