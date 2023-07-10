using Business;
using Business.Create;
using Business.Policy.Create;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
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
        
        [HttpPost("PolicyCustomerId")]
        public async Task<IActionResult> Create(CreatePolicyIdCommand policy)
        {
            var response = await business.CreateIdCustomer(policy);
            if (response.Data is null)
            {
                return NoContent();
            }
            return StatusCode(response.Code, response);

        }
        [HttpGet("Find")]
        public async Task<IActionResult> GetAll(string policNumberOrPlaque)
        {
            var result = await business.Find(policNumberOrPlaque);
            return StatusCode(result.Code, result);

        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await business.GetAll();
            return StatusCode(result.Code, result);

        }
    }
}
