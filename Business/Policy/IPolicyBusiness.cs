using Business.Create;
using Entities;

namespace Business
{
    public interface IPolicyBusiness
    {
        Task<ResponseBase<List<string>>> Create(CreatePolicyCommand policy);
    }
}