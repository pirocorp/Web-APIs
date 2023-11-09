namespace GraphQL.Infrastructure.Data;

using Domain.Models;

using Microsoft.EntityFrameworkCore;

public class GraphQlDbContext : DbContext
{
    public GraphQlDbContext(DbContextOptions<GraphQlDbContext> options)
        : base(options)
    { }

    public DbSet<Platform> Platforms => this.Set<Platform>();

    public DbSet<Command> Commands => this.Set<Command>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GraphQlDbContext).Assembly);
    }
}
