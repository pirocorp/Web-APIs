namespace GraphQL.Infrastructure.GraphQL.Platforms;

using Domain.Models;

using HotChocolate.Types;

/// <inheritdoc />
public class CommandType : ObjectType<Command>
{
    /// <inheritdoc />
    protected override void Configure(IObjectTypeDescriptor<Command> descriptor)
    {
        //descriptor
        //    .Field(c => c.Platform)
        //    .Ignore();
    }
}
