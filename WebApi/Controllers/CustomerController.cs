using Business;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{
    private readonly CustomersBusiness _business;

    public CustomerController(
        CustomersBusiness business
    )
    {
        _business = business;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Customers customers)
    {
        _business.Create(customers);
        return NoContent();

    }
}