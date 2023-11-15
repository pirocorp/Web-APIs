namespace GraphQL.Domain.Factories;

using GraphQL.Domain.Models;

/// <summary>
/// Interface for factory producing <see cref="Platform"/>
/// </summary>
public interface IPlatformFactory : IFactory<Platform>
{
    /// <summary>
    /// Build a <see cref="Platform"/> object
    /// </summary>
    /// <returns>New <see cref="Platform"/> instance</returns>
    Platform Build(string name, string? licenseKey = null);

    /// <summary>
    /// Add Name
    /// </summary>
    /// <param name="name">Name</param>
    IPlatformFactory WithName(string name);

    /// <summary>
    /// Add License Key
    /// </summary>
    /// <param name="licenseKey">License Key</param>
    IPlatformFactory WithLicenseKey(string? licenseKey);
}
