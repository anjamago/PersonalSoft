using Entities.DTO;
using MediatR;

namespace Business.Customer.Query
{
    public class GetCustomerByIdRequest : IRequest<CustomerResponseModel>
    {
        public string Id { get; set; }
    }
}
