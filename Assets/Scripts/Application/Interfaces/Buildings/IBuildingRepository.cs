using Domain.Models.Buildings;
using System.Collections.Generic;

namespace Application.Interfaces.Buildings
{
    public interface IBuildingRepository
    {
        IBuildingConfig Get(BuildingType type);
        IReadOnlyDictionary<BuildingType, IBuildingConfig> GetAll();
    }
}