namespace GraphQL.Domain;

using GraphQL.Domain.Factories;

using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Service Collection Extension Methods
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Register Domain Services in Service Collection
    /// </summary>
    /// <param name="serviceCollection">Service Collection</param>
    /// <returns>Service Collection</returns>
    public static IServiceCollection RegisterDomainServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IPlatformFactory, PlatformFactory>();
        serviceCollection.AddTransient<ICommandFactory, CommandFactory>();

        return serviceCollection;
    }
}
