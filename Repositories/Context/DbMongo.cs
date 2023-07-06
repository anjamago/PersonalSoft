using Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Entities.Interface.Repositories;
using Entities.Models;


namespace Repository.Context;

public class DbMongo<T>: IDbMongo<T> where  T : class
{
    private readonly IMongoCollection<T> _booksCollection;
    public DbMongo(IOptions<StoreDatabaseSettings> storeDataBaseSetting)
    {
        var mongoClinte = new MongoClient(
            storeDataBaseSetting.Value.ConnectionString
        );
        var mongoDatabase = mongoClinte.GetDatabase(storeDataBaseSetting.Value.DatabaseName);

        var collection = typeof(T).Name;
        
        _booksCollection = mongoDatabase.GetCollection<T>(
            collection.ToLower()
            );
    }
    
    public async Task<List<T>> GetAsync() =>
        await _booksCollection.Find(_ => true).ToListAsync();

    public async Task<T> GetAsync(string id) =>
        await _booksCollection.Find(x => x == id).FirstOrDefaultAsync();

    public async Task CreateAsync(T newBook) =>
        await _booksCollection.InsertOneAsync(newBook);

    public async Task UpdateAsync(string id, T updatedBook) =>
        await _booksCollection.ReplaceOneAsync(x => x == id, updatedBook);

    public async Task RemoveAsync(string id) =>
        await _booksCollection.DeleteOneAsync(x => x == id);
}