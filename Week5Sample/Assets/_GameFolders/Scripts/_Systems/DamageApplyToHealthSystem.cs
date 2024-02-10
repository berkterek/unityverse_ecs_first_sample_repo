using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace SampleScripts
{
    [BurstCompile]
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateAfter(typeof(TransformSystemGroup))]
    public partial struct DamageApplyToHealthSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            new DamageApplyToHealthJob().ScheduleParallel();
        }
    }

    [BurstCompile]
    public partial struct DamageApplyToHealthJob : IJobEntity
    {
        [BurstCompile]
        void Execute(Entity entity, ref HealthData healthData, DynamicBuffer<DamageBufferData> damageBufferDatas)
        {
            foreach (var damageBufferData in damageBufferDatas)
            {
                healthData.CurrentHealth -= damageBufferData.Value;
            }
            
            damageBufferDatas.Clear();
        }
    }
}