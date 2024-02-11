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
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EndSimulationEntityCommandBufferSystem.Singleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var entityCommandBufferSystem = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
            var entityCommandBuffer = entityCommandBufferSystem.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter();
            
            new DamageApplyToHealthJob()
            {
                Ecb = entityCommandBuffer
            }.ScheduleParallel();
        }
    }

    [BurstCompile]
    public partial struct DamageApplyToHealthJob : IJobEntity
    {
        public EntityCommandBuffer.ParallelWriter Ecb;
        
        [BurstCompile]
        void Execute(Entity entity, ref HealthData healthData, DynamicBuffer<DamageBufferData> damageBufferDatas, [ChunkIndexInQuery] int sortKey)
        {
            foreach (var damageBufferData in damageBufferDatas)
            {
                healthData.CurrentHealth -= damageBufferData.Value;
            }
            
            damageBufferDatas.Clear();

            if (healthData.CurrentHealth > 0) return;
            
            //Self Destroy
            Ecb.DestroyEntity(sortKey, entity);
        }
    }
}