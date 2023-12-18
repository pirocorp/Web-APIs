namespace GraphQL.Infrastructure.GraphQL.Commands;

using Domain.Models;

/// <summary>
/// Add Command Mutation Response
/// </summary>
/// <param name="Command"></param>
public record AddCommandPayload(Command Command);
