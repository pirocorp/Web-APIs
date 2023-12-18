namespace GraphQL.Infrastructure.GraphQL.Platforms;

using Domain.Models;

/// <summary>
/// Add Platform Mutation Response
/// </summary>
/// <param name="Platform">The new platform</param>
public record AddPlatformPayload(Platform Platform);
