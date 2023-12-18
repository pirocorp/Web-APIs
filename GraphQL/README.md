# Graph QL

## What is GraphQL

- GraphQL is a query and manipulation language for APIs
- GraphQL is also the runtime for fulfilling requests
- Created at Facebook to address both over and under-fetching
- Open Source - hosted by Linux Foundation

### Benefits of Declarative Data Fetching

- Avoids round-trips to fetch data
- No more over-fetching or under-fetching of data
- Your specify exactly the data you need and GraphQL gives you exactly what you asked for
- GraphQL is a strongly-typed language, and its schema should have types for all objects that it uses
- The schema serves as a contract between client and server
- GraphQL offers a lot of flexibility

## Core Concepts

**GraphQL** - is a query language for your **API**. **GraphQL** has a strongly typed schema and this schema acts as a **contract** between a **Client** and a **Server**.

**Schema** - describes the API in full
- The **Queries** you can perform
- The **Objects** it contains
- The **Data Types** that it has
- Descriptions and documentation for the schema
- **Self-documenting**
- Comprised of **Types**
- Must have a **Root Query Type**

```graphql
schema {
    query: Query
    mutation: Mutation
}
```

**Types** - anything in GraphQL is a Type
 - **Queries** - the **Query** represents what the **Client** is asking for
    ```graphql
    type Query {
        author_details: [Author]
    }
    ```
 - Mutations - are the state changes of the API
    ```graphql
    type Mutation {
        addAuthor(firstName: String, lastName: String): Author
    }
    ```
 - Subscriptions
 - Objects - Complex types. The exclamation mark shows that the fields cannot be null
    ```graphql
    type Author{
        id: Id!
        firstName: String
        lastName: String 
        rating: Float
        numOfCourses: Int
        courses: [String!]
    }
    ```
 - Enumerations - enums are special scalar types that are restricted to a particular set of allowed values.
    ```graphql
    enum language {
        ENGLISH
        SPANISH
        BULGARIAN
    }
    ```
 - Scalar - primitive data types in 
   - Id
   - Int
   - Float
   - String
   - Boolean
 - Resolvers - A resolver function is a function that resolves a value for a type/field in the GraphQL Schema. Resolvers can return Objects, Scalars, Enumerations

