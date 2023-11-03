namespace GraphQL.Domain.Models;

using System;

using GraphQL.Domain.Common;
using GraphQL.Domain.Exceptions;

public class Platform
{
    private Platform() // Used by EF Core
    {
        this.Name = string.Empty;
        this.LicenseKey = string.Empty;
    }

    internal Platform(string name, string? licenseKey)
    {
        this.Validate(name);

        this.Id = Guid.NewGuid();
        this.Name = name;
        this.LicenseKey = licenseKey;
    }

    public Guid Id { get; private set; }

    public string Name { get; private init; }

    public string? LicenseKey { get; private set; }

    private void Validate(string name)
    {
        Guard.AgainstEmptyString<InvalidPlatformException>(name, nameof(this.Name));
    }
}
