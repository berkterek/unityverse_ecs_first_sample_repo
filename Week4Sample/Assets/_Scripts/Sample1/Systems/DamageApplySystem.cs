using Unity.Burst;
using Unity.Entities;

namespace Sample1
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateAfter(typeof(DamageSystem))]
    public partial struct DamageApplySystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (healthData, damageBufferDatas) in SystemAPI.Query<RefRW<HealthData>, DynamicBuffer<DamageBufferData>>())
            {
                foreach (var damageBufferData in damageBufferDatas)
                {
                    healthData.ValueRW.CurrentHealth -= damageBufferData.Damage;
                }
                
                damageBufferDatas.Clear();
            }
        }
    }

    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateAfter(typeof(DamageApplySystem))]
    public partial struct DestroyHealthZeroEntitySystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecbSystem = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSystem.CreateCommandBuffer(state.WorldUnmanaged);
            foreach (var (healthData, entity) in SystemAPI.Query<RefRO<HealthData>>().WithEntityAccess())
            {
                if(healthData.ValueRO.CurrentHealth > 0f) continue;
                
                ecb.DestroyEntity(entity);
            }
        }
    }
}