using GreenCross.Mammals.Contracts.Dtos;
using GreenCross.Mammals.Contracts.Repositories;
using GreenCross.Mammals.Entities;
using GreenCross.Utils.Geo;
using Microsoft.EntityFrameworkCore;

namespace GreenCross.Mammals.Data.Repositories;

public class HarvestMouseRecordRepository : Repository<HarvestMouseRecord>, IHarvestMouseRecordRepository
{
    public HarvestMouseRecordRepository(MammalDbContext context) : base(context)
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

    public async Task<IEnumerable<HarvestMouseRecordGridDto>> GetRecordsForGridAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .AsNoTracking()
            .Include(h => h.Recorder)
            .Include(h => h.Location)
            .Include(h => h.VerificationStatus)
            .Where(h => !h.IsDeleted)
            .OrderByDescending(h => h.DateRecorded)
            .Select(h => new HarvestMouseRecordGridDto
            {
                RecordId = h.RecordId,
                DateRecorded = h.DateRecorded,
                RecorderInitials = h.Recorder.Initials,
                GridReference = h.Location.FullGridRef,
                LocationName = h.Location.LocationName,
                VerificationStatus = h.VerificationStatus.Status
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<HarvestMouseNest1KmExportDto>> GetNestCountBy1KmSquareAsync(CancellationToken cancellationToken = default)
    {
        var records = await _dbSet
            .AsNoTracking()
            .Include(h => h.Location)
            .Where(h => !h.IsDeleted)
            .Select(h => new
            {
                h.Location.Latitude,
                h.Location.Longitude,
                Year = h.DateRecorded.Year
            })
            .ToListAsync(cancellationToken);

        return records
            .Select(r => new
            {
                MonadSquare = OSGridReferenceHelper.ToMonadGridReference(r.Latitude, r.Longitude),
                r.Year
            })
            .GroupBy(r => new { r.MonadSquare, r.Year })
            .Select(g => new HarvestMouseNest1KmExportDto
            {
                MonadSquare = g.Key.MonadSquare,
                Year = g.Key.Year,
                NestCount = g.Count()
            })
            .OrderBy(r => r.Year)
            .ThenBy(r => r.MonadSquare)
            .ToList();
    }

    public async Task<IEnumerable<HarvestMouseNestMonadExportDto>> GetNestCountByMonadAsync(CancellationToken cancellationToken = default)
    {
        var records = await _dbSet
            .AsNoTracking()
            .Include(h => h.Location)
            .Where(h => !h.IsDeleted)
            .Select(h => new
            {
                h.Location.Latitude,
                h.Location.Longitude
            })
            .ToListAsync(cancellationToken);

        return records
            .Select(r => OSGridReferenceHelper.ToMonadGridReference(r.Latitude, r.Longitude))
            .GroupBy(monadSquare => monadSquare)
            .Select(g => new HarvestMouseNestMonadExportDto
            {
                MonadSquare = g.Key,
                NestCount = g.Count()
            })
            .OrderBy(r => r.MonadSquare)
            .ToList();
    }
}
