namespace Entities.Interface.Business;

public interface ICustomersBusiness
{
    public Task<ResponseBase<dynamic>> GetFind(string id);
    //public Task Create(Customers custo);
}