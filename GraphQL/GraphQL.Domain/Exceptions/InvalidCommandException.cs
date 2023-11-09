namespace GraphQL.Domain.Exceptions;

public class InvalidCommandException : BaseDomainException
{
    public InvalidCommandException()
    { }

    public InvalidCommandException(string message)
    {
        this.Error = message;
    }
}
