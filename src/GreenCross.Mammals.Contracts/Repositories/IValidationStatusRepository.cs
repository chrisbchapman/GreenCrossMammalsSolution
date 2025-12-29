using GreenCross.Mammals.Entities;

namespace GreenCross.Mammals.Contracts.Repositories;

public interface IValidationStatusRepository : IRepository<ValidationStatus>
{
    // Add any ValidationStatus-specific repository methods here if needed
    // Example: Task<IEnumerable<ValidationStatus>> GetByTypeAsync(string type, CancellationToken cancellationToken = default);
}
