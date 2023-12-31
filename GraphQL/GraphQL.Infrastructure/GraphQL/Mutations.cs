﻿namespace GraphQL.Infrastructure.GraphQL;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Domain.Factories;
using Domain.Models;
using Infrastructure.Data;
using Infrastructure.GraphQL.Commands;
using Infrastructure.GraphQL.Platforms;

using HotChocolate;
using HotChocolate.Subscriptions;

using Microsoft.AspNetCore.Mvc;

using static Common.InfrastructureConstants;

/// <summary>
/// Mutations container
/// </summary>
public class Mutations
{
    /// <summary>
    /// Add Platform mutation
    /// </summary>
    /// <param name="input">Platform Input</param>
    /// <param name="dbContext">GraphQL DbContext</param>
    /// <param name="platformFactory">Platform Factory</param>
    /// <param name="eventSender">Service used for publishing events</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Add Platform mutation payload</returns>
    public async Task<AddPlatformPayload> AddPlatformAsync(
        AddPlatformInput input, 
        GraphQlDbContext dbContext,
        [FromServices] IPlatformFactory platformFactory,
        [Service] ITopicEventSender eventSender,
        CancellationToken cancellationToken)
    {
        Platform platform = platformFactory.WithName(input.Name).Build();

        await dbContext.AddAsync(platform, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        await eventSender.SendAsync(PLATFORM_ADDED_TOPIC, platform, cancellationToken);

        return new AddPlatformPayload(platform);
    }

    /// <summary>
    /// Add Command Mutation
    /// </summary>
    /// <param name="input">Command Input</param>
    /// <param name="dbContext">GraphQL DbContext</param>
    /// <param name="commandFactory">Command Factory</param>
    /// <returns>Add Command mutation payload</returns>
    public async Task<AddCommandPayload> AddCommandAsync(
        AddCommandInput input, 
        GraphQlDbContext dbContext,
        [FromServices] ICommandFactory commandFactory)
    {
        Platform platform = dbContext.Platforms
            .Single(p => p.Id == input.PlatformId);

        Command command = commandFactory
            .WithCommandLine(input.CommandLine)
            .WithDescription(input.Description)
            .WithPlatform(platform)
            .Build();

        await dbContext.AddAsync(command);
        await dbContext.SaveChangesAsync();

        return new AddCommandPayload(command);
    }
}
