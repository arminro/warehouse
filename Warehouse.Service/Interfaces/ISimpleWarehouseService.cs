namespace Warehouse.Service.Interfaces
{
    /// <summary>
    /// A simple service for managing CRUD operations. <br/> For the sake of simplicity, this service is working with a single business object instead of spearating them into Request-Response pairs.
    /// </summary>
    /// <typeparam name="TBusinessObject"></typeparam>
    public interface ISimpleWarehouseService<TBusinessObject>
        where TBusinessObject : class
    {
        Task AddAsync(TBusinessObject model);

        Task<TBusinessObject> UpdateAsync(TBusinessObject model);

        Task SoftDeleteAsync(int id);

        // we use a collection becuase we want to indicate that the order of elements are by no means guaranteed, the readonly part indicates that these are not intended to be modified by the consumer
        Task<IReadOnlyCollection<TBusinessObject>> GetPaginatedListAsync(int pageNumber = 1, int pageSize = 10);
    }
}
