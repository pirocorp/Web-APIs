namespace GraphQL.Domain.Factories;

using GraphQL.Domain.Models;

public interface ICommandFactory : IFactory<Command>
{
    Command Build(string description, string commandLine, Platform platform);

    ICommandFactory WithDescription(string description);
    
    ICommandFactory WithCommandLine(string commandLine);

    ICommandFactory WithPlatform(Platform platform);
}
