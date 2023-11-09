namespace GraphQL.Infrastructure.GraphQL;

using System.Linq;

using Domain.Models;
using Infrastructure.Data;

public class Query
{
    public IQueryable<Platform> GetPlatform(GraphQlDbContext context) // Method injection supported by the HotChocolate
        => context.Platforms;
}
