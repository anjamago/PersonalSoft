using FluentValidation;

namespace Business.Create;

public class CreatePolicyCommandValidate : AbstractValidator<CreatePolicyCommand>
{
    public CreatePolicyCommandValidate()
    {
        RuleFor(x => x.policyNumber).NotEmpty().NotNull().WithMessage("Se requiere un numero de poliza valido");
        RuleFor(x => x.idPlan).NotEmpty().NotNull().WithMessage("Selecione un plan valido");
        RuleFor(x => x.plaque).NotEmpty().NotNull().WithMessage("Ingrese la placa del vehiculo");
        RuleFor(x => x.vehicleModel).NotEmpty().NotNull().WithMessage("Ingrese el modelo del vehiculo");
        RuleFor(x => x.whitInspection).Must(x => x == true || x == false).WithMessage("Indique si el vehiculo cuenta con inspecion ");


        RuleFor(x => x.customerName).NotEmpty().NotNull().WithMessage("Nombre de cliente requerido");
        RuleFor(x => x.identification).NotEmpty().NotNull().WithMessage("Numero identificacion requerido");
        RuleFor(x => x.City).NotEmpty().NotNull().WithMessage("Ciudad es requerida");
        RuleFor(x => x.Address).NotEmpty().NotNull().WithMessage("Se requiere una direccion");


    }



}