![image](https://github.com/pirocorp/Web-APIs/assets/34960418/182f90ab-365c-4ad7-b987-e017f773441e)

## Quering with GraphQL

### Fields

A GraphQL query is all about asking for specific fields on objects

```graphql
{
    viewer {
        login
        bio,
        email
        id
        name
    }
}
```

### Arguments

In GraphQL you cna pass arguments to fields. Every field and nested object can get its own set of arguments. This gets rid of multiple API fetches

```graphql
{
    viewer {
        login
        bio
        email
        id
        name
        followers (last : 3) {
            nodes {
                id
                bio
            }
        }
    }
}
```

### Alias

You can't query for the same field with different arguments. Hence you need aliases. They let you rename the result of a field with anything you want.

```graphql
{
    viewer {
        login
        bio
        email
        id
        name
        lastFollowers: followers (last : 3) {
            nodes {
                id
                bio
            }
        }
        firstFollowers: followers (first : 5) {
            nodes {
                id
                bio
            }
        }
    }
}
```

### Fragments

Fragments are GraphQL's reusable units. They let you build sets of fields and then include them in multiple queries.

```graphql
{
    viewer {
        login
        bio
        email
        id
        name
        lastFollowers: followers (last : 3) {
            nodes {
                ...userInfo
            }
        }
        firstFollowers: followers (first : 5) {
            nodes {
                ...userInfo
            }
        }
    }
}

fragment userInfo on User {
    id
    bio
    bioHTML
    avatarUrl
}
```

### Operation Name

A meaningful and explicit name for your operation. Think of it like a function name in a programming language.

```graphql
query viewerInfo {
    viewer {
        login
        bio
        email
        id
        name
        lastFollowers: followers (last : 3) {
            nodes {
                ...userInfo
            }
        }
        firstFollowers: followers (first : 5) {
            nodes {
                ...userInfo
            }
        }
    }
}

fragment userInfo on User {
    id
    bio
    bioHTML
    avatarUrl
}
```

### Variables

Arguments to fields can be dynamic. GraphQL uses variables to factor dynamic values out of the query and pass them as a separate dictionary.

```graphql
query viewerInfo($isOwner: Boolean!) {
    viewer {
        id
        name
        starredRepositories(ownedByViewer: $isOwner, last: 5) {
            nodes {
                id,
                name
            }
        }
    }
}

{
    "isOwner": true
}
```


## Mutations

Mutations are used to make changes to the data (Create, Update, Delete data). GraphQL assumes side-effects after mutations and changes the dataset after a mutation.

While query fields are executed in parallel, mutation fields run in series, one after the other.

```graphql
mutation NewStatus($input: ChangeUserStatusInput!) {
    changeUserStatus(input: $input) {
        clientMutationId
        status {
            message
        }
    }
}

query viewerInfo {
    viewer {
        login
        name
        status {
            id
            message
        }
    }
}

{
    "input": {
        "clientMutationId": "10101020",
        "message": "Test Demo"
    }
}
```

## GraphQL vs REST

- REST over-fetches: returning more data than you need
- REST under-fetches: you need to make multiple requests

| REST                               	| GraphQL                  	|
|------------------------------------	|--------------------------	|
| Non-interactive (system to system) 	| Interactive/real-time  	|
| Microservices                      	| Mobile applications      	|
| Simple Object Hierarchy            	| Complex Object Hierarchy 	|
| Repeated, simple queries           	| Complex Queries          	|
| Simple to create                   	| Much harder to create    	|


------------------------------------------------------------------------------------------------------------------------------------------------
 

# Demo Application Architecture 

![image](https://github.com/pirocorp/Web-APIs/assets/34960418/abe0508d-21c8-4c86-ba5c-ab51bbbeb7f5)

## GraphQL with .NET (GraphQL Execution Engines)

GraphQL Execution Engine is responsible for parsing the query from the client, validation of schema, return of the JSON response. The query is traversed field by field executing resolvers for each field. In the end the execution algorithm puts everything together into the correct shape for the results and returns that. This is where the concept for the batch resolving comes into play.

- GraphQL.NET
- HotChocolate

## Endpoints

- GraphQL endpoint: https://localhost:7243/graphql/
- Schema Visualization: https://localhost:7243/ui/voyager

## Codding

### Project Setup

Add the following Nuget dependencies:

- GraphQL.Server.Ui.Voyager
- HotChocolate.AspNetCore
- HotChocolate.Data.EntityFramework
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Tools

### Services registration

Register the DbContext as a [Pooled](https://chillicream.com/docs/hotchocolate/v13/integrations/entity-framework) service

```csharp
 serviceCollection
     .AddPooledDbContextFactory<GraphQlDbContext>(options => options.UseSqlServer(
         configuration.GetConnectionString(DEFAULT_CONNECTION),
         b => b.MigrationsAssembly(typeof(GraphQlDbContext).Assembly.FullName)));

// The Hot Chocolate Resolver Compiler will then take care of correctly injecting your scoped DbContext instance
// into your resolvers and also ensure that the resolvers using it are never run in parallel.

// You can also specify a DbContextKind as an argument to the RegisterDbContext<T> method,
// to change how the DbContext should be injected.

// DbContextKind.Pooled
// This injection mechanism will require your DbContext to be registered as a pooled IDbContextFactory<T>.

// When injecting a DbContext using the DbContextKind.Pool, Hot Chocolate will retrieve one DbContext
// instance from the pool for each invocation of a resolver. Once the resolver has finished executing,
// the instance will be returned to the pool.
```


Register the GraphQL dependencies in the Service Collection

```csharp
serviceCollection
    .AddGraphQLServer()
    .RegisterDbContext<GraphQlDbContext>(DbContextKind.Pooled)
    .AddQueryType<Query>()
    .AddType<PlatformType>()
    .AddType<CommandType>()
    .AddProjections();
```



### Domain Model and DbContext

```csharp
public abstract class Entity<TId> where TId : struct
{
    public TId Id { get; protected set; } = default;

    public override bool Equals(object? obj)
    {
        // base is derived = true
        if (obj is not Entity<TId> other)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        // base != derived
        if (this.GetType() != other.GetType())
        {
            return false;
        }

        // If only one object from both has a default id, they are not equal.
        if (this.Id.Equals(default(TId)) || other.Id.Equals(default(TId)))
        {
            return false;
        }

        return this.Id.Equals(other.Id);
    }

    public static bool operator ==(Entity<TId>? first, Entity<TId>? second)
    {
        if (first is null && second is null)
        {
            return true;
        }

        if (first is null || second is null)
        {
            return false;
        }

        return first.Equals(second);
    }

    public static bool operator !=(Entity<TId>? first, Entity<TId>? second) => !(first == second);

    // ReSharper disable once NonReadonlyMemberInGetHashCode
    public override int GetHashCode() => HashCode.Combine(this.GetType().ToString(), this.Id);
}

public class Platform : Entity<Guid>
{
    private readonly HashSet<Command> commands = new HashSet<Command>();

    private Platform() // Used by EF Core
    {
        this.Name = string.Empty;
        this.LicenseKey = string.Empty;
    }

    internal Platform(string name, string? licenseKey)
    {
        Validate(name);

        this.Id = Guid.NewGuid();
        this.Name = name;
        this.LicenseKey = licenseKey;
    }

    public string Name { get; private set; }

    public string? LicenseKey { get; private set; }

    public IReadOnlyCollection<Command> Commands => this.commands.ToList().AsReadOnly();

    public void ChangeName(string name)
    {
        Validate(name);

        this.Name = name;
    }

    public void AddCommand(Command command)
    {
        this.commands.Add(command);
    }

    private static void Validate(string name)
    {
        Guard.AgainstEmptyString<InvalidPlatformException>(name, nameof(Name));
    }
}

public class Command : Entity<Guid>
{
    private Command() // Used by EF Core
    {
        this.CommandLine = string.Empty;
        this.Description = string.Empty;
        this.Platform =  default!;
    }

    internal Command(string description, string commandLine, Platform platform)
    {
        this.Id = Guid.NewGuid();

        Validate(description, commandLine);

        this.Description = description;
        this.CommandLine = commandLine;
        this.Platform = platform;
    }

    public string CommandLine { get; private set; }

    public string Description { get; private set; }

    public Platform Platform { get; private set; }

    public void ChangeCommand(string command)
    {
        Guard.AgainstEmptyString<InvalidCommandException>(command, nameof(this.CommandLine));

        this.CommandLine = command;
    }

    public void UpdateDescription(string description)
    {
        Guard.AgainstEmptyString<InvalidCommandException>(description, nameof(this.Description));

        this.Description = description;
    }

    public void SwitchPlatform(Platform platform)
    {
        this.Platform = platform;
    }

    private static void Validate(string description, string commandLine)
    {
        Guard.AgainstEmptyString<InvalidCommandException>(description, nameof(Description));
        Guard.AgainstEmptyString<InvalidCommandException>(commandLine, nameof(CommandLine));
    }
}
```

### Simple Quering

```graphql
query{
    platform{
        id,
        name
    }
}
```

```csharp
public class Query
{
    public IQueryable<Platform> GetPlatform([Service] GraphQlDbContext context)
        => context.Platforms;
}
```

### Annotation vs Code First Approaches

Annotation (aka "Pure" Code-first) Approach

- Use 'clean' C# code / pure .NET types
- Annotate with attributes to provide GraphQL features

Code First

- Introduce dedicated GraphQL schema types
- Allow us to separate / abstract separate concerns


### Introducing Types

Registering 'Root' query types

```csharp
public class Query
{
    public IQueryable<Platform> GetPlatform(GraphQlDbContext context) // Method injection supported by the HotChocolate
        => context.Platforms;

    public IQueryable<Command> GetCommand(GraphQlDbContext context)
        => context.Commands;
}
```

Platform Type with resolver injection of a ([Pooled](https://chillicream.com/docs/hotchocolate/v13/integrations/entity-framework)) DbContext

```csharp
public class PlatformType : ObjectType<Platform>
{
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

    private class Resolvers
    {
        public IQueryable<Command> GetCommands([Parent]Platform platform, GraphQlDbContext graphQlDbContext)
        {
            return graphQlDbContext.Commands.Where(p => p.Platform.Id == platform.Id);
        }
    }
}
```

Command Type with resolver injection of a ([Pooled](https://chillicream.com/docs/hotchocolate/v13/integrations/entity-framework)) DbContext

```csharp
public class CommandType : ObjectType<Command>
{
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

    private class Resolvers
    {
        public Platform? GetPlatform([Parent] Command command, GraphQlDbContext graphQlDbContext)
        {
            return graphQlDbContext.Platforms.FirstOrDefault(p => p.Id == command.PlatformId);
        }
    }
}
```

### Filtering and Sorting

Update the Query class

```csharp
public class Query
{
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Platform> GetPlatform(GraphQlDbContext context) // Method injection supported by the HotChocolate
        => context.Platforms;

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Command> GetCommand(GraphQlDbContext context)
        => context.Commands;
}
```

Update registration of GraphQL dependencies in the Service Collection

```csharp
serviceCollection
    .AddGraphQLServer()
    .RegisterDbContext<GraphQlDbContext>(DbContextKind.Pooled)
    .AddQueryType<Query>()
    .AddType<CommandType>()
    .AddType<PlatformType>()
    .AddProjections()
    .AddFiltering()
    .AddSorting();
```

### Mutations

#### Add Platform Mutation

Create Mutations class and in it create ```AddPlatformAsync``` method.

```csharp
public class Mutations
{
    public async Task<AddPlatformPayload> AddPlatformAsync(
        AddPlatformInput input, 
        GraphQlDbContext dbContext,
        IPlatformFactory platformFactory)
    {
        Platform platform = platformFactory.WithName(input.Name).Build();

        await dbContext.AddAsync(platform);
        await dbContext.SaveChangesAsync();

        return new AddPlatformPayload(platform);
    }
}
```

Register GraphQL mutations in Service Collection

```csharp
serviceCollection
    .AddGraphQLServer()
    .RegisterDbContext<GraphQlDbContext>(DbContextKind.Pooled)
    .AddQueryType<Query>()
    .AddMutationType<Mutations>()
    .AddType<CommandType>()
    .AddType<PlatformType>()
    .AddProjections()
    .AddFiltering()
    .AddSorting();
```

#### Add Command Mutation

In Mutations class create ```AddCommandAsync``` method.

```csharp
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
```


### Subscriptions

## Quering and Mutating the API

### Get Platforms Query

```graphql
query{
  platform{
    id,
    name
  }
}
```

### Get Platforms Paralel Query

```graphql
query{
    a:platform{
        id,
        name
    }
    b:platform{
        id,
        name
    }
    c:platform{
        id,
        name
    }
}
```

### Get Platforms and Commands Query

```graphql
query{
  platform{
    id,
    name,
    commands{
      id,
      description,
      commandLine
    }
  }
}
```

### Get Commands with Platform Name Query
```graphql
query {
  Command {
    command {
      commandLine
      description
      id
      platform {
        name
      }
    }
  }
}
```

### Filter Commands by Platform Id Query

```graphql
query CommandFilterQuery {
    command(where: { platformId: { eq: "9877F87B-0377-461D-859B-E8FB08D26802" } }) {
        commandLine
        description
        platform {
            name
            id
        }
    }
}
```

### Sorting Platforms Alphabetically Query

```graphql
query SortingPlatform {
    platform(order: {name: ASC}) {
        name
    }
}
```

### Add Platform Mutation 

```graphql
mutation AddPlatform {
    addPlatform(input: { name: "Azure" }) {
        platform {
            id
            name
        }
    }
}
```

### Add Command Mutation 

```graphql
mutation AddCommand {
    addCommand(
        input: {
            description: "Perform directory listing"
            commandLine: "ls"
            platformId: "69C7BCB8-E4A3-4164-96A5-37D9946AEE84"
        }
    ) {
        command {
            id
            description
            commandLine
            platform {
                name
            }
            platformId
        }
    }
}
```

## Documentation

Out of the box, Hot Chocolate has the ability to automatically generate API documentation from your existing [XML documentation comments](https://docs.microsoft.com/en-us/dotnet/csharp/codedoc).

Simply Add ```<GenerateDocumentationFile>true</GenerateDocumentationFile>``` to your ```<PropertyGroup>``` in csproj file.

```xml
<PropertyGroup>
  <GenerateDocumentationFile>true</GenerateDocumentationFile>
</PropertyGroup>
```

## Helpers

While in `GraphQL.Infrastructure` project use this type of command to work with migrations

```bash
dotnet ef --startup-project ..\GraphQL.WebApi\ migrations list
```

## Next Steps

TODO: Add Update and Delete Mutations

## Wrap Up
