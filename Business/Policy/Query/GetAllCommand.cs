using Entities.DTO;
using MediatR;

namespace Business.Query;

public class GetAllCommand : IRequest<List<PolicyCustomerDto>>
{
    
}