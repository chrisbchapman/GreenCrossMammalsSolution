using GreenCross.App.Entities;

namespace GreenCross.App.Contracts.Repositories
{
    public interface IFirstEntityRepository
    {
        Task<FirstEntity> AddAsync(FirstEntity entity, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
        Task<FirstEntity> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        void Update(FirstEntity entity);
        Task UpdateAsync(FirstEntity entity, CancellationToken cancellationToken);
    }
}
