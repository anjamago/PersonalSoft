using Business.Create;
using Business.Plans.Query;
using Entities;
using Entities.DTO;
using FluentValidation;
using MediatR;

namespace Business;

public class PlansBusiness
{
    public readonly ISender _sender;
    public readonly IValidator<PlansCommand> _validate;
    public PlansBusiness(ISender sender, IValidator<PlansCommand> validation)
    {
        _sender = sender;
        _validate = validation;
    }

    public async Task<ResponseBase<List<Plan>>> GetList()
    {
        var result = await _sender.Send(new GetPlansList());
        return new ResponseBase<List<Plan>>(data: result);
    }

    public async Task<ResponseBase<List<string>>> Create(PlansCommand plan)
    {
        var result = _validate.Validate(plan);
        var response = new ResponseBase<List<string>>();
        if (!result.IsValid)
        {
            response.Data = new List<string>();
            response.Code = 404;
            response.Message = "Error En informacion suministrada";

            response.Data = result.Errors.Select(s => s.ErrorMessage).ToList();

            return response;
        }
        await _sender.Send(plan);

        return response;

    }
}