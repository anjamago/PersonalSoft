using Entities.DTO;
using FluentValidation;
using Repository.Interface;

namespace Business.Customer.Create
{
    public sealed class CustomerCommandValidate : AbstractValidator<CustomerCommand>
    {
        public CustomerCommandValidate(ICustomerRepository _customer)
        {
            RuleFor(r => r.Identification).MustAsync(async (identification, _)
                    => !await _customer.IsIdentificationUniquedAsync(identification)
            ).WithMessage("Identificacion del cliente ya se encuentra reguistrada en el sistema");
        }
    }
}
