using Unity.Entities;

namespace Sample2
{
    public struct SpawnData : IComponentData
    {
        public Entity Entity;
        public int SpawnCount;
    }
}