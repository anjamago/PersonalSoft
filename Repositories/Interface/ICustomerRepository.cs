using Entities.Models;

namespace Repository.Interface
{
    public interface ICustomerRepository
    {
        Task<bool> IsIdentificationUniquedAsync(string identification);
        Task AddCustomerAsync(Customers customer);
        Task<List<Customers>> GetFindId(string id);
        Task<List<Customers>> GetList();
    }
}
