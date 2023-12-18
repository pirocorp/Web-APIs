namespace GraphQL.Infrastructure.GraphQL.Commands;

using System;

/// <summary>
/// Command creation request
/// </summary>
/// <param name="Description">Command Description</param>
/// <param name="CommandLine">Command Line</param>
/// <param name="PlatformId">Platform Id</param>
public record AddCommandInput(string Description, string CommandLine, Guid PlatformId);
