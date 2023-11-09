namespace GraphQL.Domain.Models;

using System;
using System.Collections.Generic;
using System.Linq;

using GraphQL.Domain.Common;
using GraphQL.Domain.Exceptions;

public class Platform : Entity<Guid>
{
    private readonly HashSet<Command> commands = new HashSet<Command>();

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

    public string Name { get; private set; }

    public string? LicenseKey { get; private set; }

    public IReadOnlyCollection<Command> Commands => this.commands.ToList().AsReadOnly();

    public void ChangeName(string name)
    {
        Validate(name);

        this.Name = name;
    }

    public void AddCommand(Command command)
    {
        this.commands.Add(command);
    }

    private static void Validate(string name)
    {
        Guard.AgainstEmptyString<InvalidPlatformException>(name, nameof(Name));
    }
}
