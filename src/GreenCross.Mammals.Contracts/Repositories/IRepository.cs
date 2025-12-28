namespace GreenCross.Mammals.Contracts.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates the specified entity.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <remarks>
    /// This method is not asynchronous and does not return a Task because underlying database operations are synchronous.
    /// </remarks>
    void Update(TEntity entity, CancellationToken cancellationToken); // Changed: removed async, no Task
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);

    // Paging support
    Task<(IEnumerable<TEntity> Items, int TotalCount)> GetPagedAsync(
    int pageNumber,
    int pageSize,
    CancellationToken cancellationToken = default);

    // Projection support for memory efficiency
    Task<IEnumerable<TResult>> GetAllProjectedAsync<TResult>(
        System.Linq.Expressions.Expression<System.Func<TEntity, TResult>> selector,
        CancellationToken cancellationToken = default);

}
