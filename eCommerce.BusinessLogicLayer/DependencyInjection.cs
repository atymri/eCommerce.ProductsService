using eCommerce.BusinessLogicLayer.DTOs.MappingProfiles;
using eCommerce.BusinessLogicLayer.MessageBroker;
using eCommerce.BusinessLogicLayer.MessageBroker.Abstractions;
using eCommerce.BusinessLogicLayer.ServiceContracts;
using eCommerce.BusinessLogicLayer.Services;
using eCommerce.BusinessLogicLayer.Validators;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.BusinessLogicLayer;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
    {
        //services.AddFluentValidationAutoValidation();  // in case of minimal api we cant use this method.
        services.AddValidatorsFromAssemblyContaining<ProductAddRequestValidator>();

        services.AddAutoMapper(typeof(ProductMappingProfile).Assembly);
        services.AddScoped<IProductsService, ProductsService>();

        services.AddSingleton<IConnector, Connector>();
        services.AddTransient<IPublisher, Publisher>();

        return services;
    }
}

