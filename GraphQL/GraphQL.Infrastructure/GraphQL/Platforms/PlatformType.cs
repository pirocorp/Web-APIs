namespace GraphQL.Infrastructure.GraphQL.Platforms;

using System.Linq;

using Domain.Models;
using Infrastructure.Data;

using HotChocolate;
using HotChocolate.Types;

/// <inheritdoc />
public class PlatformType : ObjectType<Platform>
{
    /// <inheritdoc />
    protected override void Configure(IObjectTypeDescriptor<Platform> descriptor)
    {
        descriptor.Description("Represents software or service that has a command line interface.");

        descriptor
            .Field(p => p.Id)
            .IsProjected(); // this field should be always projected (included in the query)

        descriptor
            .Field(p => p.LicenseKey)
            .Description("Valid license for the platform.")
            .Ignore(); // The property will not be exposed.

        descriptor
            .Field(p => p.Commands)
            .ResolveWith<Resolvers>(p => p.GetCommands(default!, default!))
            .Description("This is the list of available commands for this Platform.");
    }

    // ReSharper disable once ClassNeverInstantiated.Local
    /// <summary>
    /// Resolvers container
    /// </summary>
    private class Resolvers
    {
        /// <summary>
        /// Resolving Commands collection
        /// </summary>
        /// <param name="platform">Parent platform object</param>
        /// <param name="graphQlDbContext">Db Context</param>
        /// <returns>Queryable commands</returns>
        public IQueryable<Command> GetCommands([Parent]Platform platform, GraphQlDbContext graphQlDbContext)
        {
            return graphQlDbContext.Commands.Where(p => p.Platform.Id == platform.Id);
        }
    }
}
