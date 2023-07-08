using Business;
using Business.Create;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PolicyController: ControllerBase
    {

        private readonly IPolicyBusiness business;

        public PolicyController(IPolicyBusiness business)
        {
            this.business = business;
        }

      

        [HttpPost]
        public async Task<IActionResult> Create(CreatePolicyCommand policy)
        {
            var response = await business.Create(policy);
            if (response.Data is null)
            {
                return NoContent();
            }
            return StatusCode(response.Code, response);

        }
    }
}
