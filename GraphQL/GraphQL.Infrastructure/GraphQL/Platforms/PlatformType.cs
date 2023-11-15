namespace GraphQL.Infrastructure.GraphQL.Platforms;

using System.Linq;

using Domain.Models;
using HotChocolate;
using Infrastructure.Data;

using HotChocolate.Types;

/// <inheritdoc />
public class PlatformType : ObjectType<Platform>
{
    /// <inheritdoc />
    protected override void Configure(IObjectTypeDescriptor<Platform> descriptor)
    {
        descriptor.Description("Represents software or service that has a command line interface.");

        descriptor
            .Field(p => p.LicenseKey)
            .Ignore();

        descriptor
            .Field(p => p.Commands)
            .ResolveWith<Resolvers>(p => p.GetCommands(default!, default!))
            .Description("This is the list of available commands for this Platform.");
    }

    private class Resolvers
    {
        public IQueryable<Command> GetCommands([Parent]Platform platform, GraphQlDbContext graphQlDbContext)
        {
            return graphQlDbContext.Commands.Where(p => p.Platform.Id == platform.Id);
        }
    }
}
