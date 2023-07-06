using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Entities;
using Entities.Interface.Repositories;
using Repository.Context;
using Repository.Base;

namespace Repository;

public static class StartUp
{
    public static void AddMongoClient(this IServiceCollection service , IConfiguration config)
    {
        service.Configure<StoreDatabaseSettings>(
            config.GetSection("StoreDatabaseSettings")
        );
        service.AddTransient(typeof(MongoDbContext<>));
        service.AddScoped(typeof(IMongoRepositoryBase<>), typeof(MongoBaseRepository<>));

        service.AddScoped<ICustomerRepository, CustomerRepository>();
    }
    
}