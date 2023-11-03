namespace GraphQL.Exceptions;

using System;

public class NullConfigurationException : Exception
{
    public NullConfigurationException()
        : base("Application configuration cannot be null")
    { }
}
