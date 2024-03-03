using EcsGame.Components;
using Unity.Entities;

namespace EcsGame.Systems
{
    [UpdateInGroup(typeof(PresentationSystemGroup))]
    public partial class PlayerApplyDamageVisualSystem : SystemBase
    {
        public event System.Action<float, float> OnHealthChanged;
        
        protected override void OnUpdate()
        {
            foreach (var (playerTag, healthData) in SystemAPI.Query<RefRO<PlayerTag>, RefRW<HealthData>>())
            {
                if(!healthData.ValueRO.OnValueChanged) continue;

                healthData.ValueRW.OnValueChanged = false;
                OnHealthChanged?.Invoke(healthData.ValueRO.CurrentHealth, healthData.ValueRO.MaxHealth);
            }
        }
    }
}