namespace GraphQL.Domain.Factories;

using GraphQL.Domain.Models;

/// <summary>
/// Interface for factory producing <see cref="Command"/>
/// </summary>
public interface ICommandFactory : IFactory<Command>
{
    /// <summary>
    /// Build a <see cref="Command"/> object
    /// </summary>
    /// <returns>New <see cref="Command"/> instance</returns>
    Command Build(string description, string commandLine, Platform platform);

    /// <summary>
    /// Add Description
    /// </summary>
    /// <param name="description">Description</param>
    ICommandFactory WithDescription(string description);
    
    /// <summary>
    /// Add Command Line
    /// </summary>
    /// <param name="commandLine">Command Line</param>
    ICommandFactory WithCommandLine(string commandLine);

    /// <summary>
    /// Add Platform
    /// </summary>
    /// <param name="platform">Platform</param>
    ICommandFactory WithPlatform(Platform platform);
}
