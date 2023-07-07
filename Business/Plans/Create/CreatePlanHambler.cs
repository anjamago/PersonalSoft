namespace Business.Create;
using MediatR;
using Repository.Interface;

public class CreatePlanHambler : IRequestHandler<PlansCommand>
{
    private readonly IPlanRepository _planRepository;

    public CreatePlanHambler(IPlanRepository planRepository)
    {
        _planRepository = planRepository;
    }

    public async Task Handle(PlansCommand request, CancellationToken cancellationToken)
    {
        var plan = new Entities.Models.Plans()
        {
            ToppingsId = request.ToppingsId,
            PlanName = request.Name,
            TotalCoverage = request.TotalCoverage,

        };
        await _planRepository.Create(plan);
    }
}