namespace API.Services.CompanyService
{
    public interface ICompanyService
    {
        Task<CompanyDto> GetCompany(int id);
    }
}
