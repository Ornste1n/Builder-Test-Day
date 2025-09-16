using UnityEngine;
using Domain.Models.Buildings;
using Application.Interfaces.Buildings;

namespace Infrastructure.Building
{
    public class BuildingFactory : IBuildingFactory
    {
        private readonly Transform _parent;
        private readonly IBuildingRepository _repository;

        public BuildingFactory(IBuildingRepository repository, Transform parent = null)
        {
            _repository = repository;
            _parent = parent;
        }

        public void Create(BuildingType type, Vector3 worldPosition)
        {
            IBuildingConfig config = _repository.Get(type);
            GameObject go = Object.Instantiate(config.Prefab, worldPosition, Quaternion.identity, _parent);
            go.transform.localScale = config.Prefab.transform.localScale;
        }
    }
}