using GreenCross.Mammals.Entities;

namespace GreenCross.Mammals.Contracts.Services;

public interface IRecorderService
{
    Task<Recorder?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Recorder> CreateAsync(Recorder entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(Recorder entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
