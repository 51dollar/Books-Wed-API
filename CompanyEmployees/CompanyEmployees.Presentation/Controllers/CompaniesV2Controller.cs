﻿using Microsoft.AspNetCore.Mvc;
using Service;

namespace CompanyEmployees.Presentation.Controllers;

[Route("api/companies")]
[ApiController]
[ApiExplorerSettings(GroupName = "v2")]
public class CompaniesV2Controller : ControllerBase
{
    private readonly IServiceManager _service;

    public CompaniesV2Controller(IServiceManager service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetCompanies()
    {
        var companies = await _service.CompanyService
            .GetAllCompaniesAsync(trackChanges: false);
        
        return Ok(companies);
    }
}