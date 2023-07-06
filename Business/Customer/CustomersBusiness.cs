using Business.Customer.Query;
using Entities;
using Entities.DTO;
using FluentValidation;
using MediatR;

namespace Business.Customer;

public class CustomersBusiness
{
    private readonly IValidator<CustomerCommand> _validator;
    private readonly IMediator _sender;

    public CustomersBusiness(
        IValidator<CustomerCommand> validator,
        IMediator sender
        )
    {
        _validator = validator;
        _sender = sender;
    }


    public async Task<ResponseBase<dynamic>> GetFind(string id)
    {
        if (id is null)
        {
            return new ResponseBase<dynamic>(message: "Id cliente no puede ser null");
        }
        var request = new GetCustomerByIdRequest()
        {
            Id = id
        };
        var customer = await _sender.Send(request);
        return new ResponseBase<dynamic>(message: "Ok", data: customer);
    }

    public async Task<ResponseBase<dynamic>> GetList()
    {
        var response = new ResponseBase<List<string>>();
        var result = await _sender.Send(new GetCustomersRequest());
        return new ResponseBase<dynamic>(data: result, message: "Ok");
    }

    public async Task<ResponseBase<List<string>>> Create(CustomerCommand custo)
    {
        var response = new ResponseBase<List<string>>();
        var result = await _validator.ValidateAsync(custo);
        if (!result.IsValid)
        {
            response.Data = new List<string>();
            response.Code = 404;
            response.Message = "Error En informacion suministrada";
            result.Errors.ForEach(error =>
            {
                response.Data.Add(error.ErrorMessage);
            });

            return response;
        }
        await _sender.Send(custo);

        return response;

    }
}