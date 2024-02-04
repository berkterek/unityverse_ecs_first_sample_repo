using Unity.Entities;

namespace Sample2
{
    public struct SpawnTimeData : IComponentData
    {
        public float CurrentSpawnTime;
        public float MaxSpawnTime;
    }
}