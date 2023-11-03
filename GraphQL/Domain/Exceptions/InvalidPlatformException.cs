namespace GraphQL.Domain.Exceptions;

public class InvalidPlatformException : BaseDomainException
{
    public InvalidPlatformException()
    { }

    public InvalidPlatformException(string message)
    {
        this.Error = message;
    }
}
