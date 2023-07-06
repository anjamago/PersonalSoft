using Entities.DTO;
using Entities.Interface.Repositories;
using MediatR;

namespace Business.Customer.Query
{
    internal class GetCustomerByIdHambler : IRequestHandler<GetCustomerByIdRequest, CustomerResponseModel>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomerByIdHambler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<CustomerResponseModel> Handle(GetCustomerByIdRequest request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetFindId(request.Id);
            return customer.Select(s => new CustomerResponseModel(
                     Name: s.Name,
                     City: s.City,
                     Address: s.Address,
                     Id: s.Id,
                     Identification: s.Identification
                 )).First();
        }
    }
}
