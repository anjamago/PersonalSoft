using Business.Create;
using Business.Policy.Create;
using Business.Query;
using Entities;
using Entities.DTO;
using FluentValidation;
using MediatR;

namespace Business;

public class PolicyBusiness : IPolicyBusiness
{
    private readonly IValidator<CreatePolicyCommand> _validator;
    private readonly IValidator<CreatePolicyIdCommand> _validatorId;
    private readonly ISender _sender;

    private ResponseBase<List<string>> response = new(data: new List<string>());

    public PolicyBusiness(IValidator<CreatePolicyCommand> validator, IValidator<CreatePolicyIdCommand> validatorid,ISender sender)
    {
        _validator = validator;
        _sender = sender;
        _validatorId = validatorid;

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

    public async Task<ResponseBase<List<string>>> CreateIdCustomer(CreatePolicyIdCommand policy)
    {
        var result = _validatorId.Validate(policy);

        if (!result.IsValid)
        {
            var errors = result.Errors.Select(s => s.ErrorMessage).ToList();
            ResulErrors(errors: errors);
            return response;
        }

        var isvalid = !IsPolicyValid(DateTime.Parse(policy.StartDate), DateTime.Parse(policy.EndDate));

        if (isvalid) return response;
        var modelSender = new CreatePolicyCommand(
                policyNumber: policy.policyNumber,
                idPlan : policy.idPlan,
                vehicleModel:policy.vehicleModel,
                whitInspection:policy.whitInspection,
                StartDate: policy.StartDate,
                EndDate:policy.EndDate,
                IdCustomer:policy.IdCustomer!,
                plaque:policy.plaque,
                City: "",
                Address:"",
                customerName:"",
                identification:""
            );
        var senderRespon = await _sender.Send(modelSender);

        return !senderRespon.Any() ?
             new ResponseBase<List<string>>() :
             new ResponseBase<List<string>>(
                code: System.Net.HttpStatusCode.BadRequest,
                data: senderRespon,
                message: "Error al intentar crear la solicitud"

             );
    }

    public async Task<ResponseBase<PolicyCustomerDto>> Find(string policNumberOrPlaque)
    {
        var modelSender = new FindPolicyOrPlaqueCommand()
        {
            policyOrPlaque = policNumberOrPlaque
        };

        var result = await _sender.Send(modelSender);
        
        return  new ResponseBase<PolicyCustomerDto>(data: result);
    }

    public async Task<ResponseBase<List<PolicyCustomerDto>>> GetAll()
    {
        var result = await _sender.Send(new GetAllCommand());
        return new ResponseBase<List<PolicyCustomerDto>>(data: result);
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
            ResulErrors(error: "La p�liza a�n no es vigente. ");
            isValid = false;
        }
        if(effectiveDate.Date > EndDate.Date || effectiveDate.Date < EndDate.Date)
        {
            ResulErrors(error: "La p�liza a�n no es vigente. ");
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