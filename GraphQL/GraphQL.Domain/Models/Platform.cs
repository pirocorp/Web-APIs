namespace GraphQL.Domain.Models;

using System;
using System.Collections.Generic;

using GraphQL.Domain.Common;
using GraphQL.Domain.Exceptions;
/// <summary>
/// Represents any software or service that has a command line interface.
/// </summary>
public class Platform : Entity<Guid>
{
    private Platform() // Used by EF Core
    {
        this.Name = string.Empty;
        this.LicenseKey = string.Empty;
    }

    internal Platform(string name, string? licenseKey)
    {
        Validate(name);

        this.Id = Guid.NewGuid();
        this.Name = name;
        this.LicenseKey = licenseKey;
    }

    /// <summary>
    /// Name of the Platform
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// License Key of the platform
    /// </summary>
    public string? LicenseKey { get; private set; }

    /// <summary>
    /// Commands that can be executed on this platform
    /// </summary>
    public ICollection<Command> Commands { get; private set; } = new List<Command>();

    /// <summary>
    /// Change name of the platform
    /// </summary>
    /// <param name="name">New platform name</param>
    public void ChangeName(string name)
    {
        Validate(name);

        this.Name = name;
    }

    /// <summary>
    /// Add Command to the platform
    /// </summary>
    /// <param name="command"></param>
    public void AddCommand(Command command)
    {
        this.Commands.Add(command);
    }

    private static void Validate(string name)
    {
        Guard.AgainstEmptyString<InvalidPlatformException>(name, nameof(Name));
    }
}
