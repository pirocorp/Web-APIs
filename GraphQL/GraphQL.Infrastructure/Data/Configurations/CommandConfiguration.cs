namespace GraphQL.Infrastructure.Data.Configurations;

using Domain.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

/// <inheritdoc />
public class CommandConfiguration : IEntityTypeConfiguration<Command>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Command> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.CommandLine)
            .IsRequired();

        builder
            .Property(x => x.Description)
            .IsRequired();
    }
}
