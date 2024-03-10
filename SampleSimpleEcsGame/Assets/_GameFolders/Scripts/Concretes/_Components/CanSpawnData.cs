using Unity.Entities;

namespace EcsGame.Components
{
    public struct CanSpawnData : IComponentData
    {
        public bool CanSpawn;
    }
}