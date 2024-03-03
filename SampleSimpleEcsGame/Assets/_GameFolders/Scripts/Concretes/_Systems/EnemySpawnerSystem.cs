using EcsGame.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace EcsGame.Systems
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct EnemySpawnerSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EndInitializationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<EnemySpawnData>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;
            var entityCommandBufferSystem =
                SystemAPI.GetSingleton<EndInitializationEntityCommandBufferSystem.Singleton>();
            var entityCommandBuffer = entityCommandBufferSystem.CreateCommandBuffer(state.WorldUnmanaged);

            foreach (var (enemySpawnData, randomData, enemySpawnPositionReference, enemySpawnEntityBuffers, entity) in SystemAPI.Query<RefRW<EnemySpawnData>, RefRW<RandomData>, RefRO<EnemySpawnPositionReference>, DynamicBuffer<EnemySpawnEntityBuffer>>().WithEntityAccess())
            {
                enemySpawnData.ValueRW.CurrentTime += deltaTime;
                if(enemySpawnData.ValueRO.CurrentTime < enemySpawnData.ValueRO.MaxTime) continue;
                enemySpawnData.ValueRW.CurrentTime = 0f;

                var positionLength = enemySpawnPositionReference.ValueRO.BlobAssetReference.Value.Values.Length;
                var randomPositionIndex = randomData.ValueRW.RandomValue.NextInt(0, positionLength);
                var randomPosition =
                    enemySpawnPositionReference.ValueRO.BlobAssetReference.Value.Values[randomPositionIndex];
                
                var entityLength = enemySpawnEntityBuffers.Length;
                var randomEntityIndex = randomData.ValueRW.RandomValue.NextInt(0, entityLength);
                var newEntity = entityCommandBuffer.Instantiate(enemySpawnEntityBuffers[randomEntityIndex].EnemyEntity);
                
                entityCommandBuffer.SetComponent(newEntity, new LocalTransform()
                {
                    Position = randomPosition,
                    Rotation = quaternion.identity,
                    Scale = 1f
                });

                enemySpawnData.ValueRW.CurrentCount++;

                if (enemySpawnData.ValueRO.CurrentCount >= enemySpawnData.ValueRO.MaxCount)
                {
                    entityCommandBuffer.RemoveComponent<EnemySpawnData>(entity);
                }
            }
        }
    }
}