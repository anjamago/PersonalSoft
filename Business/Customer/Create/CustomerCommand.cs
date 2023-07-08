using MediatR;
namespace Entities.DTO;
public record CustomerCommand(
     string Name,
     string Identification,
     string City,
     string Address

) : IRequest;

