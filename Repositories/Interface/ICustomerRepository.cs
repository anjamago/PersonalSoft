using Entities.Models;

namespace Repository.Interface
{
    public interface ICustomerRepository
    {
        Task<bool> IsIdentificationUniquedAsync(int identification);
        Task AddCustomerAsync(Customers customer);
        Task<List<Customers>> GetFindId(string id);
        Task<List<Customers>> GetList();
    }
}
