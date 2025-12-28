using GreenCross.Mammals.Contracts.Data;
using GreenCross.Mammals.Contracts.Repositories;
using GreenCross.Mammals.Contracts.Services;
using GreenCross.Mammals.Entities;
using Microsoft.Extensions.Logging;

namespace GreenCross.Mammals.BLL;

public class RecorderService : BaseService<Recorder>, IRecorderService
{
    public RecorderService(
        IRecorderRepository repository,
        IUnitOfWork unitOfWork,
        ILogger<RecorderService> logger)
        : base(repository, unitOfWork, logger)
    {
    }

    // All CRUD methods are now inherited from BaseService<Recorder>
    // Override only if you need custom behavior for specific operations
}
