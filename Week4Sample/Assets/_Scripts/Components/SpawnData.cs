using Unity.Entities;
using Unity.Mathematics;

namespace Sample1
{
    public struct SpawnData : IComponentData
    {
        public float MaxSpawnTime;
        public float CurrentTime;
        public float3 SpawnPosition;
    }

    public struct SpawnEntityData : IComponentData
    {
        public Entity Entity;
    }
}