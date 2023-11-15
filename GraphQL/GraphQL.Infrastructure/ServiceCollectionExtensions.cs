namespace GraphQL.Infrastructure;

using global::GraphQL.Infrastructure.GraphQL.Platforms;
using HotChocolate.Data;

using Infrastructure.GraphQL;
using Infrastructure.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using static Common.InfrastructureConstants;

/// <summary>
/// Service Collection Extension Methods
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Register Domain Services in Service Collection
    /// </summary>
    /// <param name="serviceCollection">Service Collection</param>
    /// <param name="configuration">Application Configuration</param>
    /// <returns>Service Collection</returns>
    public static IServiceCollection RegisterInfrastructureServices(
        this IServiceCollection serviceCollection, 
        IConfiguration configuration)
    {
        serviceCollection
            .AddPooledDbContextFactory<GraphQlDbContext>(options => options.UseSqlServer(
                configuration.GetConnectionString(DEFAULT_CONNECTION),
                b => b.MigrationsAssembly(typeof(GraphQlDbContext).Assembly.FullName)));

        serviceCollection
            .AddGraphQLServer()
            // The Hot Chocolate Resolver Compiler will then take care of correctly injecting your scoped DbContext instance
            // into your resolvers and also ensuring that the resolvers using it are never run in parallel.
            .RegisterDbContext<GraphQlDbContext>(DbContextKind.Pooled)  // You can also specify a DbContextKind as argument to the RegisterDbContext<T> method,
                                                                        // to change how the DbContext should be injected.
            .AddQueryType<Query>()
            .AddType<PlatformType>()
            //.AddType<CommandType>()
            .AddProjections();

        return serviceCollection;
    }
}
