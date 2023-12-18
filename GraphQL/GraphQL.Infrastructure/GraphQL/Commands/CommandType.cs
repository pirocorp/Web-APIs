namespace GraphQL.Infrastructure.GraphQL.Commands;

using System.Linq;

using Domain.Models;
using Infrastructure.Data;

using HotChocolate;
using HotChocolate.Types;

/// <inheritdoc />
public class CommandType : ObjectType<Command>
{
    /// <inheritdoc />
    protected override void Configure(IObjectTypeDescriptor<Command> descriptor)
    {
        descriptor.Description("Represents any executable command.");

        descriptor
            .Field(c => c.PlatformId)
            .IsProjected(); // this field should be always projected (included in the query)

        descriptor
            .Field(c => c.Platform)
            .ResolveWith<Resolvers>(c => c.GetPlatform(default!, default!))
            .Description("This is the platform on which command can be executed.");
    }

    // ReSharper disable once ClassNeverInstantiated.Local
    /// <summary>
    /// Resolvers container
    /// </summary>
    private class Resolvers
    {
        /// <summary>
        /// Resolving Platform
        /// </summary>
        /// <param name="command">Parent Command Object</param>
        /// <param name="graphQlDbContext">Db Context</param>
        /// <returns>Platform</returns>
        public Platform? GetPlatform([Parent] Command command, GraphQlDbContext graphQlDbContext)
        {
            return graphQlDbContext.Platforms.FirstOrDefault(p => p.Id == command.PlatformId);
        }
    }
}
