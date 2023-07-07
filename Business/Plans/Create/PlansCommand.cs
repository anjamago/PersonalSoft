using MediatR;

namespace Business.Create;

public record PlansCommand(
        string Name, string TotalCoverage, List<string> ToppingsId
    ) : IRequest;