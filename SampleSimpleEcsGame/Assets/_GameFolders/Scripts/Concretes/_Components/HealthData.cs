using Unity.Entities;

namespace EcsGame.Components
{
    public struct HealthData : IComponentData
    {
        public float MaxHealth;
        public float CurrentHealth;
        public bool OnValueChanged;
    }
}