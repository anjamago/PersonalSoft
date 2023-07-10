using Business.Customer;
using Entities.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class CustomerController : ControllerBase
{
    private readonly ICustomersBusiness _business;

    public CustomerController(
        ICustomersBusiness business
    )
    {
        _business = business;
    }

    [HttpGet("List")]
    public async Task<IActionResult> GetList()
    {
        var response = await _business.GetList();
        return StatusCode(response.Code, response);
    }

    [HttpGet("Find")]
    public async Task<IActionResult> GetList(string id)
    {
        var response = await _business.GetFind(id);
        return StatusCode(response.Code, response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CustomerCommand customers)
    {
        var response = await _business.Create(customers);
        if (response.Data is null)
        {
            return NoContent();
        }
        return StatusCode(response.Code, response);

    }
}