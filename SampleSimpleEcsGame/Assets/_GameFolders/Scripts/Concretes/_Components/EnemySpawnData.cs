using Unity.Entities;

namespace EcsGame.Components
{
    public struct EnemySpawnData : IComponentData
    {
        public float CurrentTime;
        public float MaxTime;
        public int CurrentCount;
        public int MaxCount;
    }
}