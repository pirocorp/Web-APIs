namespace GraphQL.Domain.Common;

using System;

/// <summary>
/// Domain entity base class.
/// <remarks>
/// All domain entities should inherit from this class
/// </remarks>
/// </summary>
/// <typeparam name="TId"></typeparam>
public abstract class Entity<TId> where TId : struct
{
    /// <summary>
    /// The identity of the domain entity
    /// </summary>
    public TId Id { get; protected set; } = default;

    /// <summary>
    /// Equality compare the current object with another object
    /// </summary>
    /// <param name="obj">The other object</param>
    public override bool Equals(object? obj)
    {
        // base is derived = true
        if (obj is not Entity<TId> other)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        // base != derived
        if (this.GetType() != other.GetType())
        {
            return false;
        }

        // If only one object from both has a default id, they are not equal.
        if (this.Id.Equals(default(TId)) || other.Id.Equals(default(TId)))
        {
            return false;
        }

        return this.Id.Equals(other.Id);
    }

    /// <summary>
    /// Compare if two object are equal
    /// </summary>
    /// <param name="first">The first object</param>
    /// <param name="second">The second object</param>
    public static bool operator ==(Entity<TId>? first, Entity<TId>? second)
    {
        if (first is null && second is null)
        {
            return true;
        }

        if (first is null || second is null)
        {
            return false;
        }

        return first.Equals(second);
    }

    /// <summary>
    /// Compare if two object are NOT equal
    /// </summary>
    /// <param name="first">The first object</param>
    /// <param name="second">The second object</param>
    public static bool operator !=(Entity<TId>? first, Entity<TId>? second) => !(first == second);

    /// <inheritdoc />
    // ReSharper disable once NonReadonlyMemberInGetHashCode
    public override int GetHashCode() => HashCode.Combine(this.GetType().ToString(), this.Id);
}
