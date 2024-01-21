using Unity.Entities;

namespace Sample_2_Scripts
{
    public struct SpawnerTimeData : IComponentData
    {
        public float MaxTime;
        public float CurrentTime;
    }
}