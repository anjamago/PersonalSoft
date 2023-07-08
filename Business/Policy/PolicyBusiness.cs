using Business.Create;
using Business.Policy.Create;
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
        var result =  _validator.Validate(policy) ;
       
        if (!result.IsValid)
        {
            var errors = result.Errors.Select(s => s.ErrorMessage).ToList();
            ResulErrors(errors: errors);
            return response;
        }

        var isvalid = !IsPolicyValid(DateTime.Parse(policy.StartDate), DateTime.Parse(policy.EndDate));

        if(isvalid) return response;

        var senderRespon =  await _sender.Send(policy);

        return !senderRespon.Any() ? 
             new ResponseBase<List<string>>() :
             new ResponseBase<List<string>>(
                code: System.Net.HttpStatusCode.BadRequest,
                data:senderRespon,
                message: "Error al intentar crear la solicitud"

             );
    }
    private bool IsPolicyValid(DateTime StartDate, DateTime EndDate)
    {
        int days = 364;
        bool isValid = true;
        DateTime effectiveDate = DateTime.Now.AddYears(1).AddDays(-1);
        DateTime now = DateTime.Now.Date;
        TimeSpan diferencia = EndDate- StartDate;

        if (diferencia.TotalDays -1 < days || diferencia.TotalDays-1 > days)
        {
            ResulErrors(error: "Los rangos de fecha para la poliza no son una vigente valida");
            isValid = false;
        }

        if (now < StartDate.Date)
        {
            ResulErrors(error: "La póliza aún no es vigente. ");
            isValid = false;
        }
        if(effectiveDate.Date > EndDate.Date || effectiveDate.Date < EndDate.Date)
        {
            ResulErrors(error: "La póliza aún no es vigente. ");
            isValid = false;
        }
        return isValid;

    }

    private void ResulErrors(string error = "", List<string> errors = null)
    {

        response.Code = 404;
        response.Message = "Error En informacion suministrada";
        if (!string.IsNullOrEmpty(error))
        {
            response.Data.Add(error);
        }
        if (errors?.Count > 0)
        {
            response.Data.AddRange(errors);
        }

    }

}