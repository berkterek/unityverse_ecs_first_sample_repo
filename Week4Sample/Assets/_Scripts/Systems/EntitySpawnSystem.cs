using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Sample1
{
    public partial struct EntitySpawnSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;
            var entityCommandBuffer = new EntityCommandBuffer(Allocator.Temp);
            foreach (var (spawnData, spawnEntityBufferDatas, randomData) in SystemAPI.Query<RefRW<SpawnData>, DynamicBuffer<SpawnEntityBufferData>, RefRW<RandomData>>())
            {
                spawnData.ValueRW.CurrentTime += deltaTime;
                
                if(spawnData.ValueRO.CurrentTime < spawnData.ValueRO.MaxSpawnTime) continue;
            
                spawnData.ValueRW.CurrentTime = 0f;
            
                var newEntity = entityCommandBuffer.Instantiate(spawnEntityBufferDatas[randomData.ValueRW.Random.NextInt(0, spawnEntityBufferDatas.Length)].Entity);
                entityCommandBuffer.SetComponent(newEntity, new LocalTransform()
                {
                    Position = spawnData.ValueRO.SpawnPosition,
                    Rotation = quaternion.identity,
                    Scale = 1f
                });
            }
            
            entityCommandBuffer.Playback(state.EntityManager);
            entityCommandBuffer.Dispose();
        }
    }
}