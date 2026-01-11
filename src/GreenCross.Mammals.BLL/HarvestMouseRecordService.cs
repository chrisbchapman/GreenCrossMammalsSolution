using GreenCross.Mammals.Contracts.Data;
using GreenCross.Mammals.Contracts.Dtos;
using GreenCross.Mammals.Contracts.Repositories;
using GreenCross.Mammals.Contracts.Services;
using GreenCross.Mammals.Entities;
using Microsoft.Extensions.Logging;

namespace GreenCross.Mammals.BLL;

public class HarvestMouseRecordService : BaseService<HarvestMouseRecord>, IHarvestMouseRecordService
{
    private readonly IHarvestMouseRecordRepository _harvestMouseRecordRepository;

    public HarvestMouseRecordService(
        IHarvestMouseRecordRepository repository,
        IUnitOfWork unitOfWork,
        ILogger<HarvestMouseRecordService> logger)
        : base(repository, unitOfWork, logger)
    {
        _harvestMouseRecordRepository = repository;
    }

    public async Task<IEnumerable<HarvestMouseRecordGridDto>> GetRecordsForGridAsync(CancellationToken cancellationToken = default)
    {
        return await _harvestMouseRecordRepository.GetRecordsForGridAsync(cancellationToken);
    }
}
