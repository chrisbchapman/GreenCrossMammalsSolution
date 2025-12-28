using GreenCross.Mammals.Contracts.Data;
using GreenCross.Mammals.Contracts.Repositories;
using GreenCross.Mammals.Contracts.Services;
using GreenCross.Mammals.Entities;
using Microsoft.Extensions.Logging;

namespace GreenCross.Mammals.BLL;

public class LocationService : BaseService<Location>, ILocationService
{
    public LocationService(
        ILocationRepository repository,
        IUnitOfWork unitOfWork,
        ILogger<LocationService> logger)
        : base(repository, unitOfWork, logger)
    {
    }

    // All CRUD methods are now inherited from BaseService<Location>
    // Override only if you need custom behavior for specific operations
}
