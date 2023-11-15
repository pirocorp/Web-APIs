namespace GraphQL.Domain.Exceptions;

/// <summary>
/// Invalid Platform Exception
/// </summary>
public class InvalidPlatformException : BaseDomainException
{
    /// <summary>
    /// Invalid Platform Exception Parameterless Constructor
    /// </summary>
    public InvalidPlatformException()
    { }

    /// <summary>
    /// Invalid Platform Exception Constructor
    /// </summary>
    /// <param name="message">Custom error message</param>
    public InvalidPlatformException(string message)
    {
        this.Error = message;
    }
}
