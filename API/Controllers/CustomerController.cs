﻿
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

        [HttpPost]
        [Authorize (Roles = "user")]
        public async Task<ActionResult<CustomerServiceDto>> AddCustomerService(CustomerServiceDto dto)
        {
            var result = await _CustomerService.AddCustomerService(dto);
            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<CustomerServiceDto>>> GetUserOrders()
        {
            var result = await _CustomerService.GetUserOrders();
            return Ok(result);
        }
        [HttpGet("get-company-orders")]
        [Authorize (Roles = "manager")]
        public async Task<ActionResult<List<CustomerServiceDto>>> GetCompanyOrders(string name)
        {
            var result = await _CustomerService.GetCompanyOrders(name);
            if(result == null)
                return BadRequest("Что-то пошло не так!");
            return Ok(result);
        }
        [HttpPost("update-order")]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult<string>> UpdateOrder(CustomerServiceDto dto)
        {
            var result = await _CustomerService.UpdateOrder(dto);
            return Ok(result);
        }
    }
}
