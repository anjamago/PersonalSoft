using Business.Create;
using FluentValidation;

namespace Business.Policy.Create
{
    public class CreateCommandIdValidate : AbstractValidator<CreatePolicyCommand>
    {
        public CreateCommandIdValidate()
        {
            RuleFor(x => x.policyNumber).NotEmpty().NotNull().WithMessage("Se requiere un numero de poliza valido");
            RuleFor(x => x.idPlan).NotEmpty().NotNull().WithMessage("Selecione un plan valido");
            RuleFor(x => x.plaque).NotEmpty().NotNull().WithMessage("Ingrese la placa del vehiculo");
            RuleFor(x => x.vehicleModel).NotEmpty().NotNull().WithMessage("Ingrese el modelo del vehiculo");
            RuleFor(x => x.whitInspection).Must(x => x == true || x == false).WithMessage("Indique si el vehiculo cuenta con inspecion ");


            RuleFor(x => x.IdCustomer).NotEmpty().NotNull();
        }
    }
}
