using MediatR;
namespace Entities.DTO;
public record  CustomerCommand(
     string Name,
     int Identification,
     string City,
     string Address

):IRequest;

    