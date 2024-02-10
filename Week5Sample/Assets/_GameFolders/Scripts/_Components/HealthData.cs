using Unity.Entities;

namespace SampleScripts
{
    public struct HealthData : IComponentData
    {
        public float MaxHealth;
        public float CurrentHealth;
    }
}