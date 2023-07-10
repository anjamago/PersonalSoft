using Entities.DTO;
using MediatR;

namespace Business.Query;

public class FindPolicyOrPlaqueCommand : IRequest<PolicyCustomerDto>
{
    public string policyOrPlaque { set; get; }
}