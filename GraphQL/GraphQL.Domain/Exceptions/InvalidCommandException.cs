namespace GraphQL.Domain.Exceptions;

/// <summary>
/// Invalid Command Exception
/// </summary>
public class InvalidCommandException : BaseDomainException
{
    /// <summary>
    /// Invalid Command Exception Parameterless Constructor
    /// </summary>
    public InvalidCommandException()
    { }

    /// <summary>
    /// Invalid Command Exception Constructor
    /// </summary>
    /// <param name="message">Custom error message</param>
    public InvalidCommandException(string message)
    {
        this.Error = message;
    }
}
