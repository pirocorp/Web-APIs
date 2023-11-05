namespace GraphQL.Domain.Factories;

using GraphQL.Domain.Models;

public class PlatformFactory : IPlatformFactory
{
    private string platformName = string.Empty;
    private string? platformLicenseKey = null;

    public Platform Build()
        => new (this.platformName, this.platformLicenseKey);

    public Platform Build(string name, string? licenseKey = null)
        => this
            .WithName(name)
            .WithLicenseKey(licenseKey)
            .Build();

    public IPlatformFactory WithName(string name)
    {
        this.platformName = name;

        return this;
    }

    public IPlatformFactory WithLicenseKey(string? licenseKey)
    {
        this.platformLicenseKey = licenseKey;

        return this;
    }
}
