using MediatR;

namespace Business.Policy.Create
{
    public record CreatePolicyIdCommand(
    string policyNumber,
    string idPlan,
    string plaque,
    string vehicleModel,
    bool whitInspection,
    string StartDate,
    string EndDate,
    string IdCustomer
    ) : IRequest<List<string>>;
}
