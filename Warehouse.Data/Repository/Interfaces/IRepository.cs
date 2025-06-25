using Microsoft.EntityFrameworkCore.Storage;
using Warehouse.Data.Infrastructure;

namespace Warehouse.Data.Repository.Interfaces
{
    /// <summary>
    /// A generic repository interface for CRUD operations on entities that implement IDbEntry. The repository assumes a Unit of Work pattern. 
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IRepository<TEntity>
        where TEntity : IDbEntry
    {
        void Add(TEntity entity);

        Task UpdateAsync(TEntity entity);

        // here we make the assumption that the ID is always integer, for simplicity
        Task<TEntity> DeleteAsync(int id);

        Task<TEntity> SoftDeleteAsync(int id);

        Task<TEntity> GetByIdAsync(int id);

        IQueryable<TEntity> GetMultipleReadOnly();
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task<List<TEntity>> BulkSoftDelete(IEnumerable<int> ids);
    }
}
