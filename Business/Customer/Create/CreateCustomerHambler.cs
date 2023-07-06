using Entities.DTO;
using Entities.Interface.Repositories;
using MediatR;
using Entities.Models;

namespace Business.Customer.Create
{
    public sealed class CreateCustomerHambler : IRequestHandler<CustomerCommand>
    {
        private readonly ICustomerRepository _customers;

        public CreateCustomerHambler(ICustomerRepository customers)
        {
            _customers = customers;
        }

        public async Task Handle(CustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customers() { 
                Name = request.Name,
                Identification = request.Identification,
                City = request.City,
                Address = request.Address,
            };

            await _customers.AddCustomerAsync(customer);
            
        }
    }
}
