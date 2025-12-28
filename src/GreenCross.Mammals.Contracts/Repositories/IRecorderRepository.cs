using GreenCross.Mammals.Entities;

namespace GreenCross.Mammals.Contracts.Repositories
{
    public interface IRecorderRepository : IRepository<Recorder>
    {
        Task<IEnumerable<Recorder>> GetWildlifeGroupMembersAsync(CancellationToken cancellationToken = default);
        Task<Recorder?> GetByInitialsAsync(string initials, CancellationToken cancellationToken = default);
    }
}
