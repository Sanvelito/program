using API.Models.Dto;

namespace API.Services.CustService
{
    public interface ICustomerService
    {
        Task<CustomerServiceDto> GetCustomerOrder(int id);
        Task<List<CustomerServiceDto>> GetCustomerOrders();
        Task<CustomerServiceDto> AddCustomerService(CustomerServiceDto dto);
    }
}
