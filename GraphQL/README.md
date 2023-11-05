# Graph QL

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
 - Enumerations
 - Scalar
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

### Model and DbContext

```csharp
public class Platform
{
    private Platform() // Used by EF Core
    {
        this.Name = string.Empty;
        this.LicenseKey = string.Empty;
    }

    internal Platform(string name, string? licenseKey)
    {
        this.Validate(name);

        this.Id = Guid.NewGuid();
        this.Name = name;
        this.LicenseKey = licenseKey;
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public string? LicenseKey { get; private set; }

    public void ChangeName(string name)
    {
        this.Validate(name);

        this.Name = name;
    }

    private void Validate(string name)
    {
        Guard.AgainstEmptyString<InvalidPlatformException>(name, nameof(this.Name));
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
    public IQueryable<Platform> GetPlatform(GraphQlDbContext context) // Method injection supported by the HotChocolate
        => context.Platforms;
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


### Multi-Model

### Annotation vs Code First

### Introducing Types

### Filtering and Sorting

### Mutations

### Subscriptions

## Next Steps

## Wrap Up

## Helpers

While in `GraphQL.Infrastructure` project use this type of command to work with migrations

```bash
dotnet ef --startup-project ..\GraphQL.WebApi\ migrations list
```
