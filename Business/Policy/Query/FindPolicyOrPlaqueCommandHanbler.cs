using Entities.DTO;
using Repository.Interface;

namespace Business.Query;
using MediatR;
public class FindPolicyOrPlaqueCommandHanbler:IRequestHandler<FindPolicyOrPlaqueCommand, PolicyCustomerDto>
{
    private readonly IPolicyRepository _policyRepository;
    private readonly ICustomerRepository _customerRepository;

    public FindPolicyOrPlaqueCommandHanbler(IPolicyRepository policyRepository, ICustomerRepository customerRepository)
    {
        _policyRepository = policyRepository;
        _customerRepository = customerRepository;
    }

    public async Task<PolicyCustomerDto> Handle(FindPolicyOrPlaqueCommand request, CancellationToken cancellationToken)
    {
        var result = await _policyRepository.GetPolicyPlaqueAsync(request.policyOrPlaque);
        if (result is not null)
        {
            var customers =await _customerRepository.GetFindId(result.IdCustomer);
            var customer = customers.FirstOrDefault();
            result.Name = customer.Name;
            result.City = customer.City;
            result.Address = customer.Address;
            result.Identification = customer.Identification;
        }
        return result;
    }
}