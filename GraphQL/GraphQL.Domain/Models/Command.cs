namespace GraphQL.Domain.Models;

using System;

using GraphQL.Domain.Common;
using GraphQL.Domain.Exceptions;

public class Command : Entity<Guid>
{
    private Command() // Used by EF Core
    {
        this.CommandLine = string.Empty;
        this.Description = string.Empty;
        this.Platform =  default!;
    }

    internal Command(string description, string commandLine, Platform platform)
    {
        this.Id = Guid.NewGuid();

        Validate(description, commandLine);

        this.Description = description;
        this.CommandLine = commandLine;
        this.Platform = platform;
    }

    public string CommandLine { get; private set; }

    public string Description { get; private set; }

    public Platform Platform { get; private set; }

    public void ChangeCommand(string command)
    {
        Guard.AgainstEmptyString<InvalidCommandException>(command, nameof(this.CommandLine));

        this.CommandLine = command;
    }

    public void UpdateDescription(string description)
    {
        Guard.AgainstEmptyString<InvalidCommandException>(description, nameof(this.Description));

        this.Description = description;
    }

    public void SwitchPlatform(Platform platform)
    {
        this.Platform = platform;
    }

    private static void Validate(string description, string commandLine)
    {
        Guard.AgainstEmptyString<InvalidCommandException>(description, nameof(Description));
        Guard.AgainstEmptyString<InvalidCommandException>(commandLine, nameof(CommandLine));
    }
}
