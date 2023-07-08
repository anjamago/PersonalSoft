using Microsoft.AspNetCore.Mvc;
using Business;
using Entities.DTO;
using Business.Create;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlansController: ControllerBase
    {

        private readonly IPlansBusiness business;

        public PlansController(IPlansBusiness business)
        {
            this.business = business;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await business.GetList();
            return StatusCode(result.Code, result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PlansCommand plan)
        {
            var response = await business.Create(plan);
            if (response.Data is null)
            {
                return NoContent();
            }
            return StatusCode(response.Code, response);

        }
    }
}
