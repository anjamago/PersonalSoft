using Entities.DTO;
using MediatR;
using Repository.Interface;

namespace Business.Customer.Query
{
    internal class GetCustomerHambler : IRequestHandler<GetCustomersRequest, List<CustomerResponseModel>>
    {
        private readonly ICustomerRepository _customerRepository;
        public GetCustomerHambler(
            ICustomerRepository customer
            )
        {
            _customerRepository = customer;
        }
        public async Task<List<CustomerResponseModel>> Handle(GetCustomersRequest request, CancellationToken cancellationToken)
        {
            var list = await _customerRepository.GetList();
            return list.Select(s => new CustomerResponseModel(
                     Name: s.Name,
                     City: s.City,
                     Address: s.Address,
                     Id: s.Id,
                     Identification: s.Identification
                 )).ToList();


        }
    }
}
