namespace GraphQL.Domain.Factories;

/// <summary>
/// Base interface for entity factories
/// </summary>
/// <typeparam name="TEntity">Type of entity that the factory will produce</typeparam>
public interface IFactory<out TEntity>
{
    /// <summary>
    /// Produces entity of type <typeparamref name="TEntity"/>
    /// </summary>
    /// <returns>Produced entity</returns>
    TEntity Build();
}
