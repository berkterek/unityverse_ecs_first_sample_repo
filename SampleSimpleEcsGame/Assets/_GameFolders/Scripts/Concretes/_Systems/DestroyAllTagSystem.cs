using EcsGame.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace EcsGame.Systems
{
    [BurstCompile]
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateAfter(typeof(TransformSystemGroup))]
    public partial struct DestroyAllTagSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EndSimulationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<GameStatusData>();
            state.RequireForUpdate<GameOverDestroyTag>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var gameStatusData = SystemAPI.GetSingleton<GameStatusData>();

            if (!gameStatusData.IsGameEnded) return;

            var entityCommandBufferSystem = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
            var entityCommandBuffer = entityCommandBufferSystem.CreateCommandBuffer(state.WorldUnmanaged);

            new DestroyAllTagJob()
            {
                Ecb = entityCommandBuffer.AsParallelWriter()
            }.ScheduleParallel();
        }
    }

    [BurstCompile]
    public partial struct DestroyAllTagJob : IJobEntity
    {
        public EntityCommandBuffer.ParallelWriter Ecb;
        
        [BurstCompile]
        private void Execute(Entity entity, in GameOverDestroyTag gameOverDestroyTag, [ChunkIndexInQuery]int sortKey)
        {
            Ecb.DestroyEntity(sortKey,entity);
        }
    }
}