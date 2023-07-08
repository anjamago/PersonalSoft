using Entities.Models;

namespace Repository.Interface;

public interface IPolicyRepository
{

    Task<Policy> GetPolicyPlaqueAsync(string policyNumberOrplaque);
    Task Create(Policy policy);
    Task<Policy> GetPolicyCustomerAsync(string idOrIdentification);
}