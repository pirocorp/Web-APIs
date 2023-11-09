namespace GraphQL.Infrastructure.Data.Configurations;

using Domain.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PlatformConfiguration : IEntityTypeConfiguration<Platform>
{
    public void Configure(EntityTypeBuilder<Platform> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Name)
            .IsRequired();

        // Because the Commands collection is read-only, we need to explicitly tell
        // Entity Framework Core to use the private field by providing its name ("commands").
        builder
            .HasMany(p => p.Commands)
            .WithOne(c => c.Platform)
            .OnDelete(DeleteBehavior.Restrict)
            .Metadata
            .PrincipalToDependent // Configures Platform to be principal of the relation e.g. Command to have PlatformId as Foreign Key
            // If the Foreign Key filed was in Platform should use DependentToPrincipal
            ?.SetField("commands"); // Sets commands filed on Dealer Entity
    }
}
