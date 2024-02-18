using Unity.Entities;

namespace EcsGame.Components
{
    public struct SpawnData : IComponentData
    {
        public Entity Entity;
        public float CurrentSpawnTime;
        public float MaxSpawnTime;
    }
}