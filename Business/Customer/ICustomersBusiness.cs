using Entities;
using Entities.DTO;

namespace Business.Customer
{
    public interface ICustomersBusiness
    {
        Task<ResponseBase<List<string>>> Create(CustomerCommand custo);
        Task<ResponseBase<dynamic>> GetFind(string id);
        Task<ResponseBase<dynamic>> GetList();
    }
}