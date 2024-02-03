using Unity.Entities;

namespace Sample1
{
    public struct HealthData : IComponentData
    {
        public float CurrentHealth;
        public float MaxHealth;
    }

    public struct DamageData : IComponentData
    {
        public float Damage;
    }
}