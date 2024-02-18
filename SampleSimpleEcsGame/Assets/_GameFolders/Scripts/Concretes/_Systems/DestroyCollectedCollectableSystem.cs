using EcsGame.Components;
using Unity.Burst;
using Unity.Entities;

namespace EcsGame.Systems
{
    [BurstCompile]
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateBefore(typeof(ApplyScoreForPlayerSystem))]
    public partial struct DestroyCollectedCollectableSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EndSimulationEntityCommandBufferSystem.Singleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var entityCommandBufferSystem = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
            var entityCommandBuffer = entityCommandBufferSystem.CreateCommandBuffer(state.WorldUnmanaged);
            
            new DestroyCollectedCollectableJob()
            {
               Ecb = entityCommandBuffer.AsParallelWriter()
            }.ScheduleParallel();
        }
    }

    [BurstCompile]
    public partial struct DestroyCollectedCollectableJob : IJobEntity
    {
        public EntityCommandBuffer.ParallelWriter Ecb;
        
        [BurstCompile]
        private void Execute(Entity entity, in CollectData collectData, [ChunkIndexInQuery]int sortKey)
        {
            if (!collectData.IsCollected) return;
            
            Ecb.DestroyEntity(sortKey, entity);
        }
    }
}