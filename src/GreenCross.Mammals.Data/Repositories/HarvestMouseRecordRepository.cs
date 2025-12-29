using GreenCross.Mammals.Contracts.Repositories;
using GreenCross.Mammals.Entities;
using Microsoft.EntityFrameworkCore;

namespace GreenCross.Mammals.Data.Repositories;

public class HarvestMouseRecordRepository : Repository<HarvestMouseRecord>, IHarvestMouseRecordRepository
{
    public HarvestMouseRecordRepository(AppDbContext context) : base(context)
    {
    }

    // Override for custom key property
    public override async Task<HarvestMouseRecord?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.RecordId == id, cancellationToken);
    }

    public override async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        await _dbSet.Where(e => e.RecordId == id).ExecuteDeleteAsync(cancellationToken);
    }

    public override async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AnyAsync(e => e.RecordId == id, cancellationToken);
    }
}
