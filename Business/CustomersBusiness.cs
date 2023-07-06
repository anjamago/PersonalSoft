using Entities;
using Entities.Interface.Repositories;
using Entities.Models;
using Entities.Interface.Business;
namespace Business;

public class CustomersBusiness//: ICustomersBusiness
{
    private readonly IDbMongo<Customers> _customer;

    public CustomersBusiness(IDbMongo<Customers> customer)
    {
        _customer = customer;
    }


    public async Task<ResponseBase<dynamic>>  GetFind(string id)
    {
        var list  =  await _customer.GetAsync();

        return new ResponseBase<dynamic>(data: list);
    }

    public async Task Create(Customers custo)
    {
        // validar si existe el cliente 
       await _customer.CreateAsync(custo);
        //validar que no tenga una poliza
        // registrar cliente 
    }
}