namespace GraphQL.Infrastructure.Data.Configurations;

using Domain.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

/// <inheritdoc />
public class PlatformConfiguration : IEntityTypeConfiguration<Platform>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Platform> platform)
    {
        platform.HasKey(x => x.Id);

        platform
            .Property(x => x.Name)
            .IsRequired();

        platform
            .HasMany(p => p.Commands)
            .WithOne(c => c.Platform)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
