using GraphQL.Infrastructure.Data;

namespace GraphQL.Infrastructure;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterInfrastructureServices(
        this IServiceCollection serviceCollection, 
        IConfiguration configuration)
    {
        serviceCollection
            .AddDbContext<GraphQlDbContext>(options => options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(GraphQlDbContext).Assembly.FullName)));

        return serviceCollection;
    }
}
