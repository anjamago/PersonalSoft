using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Repository.Context;
using Repository.Interface;

namespace Repository.Base
{

    public class MongoBaseRepository<TEntity> : IMongoRepositoryBase<TEntity> where TEntity : class, new()
    {
        private readonly MongoDbContext<TEntity> _context;

        public MongoBaseRepository(MongoDbContext<TEntity> context)
        {
            _context = context;
        }

        public async Task AddAsync(TEntity entity)
        {
            await _context.Collection.InsertOneAsync(entity);

        }

        public async Task<TEntity> GetAsync(int Id)
        {
            var filter = Builders<TEntity>.Filter.Eq("Id", Id);

            return await _context.Collection
                .Find(filter)
                .FirstOrDefaultAsync();
        }

        public async Task<TEntity> GetByObjectIdAsync(string Id)
        {
            var filter = Builders<TEntity>.Filter.Eq("_id", ObjectId.Parse(Id));

            return await _context.Collection
                .Find(filter)
                .FirstOrDefaultAsync();
        }

        public async Task<List<TEntity>> GetListAsync()
        {
            return await _context.Collection
                .Find(new BsonDocument())
                .ToListAsync();
        }

        public async Task<List<TEntity>> GetListFilterAsync(FilterDefinition<TEntity> filter)
        {
            return await _context.Collection
                .Find(filter).ToListAsync();
        }


        public IMongoQueryable<TEntity> Queryable()
            => _context.Collection.AsQueryable();
    }
}
