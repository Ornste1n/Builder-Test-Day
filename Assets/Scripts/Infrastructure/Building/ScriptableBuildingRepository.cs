using Repositories;
using Domain.Models.Buildings;
using System.Collections.Generic;
using Application.Interfaces.Buildings;

namespace Infrastructure.Building
{
    public class ScriptableBuildingRepository : IBuildingRepository
    {
        private readonly BuildingCatalogConfig _buildingsConfig;

        public ScriptableBuildingRepository(BuildingCatalogConfig buildingsConfig)
        {
            _buildingsConfig = buildingsConfig;
        }

        public IBuildingConfig Get(BuildingType type)
        {
            return _buildingsConfig.Catalog.GetValueOrDefault(type);
        }

        public IReadOnlyDictionary<BuildingType, IBuildingConfig> GetAll()
        {
            return _buildingsConfig.Catalog;
        }
    }
}