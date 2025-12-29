using GreenCross.Mammals.Entities;

namespace GreenCross.Mammals.Contracts.Repositories;

public interface IHarvestMouseRecordRepository : IRepository<HarvestMouseRecord>
{
    // Add any HarvestMouseRecord-specific repository methods here if needed
    // Example: Task<IEnumerable<HarvestMouseRecord>> GetByTypeAsync(string type, CancellationToken cancellationToken = default);
}
