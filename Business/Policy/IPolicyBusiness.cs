using Business.Create;
using Business.Policy.Create;
using Entities;
using Entities.DTO;

namespace Business
{
    public interface IPolicyBusiness
    {
        Task<ResponseBase<List<string>>> Create(CreatePolicyCommand policy);
        Task<ResponseBase<List<string>>> CreateIdCustomer(CreatePolicyIdCommand policy);
        
        Task<ResponseBase<PolicyCustomerDto>> Find(string  policNumberOrPlaque);
        Task<ResponseBase<List<PolicyCustomerDto>>> GetAll();

    }
}