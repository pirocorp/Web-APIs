namespace GraphQL.Infrastructure.Data.Configurations;

using Domain.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

/// <inheritdoc />
public class CommandConfiguration : IEntityTypeConfiguration<Command>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Command> command)
    {
        command.HasKey(x => x.Id);

        command
            .Property(x => x.CommandLine)
            .IsRequired();

        command
            .Property(x => x.Description)
            .IsRequired();
    }
}
