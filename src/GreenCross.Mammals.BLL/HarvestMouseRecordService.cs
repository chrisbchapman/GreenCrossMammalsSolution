using GreenCross.Mammals.Contracts.Data;
using GreenCross.Mammals.Contracts.Repositories;
using GreenCross.Mammals.Contracts.Services;
using GreenCross.Mammals.Entities;
using Microsoft.Extensions.Logging;

namespace GreenCross.Mammals.BLL;

public class HarvestMouseRecordService : BaseService<HarvestMouseRecord>, IHarvestMouseRecordService
{
    public HarvestMouseRecordService(
        IHarvestMouseRecordRepository repository,
        IUnitOfWork unitOfWork,
        ILogger<HarvestMouseRecordService> logger)
        : base(repository, unitOfWork, logger)
    {
    }

}
