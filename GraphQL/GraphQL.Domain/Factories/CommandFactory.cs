namespace GraphQL.Domain.Factories;

using GraphQL.Domain.Models;

public class CommandFactory : ICommandFactory
{
    private string commandDescription = string.Empty;
    private string commandExpression = string.Empty;
    private Platform commandPlatform = default!;

    public Command Build()
        => this.Build(this.commandDescription, this.commandExpression, this.commandPlatform;

    public Command Build(string description, string commandLine, Platform platform)
        => this
            .WithDescription(description)
            .WithCommandLine(commandLine)
            .WithPlatform(platform)
            .Build();

    public ICommandFactory WithDescription(string description)
    {
        this.commandDescription = description;

        return this;
    }

    public ICommandFactory WithCommandLine(string commandLine)
    {
        this.commandExpression = commandLine;

        return this;
    }

    public ICommandFactory WithPlatform(Platform platform)
    {
        this.commandPlatform = platform;

        return this;
    }
}
