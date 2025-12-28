using GreenCross.Mammals.Contracts.Data;
using GreenCross.Mammals.Contracts.Repositories;
using GreenCross.Mammals.Contracts.Services;
using GreenCross.Mammals.Entities;
using Microsoft.Extensions.Logging;

namespace GreenCross.Mammals.BLL;

public partial class FirstEntityService : IFirstEntityService
{
    private readonly IFirstEntityRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<FirstEntityService> _logger;

    public FirstEntityService(
        IFirstEntityRepository repository,
        IUnitOfWork unitOfWork,
        ILogger<FirstEntityService> logger)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    // High-performance logging with source generators
    [LoggerMessage(Level = LogLevel.Information, Message = "Fetching FirstEntity with ID: {Id}")]
    private partial void LogFetchingEntity(int id);

    [LoggerMessage(Level = LogLevel.Warning, Message = "FirstEntity with ID: {Id} not found")]
    private partial void LogEntityNotFound(int id);

    [LoggerMessage(Level = LogLevel.Information, Message = "Creating new FirstEntity")]
    private partial void LogCreatingEntity();

    [LoggerMessage(Level = LogLevel.Information, Message = "FirstEntity created successfully")]
    private partial void LogEntityCreated();

    [LoggerMessage(Level = LogLevel.Error, Message = "Failed to create FirstEntity")]
    private partial void LogCreateFailed(Exception ex);

    [LoggerMessage(Level = LogLevel.Information, Message = "Updating FirstEntity with ID: {Id}")]
    private partial void LogUpdatingEntity(int id);

    [LoggerMessage(Level = LogLevel.Information, Message = "FirstEntity updated successfully")]
    private partial void LogEntityUpdated();

    [LoggerMessage(Level = LogLevel.Error, Message = "Failed to update FirstEntity with ID: {Id}")]
    private partial void LogUpdateFailed(Exception ex, int id);

    [LoggerMessage(Level = LogLevel.Information, Message = "Deleting FirstEntity with ID: {Id}")]
    private partial void LogDeletingEntity(int id);

    [LoggerMessage(Level = LogLevel.Information, Message = "FirstEntity deleted successfully")]
    private partial void LogEntityDeleted();

    [LoggerMessage(Level = LogLevel.Error, Message = "Failed to delete FirstEntity with ID: {Id}")]
    private partial void LogDeleteFailed(Exception ex, int id);

    public async Task<FirstEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        LogFetchingEntity(id);

        var entity = await _repository.GetByIdAsync(id, cancellationToken);

        if (entity == null)
        {
            LogEntityNotFound(id);
        }

        return entity;
    }

    public async Task<FirstEntity> CreateAsync(FirstEntity entity, CancellationToken cancellationToken = default)
    {
        LogCreatingEntity();

        try
        {
            var result = await _repository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            LogEntityCreated();
            return result;
        }
        catch (Exception ex)
        {
            LogCreateFailed(ex);
            throw;
        }
    }

    public async Task UpdateAsync(FirstEntity entity, CancellationToken cancellationToken = default)
    {
        LogUpdatingEntity(entity.Id);

        try
        {
            await _repository.UpdateAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            LogEntityUpdated();
        }
        catch (Exception ex)
        {
            LogUpdateFailed(ex, entity.Id);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        LogDeletingEntity(id);
        try
        {
            await _repository.DeleteAsync(id, cancellationToken);
            // No need for SaveChangesAsync when using ExecuteDeleteAsync
            // await _unitOfWork.SaveChangesAsync(cancellationToken);

            LogEntityDeleted();
        }
        catch (Exception ex)
        {
            LogDeleteFailed(ex, id);
            throw;
        }
    }
}
