using Entities.Models;
using Repository.Interface;

namespace Repository
{
    public class PlanRepository : IPlanRepository
    {
        private readonly IMongoRepositoryBase<Plans> _repository;
        public PlanRepository(IMongoRepositoryBase<Plans> repository)
        {
            _repository = repository;
        }

        public async Task Create(Plans plan)
        {
            await _repository.AddAsync(plan);
        }

        public async Task<List<Plans>> GetList()
        {
            return await _repository.GetListAsync();
        }
    }
}
