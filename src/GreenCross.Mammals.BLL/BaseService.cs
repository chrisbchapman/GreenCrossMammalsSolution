using GreenCross.Mammals.Contracts.Data;
using GreenCross.Mammals.Contracts.Repositories;
using Microsoft.Extensions.Logging;

namespace GreenCross.Mammals.BLL;

public abstract partial class BaseService<TEntity> where TEntity : class
{
    protected readonly IRepository<TEntity> _repository;
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly ILogger _logger;
    protected readonly string _entityName;

    protected BaseService(
        IRepository<TEntity> repository,
        IUnitOfWork unitOfWork,
        ILogger logger)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _entityName = typeof(TEntity).Name;
    }

    [LoggerMessage(Level = LogLevel.Information, Message = "Fetching {EntityName} with ID: {Id}")]
    protected partial void LogFetchingEntity(string entityName, int id);

    [LoggerMessage(Level = LogLevel.Warning, Message = "{EntityName} with ID: {Id} not found")]
    protected partial void LogEntityNotFound(string entityName, int id);

    [LoggerMessage(Level = LogLevel.Information, Message = "Creating new {EntityName}")]
    protected partial void LogCreatingEntity(string entityName);

    [LoggerMessage(Level = LogLevel.Information, Message = "{EntityName} created successfully")]
    protected partial void LogEntityCreated(string entityName);

    [LoggerMessage(Level = LogLevel.Error, Message = "Failed to create {EntityName}")]
    protected partial void LogCreateFailed(string entityName, Exception ex);

    [LoggerMessage(Level = LogLevel.Information, Message = "Updating {EntityName} with ID: {Id}")]
    protected partial void LogUpdatingEntity(string entityName, int id);

    [LoggerMessage(Level = LogLevel.Information, Message = "{EntityName} updated successfully")]
    protected partial void LogEntityUpdated(string entityName);

    [LoggerMessage(Level = LogLevel.Error, Message = "Failed to update {EntityName} with ID: {Id}")]
    protected partial void LogUpdateFailed(string entityName, Exception ex, int id);

    [LoggerMessage(Level = LogLevel.Information, Message = "Deleting {EntityName} with ID: {Id}")]
    protected partial void LogDeletingEntity(string entityName, int id);

    [LoggerMessage(Level = LogLevel.Information, Message = "{EntityName} deleted successfully")]
    protected partial void LogEntityDeleted(string entityName);

    [LoggerMessage(Level = LogLevel.Error, Message = "Failed to delete {EntityName} with ID: {Id}")]
    protected partial void LogDeleteFailed(string entityName, Exception ex, int id);

    public virtual async Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        LogFetchingEntity(_entityName, id);

        var entity = await _repository.GetByIdAsync(id, cancellationToken);

        if (entity == null)
        {
            LogEntityNotFound(_entityName, id);
        }

        return entity;
    }

    public virtual async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        LogCreatingEntity(_entityName);

        try
        {
            var result = await _repository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            LogEntityCreated(_entityName);
            return result;
        }
        catch (Exception ex)
        {
            LogCreateFailed(_entityName, ex);
            throw;
        }
    }

    public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        // Assumes entity has an Id property - works with BaseEntity
        var id = (int)entity.GetType().GetProperty("Id")!.GetValue(entity)!;

        LogUpdatingEntity(_entityName, id);

        try
        {
            _repository.Update(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            LogEntityUpdated(_entityName);
        }
        catch (Exception ex)
        {
            LogUpdateFailed(_entityName, ex, id);
            throw;
        }
    }

    public virtual async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        LogDeletingEntity(_entityName, id);
        try
        {
            await _repository.DeleteAsync(id, cancellationToken);

            LogEntityDeleted(_entityName);
        }
        catch (Exception ex)
        {
            LogDeleteFailed(_entityName, ex, id);
            throw;
        }
    }
}
