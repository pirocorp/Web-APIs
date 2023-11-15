namespace GraphQL.Domain.Factories;

using GraphQL.Domain.Models;

/// <summary>
/// Factory for producing <see cref="Platform"/> objects
/// </summary>
public class PlatformFactory : IPlatformFactory
{
    private string platformName = string.Empty;
    private string? platformLicenseKey = null;

    /// <summary>
    /// Build a <see cref="Platform"/> object
    /// </summary>
    /// <returns>New <see cref="Platform"/> instance</returns>
    public Platform Build()
        => new (this.platformName, this.platformLicenseKey);

    /// <inheritdoc />
    public Platform Build(string name, string? licenseKey = null)
        => this
            .WithName(name)
            .WithLicenseKey(licenseKey)
            .Build();

    /// <inheritdoc />
    public IPlatformFactory WithName(string name)
    {
        this.platformName = name;

        return this;
    }

    /// <inheritdoc />
    public IPlatformFactory WithLicenseKey(string? licenseKey)
    {
        this.platformLicenseKey = licenseKey;

        return this;
    }
}
