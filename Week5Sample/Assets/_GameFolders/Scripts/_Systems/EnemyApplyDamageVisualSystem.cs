using Unity.Entities;

namespace SampleScripts
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateAfter(typeof(DamageApplyToHealthSystem))]
    public partial struct EnemyApplyDamageVisualSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (enemyTag,healthData, entity) in SystemAPI.Query<RefRO<EnemyTag>, RefRO<HealthData>>().WithEntityAccess())
            {
                if (SystemAPI.ManagedAPI.HasComponent<EnemyVisualObjectReference>(entity))
                {
                    var enemyVisualObjectReference = SystemAPI.ManagedAPI.GetComponent<EnemyVisualObjectReference>(entity);
                    enemyVisualObjectReference.EnemyVisualController.SetHealthValues(healthData.ValueRO.CurrentHealth,healthData.ValueRO.MaxHealth);
                }
            }
        }
    }
}