namespace GraphQL.Domain.Models;

using System;

using GraphQL.Domain.Common;
using GraphQL.Domain.Exceptions;

/// <summary>
/// Command that can be executed on a given platform
/// </summary>
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

    /// <summary>
    /// The command itself
    /// </summary>
    public string CommandLine { get; private set; }

    /// <summary>
    /// Description of the command
    /// </summary>
    public string Description { get; private set; }

    /// <summary>
    /// Platform on which command can be executed
    /// </summary>
    public Platform Platform { get; private set; }

    /// <summary>
    /// Change the command line
    /// </summary>
    /// <param name="command">The new command line</param>
    public void ChangeCommand(string command)
    {
        Guard.AgainstEmptyString<InvalidCommandException>(command, nameof(this.CommandLine));

        this.CommandLine = command;
    }

    /// <summary>
    /// Change the description of the command
    /// </summary>
    /// <param name="description"></param>
    public void UpdateDescription(string description)
    {
        Guard.AgainstEmptyString<InvalidCommandException>(description, nameof(this.Description));

        this.Description = description;
    }

    /// <summary>
    /// Move command to the new platform
    /// </summary>
    /// <param name="platform"></param>
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
