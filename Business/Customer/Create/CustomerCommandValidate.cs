using Entities.DTO;
using Entities.Interface.Repositories;
using FluentValidation;
namespace Business.Customer.Create
{
    public sealed class CustomerCommandValidate : AbstractValidator<CustomerCommand>
    {
        public CustomerCommandValidate(ICustomerRepository _customer)
        {
            RuleFor(r => r.Identification).MustAsync(async (identification, _) =>
            {
                return !await _customer.IsIdentificationUniquedAsync(identification);
            }).WithMessage("Identificacion del cliente ya se encuentra reguistrada en el sistema");
        }
    }
}
