namespace GraphQL.Infrastructure.Data.Configurations;

using GraphQL.Domain.Models;

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
    }
}
