using Warehouse.Data.Model;
using Warehouse.Service.DTO;
using Warehouse.Service.Interfaces;

namespace Warehouse.Service.Implementations
{
    public class WarehouseMapper : IBusinessModelToEntityModelMapper<BuildingComponentTypeDto, BuildingComponentType>, IBusinessModelToEntityModelMapper<BuildingComponentDto, BuildingComponent>, IBusinessModelToEntityModelMapper<WarehouseStateChangeResponseDto, WarehouseStateChange>
    {
        public BuildingComponentType Map(BuildingComponentTypeDto businessModel)
        {
            if (businessModel is null)
            {
                return new BuildingComponentType();
            }

            return new()
            {
                Id = businessModel.Id,
                Name = businessModel.Name,
                PriceInHungarianForints = businessModel.PriceInHungarianForints,
                Description = businessModel.Description,
                MassInGrams = businessModel.MassInGrams,
            };

        }

        public BuildingComponentTypeDto Map(BuildingComponentType entityModel)
        {
            if (entityModel is null)
            {
                return new BuildingComponentTypeDto();
            }

            return new()
            {
                Id = entityModel.Id,
                Name = entityModel.Name,
                PriceInHungarianForints = entityModel.PriceInHungarianForints,
                Description = entityModel.Description,
                MassInGrams = entityModel.MassInGrams,
                Components = entityModel.Components?.Select(c => new BuildingComponentDto
                {
                    ComponentId = c.Id,
                    CatalogId = c.CatalogNumber,
                    ComponentTypeId = c.ComponentType.Id
                }).ToList() ?? new List<BuildingComponentDto>()
            };
        }

        public BuildingComponent Map(BuildingComponentDto businessModel)
        {
            if (businessModel is null)
            {
                return new BuildingComponent();
            }

            return new()
            {
                ComponentTypeId = businessModel.ComponentTypeId,
                CatalogNumber = businessModel.CatalogId,
                Id = businessModel.ComponentId
            };
        }

        public BuildingComponentDto Map(BuildingComponent entityModel)
        {
            if (entityModel is null)
            {
                return new BuildingComponentDto();
            }

            return new()
            {
                CatalogId = entityModel.CatalogNumber,
                ComponentId = entityModel.Id,
                ComponentTypeId = entityModel.ComponentType.Id
            };
        }

        public WarehouseStateChange Map(WarehouseStateChangeResponseDto businessModel)
        {
            throw new NotImplementedException(); // this is intentionally left unimplemented
        }

        public WarehouseStateChangeResponseDto Map(WarehouseStateChange entityModel)
        {
            return new WarehouseStateChangeResponseDto
            {
                ChangeTimestamp = entityModel.ChangeTimestamp,
                ChangedElement = GetChangedElement(entityModel),
            };
        }

        private object GetChangedElement(WarehouseStateChange entityModel)
        {
            if (entityModel.Component is not null)
            {
                return entityModel.Component;
            }

            return entityModel.ComponentType;
        }
    }
}
