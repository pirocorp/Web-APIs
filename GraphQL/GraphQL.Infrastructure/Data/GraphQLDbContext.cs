namespace GraphQL.Infrastructure.Data;

using Domain.Models;

using Microsoft.EntityFrameworkCore;

/// <inheritdoc />
public class GraphQlDbContext : DbContext
{
    /// <inheritdoc />
    public GraphQlDbContext(DbContextOptions<GraphQlDbContext> options)
        : base(options)
    { }

    /// <summary>
    /// DbSet of <see cref="Platform"/> that can be used to query and save instances of <see cref="Platform"/>.
    /// </summary>
    public DbSet<Platform> Platforms => this.Set<Platform>();

    /// <summary>
    /// DbSet of <see cref="Command"/> that can be used to query and save instances of <see cref="Command"/>.
    /// </summary>
    public DbSet<Command> Commands => this.Set<Command>();

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GraphQlDbContext).Assembly);
    }
}
