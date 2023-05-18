using API.Models.Dto;

namespace API.Services.CompanyService
{
    public interface ICompanyService
    {
        Task<CompanyDto> GetCompany(int id);
        Task<List<CompanyDto>> GetAllCompanies();
    }
}
