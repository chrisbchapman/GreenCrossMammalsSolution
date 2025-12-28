using GreenCross.Mammals.Entities;

namespace GreenCross.Mammals.Contracts.Services;

public interface ILocationService
{
    Task<Location?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Location> CreateAsync(Location entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(Location entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
