namespace GraphQL.WebApi;

using System.Threading.Tasks;

using GraphQL.Domain;
using GraphQL.Infrastructure;
using GraphQL.Server.Ui.Voyager;
using GraphQL.WebApi.Exceptions;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using static Infrastructure.Common.InfrastructureConstants;

/// <summary>
/// The application entry Point class
/// </summary>
public class Program
{
    private static IConfiguration? configuration;

    /// <summary>
    /// The application entry point
    /// </summary>
    public static Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        ConfigureConfiguration(builder.Configuration);
        ConfigureServices(builder.Services);

        var app = builder.Build();

        ConfigureMiddleware(app);
        ConfigureEndpoints(app);

        return app.RunAsync();
    }

    /// <summary>
    /// Application Configuration Object
    /// </summary>
    public static IConfiguration Configuration
    {
        get => configuration ?? throw new NullConfigurationException();
        set => configuration = value ?? throw new NullConfigurationException();
    }

    private static void ConfigureConfiguration(IConfiguration config)
    {
        Configuration = config;
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services
            .RegisterDomainServices()
            .RegisterInfrastructureServices(Configuration);
    }

    private static void ConfigureMiddleware(WebApplication app)
    { }

    private static void ConfigureEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGraphQL(GRAPH_QL_ENDPOINT);

        app.MapGraphQLVoyager(
            VOYAGER_ENDPOINT,
            new VoyagerOptions()
            {
                GraphQLEndPoint = GRAPH_QL_ENDPOINT
            });
    }
}
