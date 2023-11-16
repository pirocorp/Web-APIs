# Graph QL

## Endpoints

- GraphQL endpoint: https://localhost:7243/graphql/
- Schema Visualization: https://localhost:7243/ui/voyager

## What is GraphQL

- GraphQL is a query and manipulation language for APIs
- GraphQL is also the runtime for fulfilling requests
- Created at Facebook to address both over and under-fetching
- Open Source - hosted by Linux Foundation

## Core Concepts

**Schema** - describes the API in full
- The **Queries** you can perform
- The **Objects** it contains
- The **Data Types** that it has
- Descriptions and documentation for the schema
- **Self-documenting**
- Comprised of **Types**
- Must have a **Root Query Type**

**Types** - anything in GraphQL can be a Type
 - Queries
 - Mutations
 - Subscriptions
 - Objects
 - Enumerations - enums are special scalar types that are restricted to a particular set of allowed values.
 - Scalar - primitive data types
   - Id
   - Int
   - String
   - Boolean
   - Float
  
Schema definition language of GraphQL. Defining an object of type **Car**. Exclamation marks mean it cannot be null
```graphql
type: Car{
  id: ID!
  make: String!
  model: String!
}
```
Resolvers - A resolver returns data for a given field.

![image](https://github.com/pirocorp/Web-APIs/assets/34960418/182f90ab-365c-4ad7-b987-e017f773441e)

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
 
## GraphQL with .NET

- GraphQL.NET
- HotChocolate

## Demo Application Architecture 

![image](https://github.com/pirocorp/Web-APIs/assets/34960418/abe0508d-21c8-4c86-ba5c-ab51bbbeb7f5)

## Codding

### Project Setup

Add the following Nuget dependencies:

- GraphQL.Server.Ui.Voyager
- HotChocolate.AspNetCore
- HotChocolate.Data.EntityFramework
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Tools

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

### Resolver injection of a DbContext ([Pooled](https://chillicream.com/docs/hotchocolate/v13/integrations/entity-framework))

```csharp
public class Query
{
    [UseProjection]
    public IQueryable<Platform> GetPlatform(GraphQlDbContext context) // Method injection supported by the HotChocolate
        => context.Platforms;

    [UseProjection]
    public IQueryable<Command> GetCommand(GraphQlDbContext context)
        => context.Commands;
}
```

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

 serviceCollection
     .AddGraphQLServer()
     .RegisterDbContext<GraphQlDbContext>(DbContextKind.Pooled)
     .AddQueryType<Query>();
```

### Annotation vs Code First Approaches

Annotation (aka "Pure" Code-first) Approach

- Use 'clean' C# code / pure .NET types
- Annotate with attributes to provide GraphQL features

Code First

- Introduce dedicated GraphQL schema types
- Allow us to separate / abstract separate concerns


### Introducing Types

Platform Type

```csharp
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
```

Register the Platform Type in the Service Collection

```csharp
serviceCollection
    .AddGraphQLServer()
    .RegisterDbContext<GraphQlDbContext>(DbContextKind.Pooled)
    .AddQueryType<Query>()
    .AddType<PlatformType>()
    .AddProjections();
```


### Filtering and Sorting

### Mutations

Every GraphQL service has a query type. It may or may not have a mutation type. They act as an entry point into the schema. The **Query** here represents what the client is asking for and the mutation is going to add or delete data from the API.

```graphql
schema{
 query: Query
 mutation: Mutation
}
```
Query and Mutation types are the same as any other GraphQL object type.

```graphql
type Query {
 author_details: [Author]
}

type Mutation {
 addAuthor(firstName: String, lastName: String): Author
}
```

### Subscriptions

## Next Steps

## Wrap Up

## Queries

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
