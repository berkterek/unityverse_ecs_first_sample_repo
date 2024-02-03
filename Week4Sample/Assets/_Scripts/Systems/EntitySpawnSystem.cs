using _Scripts.Aspects;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Sample1
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct EntitySpawnSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EndInitializationEntityCommandBufferSystem.Singleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;
            var entityCommandBufferSystem =
                SystemAPI.GetSingleton<EndInitializationEntityCommandBufferSystem.Singleton>();
            var ecb = entityCommandBufferSystem.CreateCommandBuffer(state.WorldUnmanaged);
            // var entityCommandBuffer = new EntityCommandBuffer(Allocator.Temp);
            // foreach (var (spawnData, spawnEntityBufferDatas, randomData) in SystemAPI.Query<RefRW<SpawnData>, DynamicBuffer<SpawnEntityBufferData>, RefRW<RandomData>>())
            // {
            //     spawnData.ValueRW.CurrentTime += deltaTime;
            //     
            //     if(spawnData.ValueRO.CurrentTime < spawnData.ValueRO.MaxSpawnTime) continue;
            //
            //     spawnData.ValueRW.CurrentTime = 0f;
            //
            //     var newEntity = entityCommandBuffer.Instantiate(spawnEntityBufferDatas[randomData.ValueRW.Random.NextInt(0, spawnEntityBufferDatas.Length)].Entity);
            //     entityCommandBuffer.SetComponent(newEntity, new LocalTransform()
            //     {
            //         Position = spawnData.ValueRO.SpawnPosition,
            //         Rotation = quaternion.identity,
            //         Scale = 1f
            //     });
            // }
            //
            // entityCommandBuffer.Playback(state.EntityManager);
            // entityCommandBuffer.Dispose();

            new EntitySpawnJob()
            {
                DeltaTime = deltaTime,
                Ecb = ecb.AsParallelWriter()
            }.ScheduleParallel();
        }
    }

    [BurstCompile]
    public partial struct EntitySpawnJob : IJobEntity
    {
        public float DeltaTime;
        public EntityCommandBuffer.ParallelWriter Ecb;
        
        [BurstCompile]
        private void Execute(EntitySpawnAspect aspect, [ChunkIndexInQuery]int sortKey)
        {
            if (!aspect.CanSpawn(DeltaTime)) return;
            
            aspect.SpawnProcess(Ecb, sortKey);
        }
    }
}