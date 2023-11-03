namespace GraphQL.Infrastructure.Data;

using GraphQL.Domain.Models;

using Microsoft.EntityFrameworkCore;

public class GraphQlDbContext : DbContext
{
    public GraphQlDbContext(DbContextOptions<GraphQlDbContext> options)
        : base(options)
    { }

    public DbSet<Platform> Platforms => Set<Platform>();
}
