using eCommerce.DataAccessLayer.DatabaseContext;
using eCommerce.DataAccessLayer.Repositories;
using eCommerce.DataAccessLayer.RepositoryContracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.DataAccessLayer;
public static class DependencyInjection
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default")
            ?? throw new KeyNotFoundException("Mysql connection string is missing!");

        services.AddDbContext<ApplicationDbContext>(options => options.UseMySQL(connectionString));
        services.AddScoped<IProductsRepository, ProductRepository>();

        return services;
    }
}

