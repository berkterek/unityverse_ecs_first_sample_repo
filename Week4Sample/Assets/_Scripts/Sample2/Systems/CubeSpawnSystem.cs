using _Scripts.Sample2.Aspects;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Sample2
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct CubeSpawnSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EndInitializationEntityCommandBufferSystem.Singleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            var ecbSystem = SystemAPI.GetSingleton<EndInitializationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSystem.CreateCommandBuffer(state.WorldUnmanaged);

            new CubeSpawnJob
            {
                Ecb = ecb.AsParallelWriter(),
                DeltaTime = deltaTime
            }.ScheduleParallel();
        }
    }

    [BurstCompile]
    public partial struct CubeSpawnJob : IJobEntity
    {
        public float DeltaTime;
        public EntityCommandBuffer.ParallelWriter Ecb;
        
        [BurstCompile]
        private void Execute(CubeSpawnAspect aspect, [ChunkIndexInQuery]int sortKey)
        {
            if (!aspect.CanSpawn(DeltaTime)) return;
            
            aspect.SpawnProcess(Ecb, sortKey);
        }
    } 
}