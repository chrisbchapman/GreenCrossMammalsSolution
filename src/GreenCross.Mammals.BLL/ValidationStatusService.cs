using GreenCross.Mammals.Contracts.Data;
using GreenCross.Mammals.Contracts.Repositories;
using GreenCross.Mammals.Contracts.Services;
using GreenCross.Mammals.Entities;
using Microsoft.Extensions.Logging;

namespace GreenCross.Mammals.BLL;

public class ValidationStatusService : BaseService<ValidationStatus>, IValidationStatusService
{
    public ValidationStatusService(
        IValidationStatusRepository repository,
        IUnitOfWork unitOfWork,
        ILogger<ValidationStatusService> logger)
        : base(repository, unitOfWork, logger)
    {
    }

}
