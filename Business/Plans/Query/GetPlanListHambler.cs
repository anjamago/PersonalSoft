using Entities.DTO;
using MediatR;
using Repository.Interface;

namespace Business.Plans.Query
{
    internal sealed class GetPlanListHambler : IRequestHandler<GetPlansList, List<Plan>>
    {
        private readonly IPlanRepository planRepository;

        public GetPlanListHambler(IPlanRepository planRepository)
        {
            this.planRepository = planRepository;
        }

        public async Task<List<Plan>> Handle(GetPlansList request, CancellationToken cancellationToken)
        {
            var listPlan = await planRepository.GetList();
            return listPlan.Select(s => new Plan() { Id = s.Id, PlanName = s.PlanName, ToppingsId = s.ToppingsId, TotalCoverage = s.TotalCoverage }).ToList();
        }
    }
}
