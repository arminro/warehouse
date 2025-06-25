using Microsoft.EntityFrameworkCore;

namespace Warehouse.Data
{
    public class EfCoreTransactionProvider(WarehouseContext context)
    {
        private readonly WarehouseContext _context = context;

        public async Task SaveAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public DbContext Context { get => _context; }
    }
}
