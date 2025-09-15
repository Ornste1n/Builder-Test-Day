namespace Domain.Gameplay.Models.Buildings
{
    [System.Serializable]
    public struct BuildingLevel
    {
        public ResourceValue Cost;
        public ResourceValue Income;
    }

    [System.Serializable]
    public struct ResourceValue
    {
        public ResourceType Type;
        public int Value;
    }
}