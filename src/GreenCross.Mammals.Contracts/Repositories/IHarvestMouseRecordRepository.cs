using GreenCross.Mammals.Contracts.Dtos;
using GreenCross.Mammals.Entities;

namespace GreenCross.Mammals.Contracts.Repositories;

public interface IHarvestMouseRecordRepository : IRepository<HarvestMouseRecord>
{
    Task<IEnumerable<HarvestMouseRecordGridDto>> GetRecordsForGridAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<HarvestMouseNest1KmExportDto>> GetNestCountBy1KmSquareAsync(CancellationToken cancellationToken = default);
}
