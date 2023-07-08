using Business.Customer;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Business;

public static class Business
{

    public static void AddBusiness(this IServiceCollection service)
    {

        service.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        service.AddValidatorsFromAssembly(typeof(ApplicationAssemblyReference).Assembly);

        service.AddScoped<ICustomersBusiness,CustomersBusiness>();
        service.AddScoped<IPlansBusiness,PlansBusiness>();
        service.AddScoped<IPolicyBusiness,PolicyBusiness>();


    }

}