# Web-APIs

Patterns, tools, architectures, libraries, and everything about APIs and things that can be useful in implementing APIs.

## [Polly](https://github.com/App-vNext/Polly)

**Resilience** and **transient fault** handling library for .NET. Used to create **Policies** in .NET apps for handling the **transient fault**s.

Supported **Polices** example:
- Retry
- Circuit Breaker
- Timeout
- Bulkhead Isolation

**Transient** - lasting only for a short time (impermanent).

**Transient faults** - fault occurrences that exist for short periods. Examples:
- A network connection is unavailable for a short time (router reboots).
- Microservice starting up
- Server refusing connections due to connection pool exhaustion

If handled correctly, rather than getting an error response and accepting failure, we could eventually get a successful response. This is particularly advantageous in distributed (i.e., microservices) application architectures. 

The example will be **retry policy** configuring the number of retries and the interval between retries.
  - Policy 1 - Retry immediately 5x times
  - Policy 2 - Retry 5x times and wait 3s between retries
  - Policy 3 - Retry 5x with exponential backoff 

NuGet package: ```Microsoft.Extensions.Http.Polly```
