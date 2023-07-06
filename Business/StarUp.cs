using Microsoft.Extensions.DependencyInjection;

namespace Business;

public static class StarUp
{

    public static void AddBusiness(this IServiceCollection service)
    {
        service.AddScoped<CustomersBusiness>();
        

    }
    
}