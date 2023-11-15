namespace GraphQL.WebApi.Exceptions;

using System;

/// <summary>
/// Null Configuration Exception
/// </summary>
public class NullConfigurationException : Exception
{
    /// <summary>
    /// Create Null Configuration Exception with predefined message
    /// </summary>
    public NullConfigurationException()
        : base("Application configuration cannot be null")
    { }
}
