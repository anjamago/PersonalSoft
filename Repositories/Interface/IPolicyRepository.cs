using Entities.DTO;
using Entities.Models;

namespace Repository.Interface;

public interface IPolicyRepository
{

    Task<PolicyCustomerDto> GetPolicyPlaqueAsync(string policyNumberOrplaque);
    Task<List<PolicyCustomerDto>> GetAll();
    Task Create(Policy policy);
    Task<Policy> GetPolicyCustomerAsync(string idOrIdentification);
}