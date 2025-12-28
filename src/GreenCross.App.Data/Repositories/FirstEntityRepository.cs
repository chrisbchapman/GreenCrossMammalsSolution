using GreenCross.App.Contracts.Repositories;
using GreenCross.App.Entities;
using Microsoft.EntityFrameworkCore;

namespace GreenCross.App.Data.Repositories;

public class FirstEntityRepository : Repository<FirstEntity>, IFirstEntityRepository
{
    public FirstEntityRepository(AppDbContext context) : base(context)
    {
    }

    // Override for custom key property
    public new async Task<FirstEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.FirstEntityId == id, cancellationToken);
    }

    Task<FirstEntity> IFirstEntityRepository.AddAsync(FirstEntity entity, CancellationToken cancellationToken)
    {
        return AddAsync(entity, cancellationToken);
    }

    Task IFirstEntityRepository.DeleteAsync(int id, CancellationToken cancellationToken)
    {
        // Override to use custom key
        return _dbSet.Where(e => e.FirstEntityId == id).ExecuteDeleteAsync(cancellationToken);
    }

    Task<FirstEntity> IFirstEntityRepository.GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return GetByIdAsync(id, cancellationToken);
    }

    Task IFirstEntityRepository.UpdateAsync(FirstEntity entity, CancellationToken cancellationToken)
    {
        // For update operations, we need tracking
        _context.Entry(entity).State = EntityState.Modified;
        return Task.CompletedTask;
    }
}
