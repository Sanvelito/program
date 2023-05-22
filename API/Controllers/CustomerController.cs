
using API.Models;
using API.Models.Dto;
using API.Services.CustService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _CustomerService;
        public CustomerController(ICustomerService customerService)
        {
            _CustomerService = customerService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerServiceDto>> GetOrderById(int id)
        {
            var result = await _CustomerService.GetCustomerOrder(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<CustomerServiceDto>>> GetAllOrders()
        {
            var result = await _CustomerService.GetCustomerOrders();
            return Ok(result);
        }

        [HttpPost]
        [Authorize (Roles = "user")]
        public async Task<ActionResult<CustomerServiceDto>> AddCustomerService(CustomerServiceDto dto)
        {
            var result = await _CustomerService.AddCustomerService(dto);
            return Ok(result);
        }
    }
}
