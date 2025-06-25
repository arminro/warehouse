namespace Warehouse.Service.Interfaces
{
    public interface IBusinessModelToEntityModelMapper<TBusinessModel, TEntityModel>
    {
        TEntityModel Map(TBusinessModel businessModel);

        TBusinessModel Map(TEntityModel entityModel);
    }
}
