using Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository.Base;
using Repository.Context;
using Repository.Interface;

namespace Repository;

public static class Repositories
{
    public static void AddMongoClient(this IServiceCollection service, IConfiguration config)
    {
        service.Configure<StoreDatabaseSettings>(
            config.GetSection("StoreDatabaseSettings")
        );
        service.AddTransient(typeof(MongoDbContext<>));
        service.AddScoped(typeof(IMongoRepositoryBase<>), typeof(MongoBaseRepository<>));

        service.AddScoped<ICustomerRepository, CustomerRepository>();
        service.AddScoped<IPlanRepository, PlanRepository>();
    }

}