using MediatR;

namespace Business.Create;

public record CreatePolicyCommand(
    string policyNumber,
    string idPlan,
    string plaque,
    string vehicleModel,
    bool whitInspection,
    string StartDate,
    string EndDate,
    string? IdCustomer,
    string City,
    string Address,
    string customerName,
    string identification
    ) : IRequest<List<string>>;