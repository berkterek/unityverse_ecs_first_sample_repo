using Unity.Entities;

namespace EcsGame.Components
{
    public struct DamageData : IComponentData
    {
        public float Damage;
        public float CurrentTime;
        public float MaxTime;
    }
}