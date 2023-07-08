using MediatR;

namespace Business.Policy.Create
{

    public record CreatePolicyCustomerCommand(
        string policyNumber,
        string idPlan,
        string plaque,
        string vehicleModel,
        bool whitInspection,
        string StartDate,
        string EndDate,
        string City,
        string Address,
        string customerName,
        string identification
    ) : IRequest<List<string>>;
}
