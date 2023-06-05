using API.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace API.Services.CustService
{
    public interface ICustomerService
    {
        Task<CustomerServiceDto> GetCustomerOrder(int id);
        Task<List<CustomerServiceDto>> GetCustomerOrders();
        Task<CustomerServiceDto> AddCustomerService(CustomerServiceDto dto);
        Task<List<CustomerServiceDto>> GetUserOrders();
        Task<List<CustomerServiceDto>> GetCompanyOrders(string name);
        Task<string> UpdateOrder(CustomerServiceDto dto);
    }
}
