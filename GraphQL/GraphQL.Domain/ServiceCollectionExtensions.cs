namespace GraphQL.Domain;

using GraphQL.Domain.Factories;

using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterDomainServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IPlatformFactory, PlatformFactory>();

        return serviceCollection;
    }
}
