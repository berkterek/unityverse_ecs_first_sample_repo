using EcsGame.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace EcsGame.Systems
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateAfter(typeof(TransformSystemGroup))]
    public partial struct ApplyDamageSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            new ApplyDamageJob().ScheduleParallel();
        }
    }

    [BurstCompile]
    public partial struct ApplyDamageJob : IJobEntity
    {
        private void Execute(ref HealthData healthData, DynamicBuffer<DamageBuffer> damageBuffers)
        {
            foreach (var damageBuffer in damageBuffers)
            {
                healthData.CurrentHealth -= math.max(0f,damageBuffer.DamageValue);
                healthData.OnValueChanged = true;
            }
                
            damageBuffers.Clear();
        }
    }
}