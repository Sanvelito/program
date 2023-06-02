using API.Models.Dto;
using API.Services.CompanyService;
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
    }
}
