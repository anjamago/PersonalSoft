using Entities.DTO;
using MediatR;

namespace Business.Customer.Query
{
    public class GetCustomersRequest : IRequest<List<CustomerResponseModel>>
    {
    }


}
