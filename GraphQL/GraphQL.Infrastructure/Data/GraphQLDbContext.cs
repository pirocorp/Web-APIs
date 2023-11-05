namespace GraphQL.Infrastructure.Data;

using GraphQL.Domain.Models;
using Microsoft.EntityFrameworkCore;

public class GraphQlDbContext : DbContext
{
    public GraphQlDbContext(DbContextOptions<GraphQlDbContext> options)
        : base(options)
    { }

    public DbSet<Platform> Platforms => this.Set<Platform>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GraphQlDbContext).Assembly);
    }
}
