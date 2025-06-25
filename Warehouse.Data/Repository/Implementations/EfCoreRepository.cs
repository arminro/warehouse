using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Warehouse.Common.Exceptions;
using Warehouse.Data.Infrastructure;
using Warehouse.Data.Repository.Interfaces;

namespace Warehouse.Data.Repository.Implementations
{
    /// <summary>
    /// A generic repository base class based on Entity Framework Core. Here we make use of the fact that EfCore supports the Unit of Work pattern by default.
    /// </summary>
    /// <typeparam name="TEntity">The type of the Entity.</typeparam>
    public class EfCoreRepository<TEntity>(EfCoreTransactionProvider provider) : IRepository<TEntity>
        where TEntity : DbEntity
    {
        private readonly EfCoreTransactionProvider _provider = provider;

        public async Task<IDbContextTransaction> BeginTransactionAsync()
            => await _provider.Context.Database.BeginTransactionAsync();

        public void Add(TEntity entity)
        {
            _provider.Context.Attach(entity);
            _provider.Context
                .Set<TEntity>()
                .Add(entity);
        }

        public async Task<TEntity> DeleteAsync(int id)
        {
            var entry = await GetByIdAsync(id)
                ?? throw new EntryNotFoundException(id.ToString());

            _provider
                .Context
                .Set<TEntity>()
                .Remove(entry);

            return entry;
        }

        public IQueryable<TEntity> GetMultipleReadOnly()
        {
            return _provider
                .Context.Set<TEntity>()
                .AsNoTracking();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _provider
                    .Context
                    .Set<TEntity>()
                    .FindAsync(id);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            // an efcore update is fine without an original entity existing
            // but we do not want to break the assumption of idempotency on the UPDATE verb by creating a new entity if it does not
            var exists = await _provider
                .Context
                .Set<TEntity>()
                .AnyAsync(e => e.Id == entity.Id);

            if (!exists)
            {
                throw new EntryNotFoundException(entity.Id.ToString());
            }

            _provider.Context
                .Set<TEntity>()
                .Update(entity);
        }

        public async Task<TEntity> SoftDeleteAsync(int id)
        {
            var entry = await GetByIdAsync(id)
               ?? throw new EntryNotFoundException(id.ToString());

            entry.IsDeleted = true;

            return entry;
        }

        public async Task<List<TEntity>> BulkSoftDelete(IEnumerable<int> ids)
        {
            var elementsToDelete = await _provider.Context
             .Set<TEntity>()
             .Where(e => ids.Contains(e.Id))
             .ToListAsync();

            elementsToDelete
                .ForEach(e => e.IsDeleted = true);

            return elementsToDelete;
        }
    }
}
