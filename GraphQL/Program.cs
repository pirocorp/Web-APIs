namespace GraphQL;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        ConfigureConfiguration(builder.Configuration);
        ConfigureServices(builder.Services);

        var app = builder.Build();

        ConfigureMiddleware(app);
        ConfigureEndpoints(app);

        app.Run();
    }

    private static void ConfigureConfiguration(IConfiguration config)
    { }

    private static void ConfigureServices(IServiceCollection services)
    { }

    private static void ConfigureMiddleware(WebApplication app)
    { }

    private static void ConfigureEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("/", () => "Hello World!");
    }
}
