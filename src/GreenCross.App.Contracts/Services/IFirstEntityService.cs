using GreenCross.App.Entities;

namespace GreenCross.App.Contracts.Services;

public interface IFirstEntityService
{
    Task<FirstEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<FirstEntity> CreateAsync(FirstEntity entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(FirstEntity entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
