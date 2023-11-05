namespace GraphQL.WebApi;

using System.Threading.Tasks;
using GraphQL.Domain;
using GraphQL.Infrastructure;
using GraphQL.WebApi.Exceptions;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public class Program
{
    private static IConfiguration? configuration;

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
        app.MapGet("/", () => "Hello World!");
    }
}
