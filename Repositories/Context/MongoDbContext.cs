using Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Entities.Interface.Repositories;
using Entities.Models;
using System.Linq.Expressions;

namespace Repository.Context;

public class MongoDbContext<T> 
{
    private readonly IMongoCollection<T> _booksCollection;
    public MongoDbContext(IOptions<StoreDatabaseSettings> storeDataBaseSetting)
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
    
    public IMongoCollection<T> Collection { get { return _booksCollection; } }

}