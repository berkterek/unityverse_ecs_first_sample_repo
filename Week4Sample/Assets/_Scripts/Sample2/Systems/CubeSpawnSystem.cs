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
            foreach (var (spawnTime, spawnEntityData) in SystemAPI.Query<RefRW<SpawnTimeData>, RefRW<SpawnEntityData>>())
            {
                spawnTime.ValueRW.CurrentSpawnTime += deltaTime;
                if(spawnTime.ValueRO.CurrentSpawnTime < spawnTime.ValueRO.MaxSpawnTime) continue;
                
                spawnTime.ValueRW.CurrentSpawnTime = 0f;

                var newEntity = ecb.Instantiate(spawnEntityData.ValueRW.Entity);
                ecb.SetComponent(newEntity, new LocalTransform()
                {
                    Position = float3.zero,
                    Rotation = quaternion.identity,
                    Scale = 1f
                });
            }
        }
    }
}