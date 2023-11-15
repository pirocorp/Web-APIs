namespace GraphQL.Domain.Common;

using GraphQL.Domain.Exceptions;

/// <summary>
/// Collection of Guard clauses
/// </summary>
public static class Guard
{
    /// <summary>
    /// If string is null, empty or white space throws exception <typeparamref name="TException"/>
    /// </summary>
    /// <typeparam name="TException"><see cref="BaseDomainException"/> Exception</typeparam>
    /// <param name="value">Value to be checked</param>
    /// <param name="name">Name of the value</param>
    public static void AgainstEmptyString<TException>(string value, string name = "Value")
        where TException : BaseDomainException, new()
    {
        if (!string.IsNullOrEmpty(value))
        {
            return;
        }

        ThrowException<TException>($"'{name}' cannot be null ot empty.");
    }

    private static void ThrowException<TException>(string message)
        where TException : BaseDomainException, new()
    {
        var exception = new TException()
        {
            Error = message
        };

        throw exception;
    }
}
