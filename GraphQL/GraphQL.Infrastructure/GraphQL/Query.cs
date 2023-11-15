namespace GraphQL.Infrastructure.GraphQL;

using System.Linq;

using Domain.Models;

using HotChocolate.Data;
using Infrastructure.Data;

/// <summary>
/// Graph QL Query Objects
/// </summary>
public class Query
{
    /// <summary>
    /// Platform Query
    /// </summary>
    [UseProjection]
    public IQueryable<Platform> GetPlatform(GraphQlDbContext context) // Method injection supported by the HotChocolate
        => context.Platforms;

    /// <summary>
    /// Command Query
    /// </summary>
    [UseProjection]
    public IQueryable<Command> GetCommand(GraphQlDbContext context)
        => context.Commands;
}
