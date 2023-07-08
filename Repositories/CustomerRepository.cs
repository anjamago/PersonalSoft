using Entities.Models;
using MongoDB.Driver;
using Repository.Interface;

namespace Repository
{
    public sealed class CustomerRepository : ICustomerRepository
    {
        private readonly IMongoRepositoryBase<Customers> _repository;
        public CustomerRepository(IMongoRepositoryBase<Customers> repository)
         => _repository = repository;


        public async Task<bool> IsIdentificationUniquedAsync(string identification)
        {
            var filter = Builders<Customers>.Filter.Eq(x => x.Identification, identification);
            var exist = await _repository.GetListFilterAsync(filter);
            return exist.Any();
        }

        public async Task AddCustomerAsync(Customers customer)
        {
            await _repository.AddAsync(customer);

        }

        public async Task<List<Customers>> GetFindId(string id)
        {
            var filter = Builders<Customers>.Filter.Eq(x => x.Id, id);
            return await _repository.GetListFilterAsync(filter);
        }
        public async Task<List<Customers>> GetList()
        => await _repository.GetListAsync();


    }
}
