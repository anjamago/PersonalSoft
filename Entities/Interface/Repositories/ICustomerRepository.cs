using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Interface.Repositories
{
    public  interface ICustomerRepository
    {
        Task<bool> IsIdentificationUniquedAsync(int identification);
        Task AddCustomerAsync(Customers customer);
        Task<List<Customers>> GetFindId(string id);
        Task<List<Customers>> GetList();
    }
}
