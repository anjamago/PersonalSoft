using Entities.DTO;
using MediatR;
using Repository.Interface;

namespace Business.Query;

public class GetAllCommandHanble:IRequestHandler<GetAllCommand, List<PolicyCustomerDto>>
{
    private readonly IPolicyRepository _policyRepository;

    public GetAllCommandHanble(IPolicyRepository policyRepository)
    {
        _policyRepository = policyRepository;
    }

    public  async Task<List<PolicyCustomerDto>> Handle(GetAllCommand request, CancellationToken cancellationToken)
        =>await _policyRepository.GetAll();
}