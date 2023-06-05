using API.Models.Dto;
using API.Services.CompService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : Controller
    {
        private readonly ICompanyService _CompanyService;
        public CompanyController(ICompanyService companyService)
        {
            _CompanyService = companyService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyDto>> GetCompany(int id)
        {
            var result = await _CompanyService.GetCompany(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<CompanyDto>> GetAllCompanies()
        {
            var result = await _CompanyService.GetAllCompanies();
            return Ok(result);
        }
        [HttpPut]
        public async Task<ActionResult<string>> UpdateCompany(CompanyDto companyDto)
        {
            var result = await _CompanyService.UpdateCompany(companyDto);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteCompany(int id)
        {
            var result = await _CompanyService.DeleteCompany(id);
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult<string>> AddNewCompany(CompanyDto companyDto)
        {
            var result = await _CompanyService.AddNewCompany(companyDto);
            return Ok(result);
        }

        [HttpGet("get-company-byname")]
        public async Task<ActionResult<CompanyDto>> GetCompanyByName(string name)
        {
            var result = await _CompanyService.GetCompanyByName(name);
            return Ok(result);
        }
        [HttpGet("get-services-by-company")]
        public async Task<ActionResult<List<ServiceDto>>> GetServicesByCompany(string name)
        {
            var result = await _CompanyService.GetServicesByCompany(name);
            return Ok(result);
        }
        [HttpPost("add-new-service")]
        public async Task<ActionResult<string>> AddNewService(ServiceDto serviceDto)
        {
            var result = await _CompanyService.AddNewService(serviceDto);
            return Ok(result);
        }
        [HttpPost("update-service")]
        public async Task<ActionResult<string>> UpdateService(ServiceDto serviceDto)
        {
            var result = await _CompanyService.UpdateService(serviceDto);
            return Ok(result);
        }
        [HttpDelete("delete-service")]
        public async Task<ActionResult<string>> DeleteService(string companyName, string serviceName)
        {
            var result = await _CompanyService.DeleteService(companyName, serviceName);
            return Ok(result);
        }
    }
}
