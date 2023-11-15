namespace GraphQL.Domain.Exceptions;

using System;

/// <summary>
/// All domain exceptions should inherit this exception
/// </summary>
public abstract class BaseDomainException : Exception
{
    private string? message;

    /// <summary>
    /// Exception message overriden by the child exceptions
    /// </summary>
    public override string Message => this.message ?? base.Message;

    /// <summary>
    /// Exception error message
    /// </summary>
    public string Error
    {
        set => this.message = value;
    }
}
