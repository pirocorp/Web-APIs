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

### Model and DbContext

### Simple Quering

### Revisit DbContext

### Multi-Model

### Annotation vs Code First

### Introducing Types

### Filtering and Sorting

### Mutations

### Subscriptions

## Next Steps

## Wrap Up


