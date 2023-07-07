using Entities.Models;
using Repository.Interface;

namespace Repository
{
    public class ToppingRepository
    {
        private readonly IMongoRepositoryBase<Topping> _repository;

        public ToppingRepository(IMongoRepositoryBase<Topping> repository)
        {
            _repository = repository;
        }

        public async Task<List<Topping>> GetList() => await _repository.GetListAsync();
    }
}
