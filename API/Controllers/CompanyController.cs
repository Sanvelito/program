﻿using API.Services.CompanyService;
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
        //[HttpPut("{id}")]
        //public async Task<ActionResult<CompanyDto>> PutCompany(int id)
        //{
        //    var result = await _CompanyService.GetCompany(id);
        //    return Ok(result);
        //}
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<CompanyDto>> DeleteCompany(int id)
        //{
        //    var result = await _CompanyService.GetCompany(id);
        //    return Ok(result);
        //}
    }
}