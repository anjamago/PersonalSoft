using Entities.DTO;
using MediatR;

namespace Business.Plans.Query
{
    public class GetPlansList : IRequest<List<Plan>>
    {
    }
}
