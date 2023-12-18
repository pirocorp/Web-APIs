namespace GraphQL.Infrastructure.GraphQL;

using Domain.Models;

using HotChocolate;
using HotChocolate.Types;

using static Common.InfrastructureConstants;

/// <summary>
/// Subscriptions container
/// </summary>
public class Subscriptions
{
    /// <summary>
    /// On Platform creation notification
    /// </summary>
    /// <param name="platform">The newly created platform</param>
    /// <returns>Platform object</returns>
    [Subscribe]
    [Topic(PLATFORM_ADDED_TOPIC)]
    public Platform OnPlatformAdded([EventMessage] Platform platform)
        => platform;
}
