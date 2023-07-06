using MongoDB.Driver;

namespace Entities.Interface.Repositories;

public interface IMongoRepositoryBase<TEntity> where TEntity : class
{
    
    Task AddAsync(TEntity entity);
    Task<TEntity> GetAsync(int Id);
    Task<List<TEntity>> GetListAsync();
    Task<TEntity> GetByObjectIdAsync(string Id);
    Task<List<TEntity>> GetListFilterAsync(FilterDefinition<TEntity> filter);

}