namespace GraphQL.Domain.Factories;

using GraphQL.Domain.Models;

public interface IPlatformFactory : IFactory<Platform>
{
    Platform Build(string name, string? licenseKey = null);

    IPlatformFactory WithName(string name);

    IPlatformFactory WithLicenseKey(string? licenseKey);
}
