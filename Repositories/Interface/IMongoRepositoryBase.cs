using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Repository.Interface;

public interface IMongoRepositoryBase<TEntity> where TEntity : class
{

    Task AddAsync(TEntity entity);
    Task<TEntity> GetAsync(int Id);
    Task<List<TEntity>> GetListAsync();
    Task<TEntity> GetByObjectIdAsync(string Id);
    Task<List<TEntity>> GetListFilterAsync(FilterDefinition<TEntity> filter);
    IMongoQueryable<TEntity> Queryable();

}