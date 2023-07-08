using Business.Create;
using Entities;
using FluentValidation;
using MediatR;

namespace Business;

public class PolicyBusiness : IPolicyBusiness
{
    private readonly IValidator<CreatePolicyCommand> _validator;
    private readonly ISender _sender;
    private ResponseBase<List<string>> response = new(data: new List<string>());

    public PolicyBusiness(IValidator<CreatePolicyCommand> validator, ISender sender)
    {
        _validator = validator;
        _sender = sender;
    }

    public async Task<ResponseBase<List<string>>> Create(CreatePolicyCommand policy)
    {
        var result = _validator.Validate(policy);
        DateTime fechaActual = DateTime.Now;

        if (fechaActual >= DateTime.Parse(policy.StartDate) && fechaActual <= DateTime.Parse(policy.EndDate))
        {
            ResulErrors(error: "Los rangos de fecha para la poliza no es vigente");
        }


        if (!result.IsValid)
        {
            var errors = result.Errors.Select(s => s.ErrorMessage).ToList();
            ResulErrors(errors: errors);
            return response;
        }

       var senderRespon =  await _sender.Send(policy);

        return !senderRespon.Any() ? 
             new ResponseBase<List<string>>() :
             new ResponseBase<List<string>>(
                code: System.Net.HttpStatusCode.BadRequest,
                data:senderRespon,
                message: "Error al intentar crear la solicitud"

             );
    }


    private void ResulErrors(string error = "", List<string> errors = null)
    {

        response.Code = 404;
        response.Message = "Error En informacion suministrada";
        if (!string.IsNullOrEmpty(error))
        {
            response.Data.Add(error);
        }
        if (errors.Count > 0)
        {
            response.Data.AddRange(errors);
        }

    }

}