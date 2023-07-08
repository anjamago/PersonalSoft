using Business.Create;
using FluentValidation;

namespace Business.Plans.Create
{
    public sealed class CreatePlanCommandValidate : AbstractValidator<PlansCommand>
    {
        public CreatePlanCommandValidate()
        {
            RuleFor(x => x.ToppingsId).Must(IsToppingValid).WithMessage("Debe ingresar almenos una cobertura");
            RuleFor(x => x.TotalCoverage).NotNull().NotEmpty().WithMessage("Debe ingresar un moto total de cobertura");
        }

        private bool IsToppingValid(List<string> toppingIds) => toppingIds.Any();

    }
}
