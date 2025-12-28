using GreenCross.Mammals.Contracts.Repositories;
using GreenCross.Mammals.Entities;
using Microsoft.EntityFrameworkCore;

namespace GreenCross.Mammals.Data.Repositories;

public class RecorderRepository : Repository<Recorder>, IRecorderRepository
{
    public RecorderRepository(AppDbContext context) : base(context)
    {
    }

    // Override for custom key property
    public override async Task<Recorder?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.RecorderId == id, cancellationToken);
    }

    public override async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        await _dbSet.Where(e => e.RecorderId == id).ExecuteDeleteAsync(cancellationToken);
    }

    public override async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AnyAsync(e => e.RecorderId == id, cancellationToken);
    }

    public async Task<IEnumerable<Recorder>> GetWildlifeGroupMembersAsync(
        CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .AsNoTracking()
            .Where(r => r.WildlifeGroupMember)
            .OrderBy(r => r.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<Recorder?> GetByInitialsAsync(
        string initials,
        CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Initials == initials, cancellationToken);
    }
}
