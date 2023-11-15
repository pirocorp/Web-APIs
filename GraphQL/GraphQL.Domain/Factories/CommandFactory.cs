namespace GraphQL.Domain.Factories;

using GraphQL.Domain.Models;

/// <summary>
/// Factory for producing <see cref="Command"/> objects
/// </summary>
public class CommandFactory : ICommandFactory
{
    private string commandDescription = string.Empty;
    private string commandExpression = string.Empty;
    private Platform commandPlatform = default!;

    /// <summary>
    /// Build a <see cref="Command"/> object
    /// </summary>
    /// <returns>New <see cref="Command"/> instance</returns>
    public Command Build()
        => this.Build(this.commandDescription, this.commandExpression, this.commandPlatform);

    /// <inheritdoc />
    public Command Build(string description, string commandLine, Platform platform)
        => this
            .WithDescription(description)
            .WithCommandLine(commandLine)
            .WithPlatform(platform)
            .Build();

    /// <inheritdoc />
    public ICommandFactory WithDescription(string description)
    {
        this.commandDescription = description;

        return this;
    }

    /// <inheritdoc />
    public ICommandFactory WithCommandLine(string commandLine)
    {
        this.commandExpression = commandLine;

        return this;
    }

    /// <inheritdoc />
    public ICommandFactory WithPlatform(Platform platform)
    {
        this.commandPlatform = platform;

        return this;
    }
}
