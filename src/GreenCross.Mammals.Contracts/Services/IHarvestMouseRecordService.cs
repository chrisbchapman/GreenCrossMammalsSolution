using GreenCross.Mammals.Contracts.Dtos;
using GreenCross.Mammals.Entities;

namespace GreenCross.Mammals.Contracts.Services;

public interface IHarvestMouseRecordService : IBaseService<HarvestMouseRecord>
{
    Task<IEnumerable<HarvestMouseRecordGridDto>> GetRecordsForGridAsync(CancellationToken cancellationToken = default);
}
