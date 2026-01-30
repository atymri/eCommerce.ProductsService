using eCommerce.DataAccessLayer.DatabaseContext;
using eCommerce.DataAccessLayer.Repositories;
using eCommerce.DataAccessLayer.RepositoryContracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.DataAccessLayer;
public static class DependencyInjection
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default")!
            .Replace("$MYSQL_HOST", Environment.GetEnvironmentVariable("MYSQL_HOST"))
            .Replace("$MYSQL_PASS", Environment.GetEnvironmentVariable("MYSQL_PASS"))
            .Replace("$MYSQL_DB",   Environment.GetEnvironmentVariable("MYSQL_DB"))
            .Replace("$MYSQL_USER", Environment.GetEnvironmentVariable("MYSQL_USER"))
            .Replace("$MYSQL_PORT", Environment.GetEnvironmentVariable("MYSQL_PORT"));

        services.AddDbContext<ApplicationDbContext>(options => options.UseMySQL(connectionString));
        services.AddScoped<IProductsRepository, ProductRepository>();

        return services;
    }
}

