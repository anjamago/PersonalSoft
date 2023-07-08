using MediatR;

namespace Business.Create;

public record CreatePolicyCommand(
    string policyNumber,
    string customerName,
    string identification,
    string idPlan,
    string? IdCustomer,
    string City,
    string Address, 
    string plaque, 
    string vehicleModel, 
    bool whitInspection,
    string StartDate,
    string EndDate
    ):IRequest<List<string>>;