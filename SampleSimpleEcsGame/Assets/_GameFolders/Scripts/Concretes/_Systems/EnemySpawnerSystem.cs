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

            new EnemySpawnerJob()
            {
                Ecb = entityCommandBuffer.AsParallelWriter(),
                DeltaTime = deltaTime
            }.ScheduleParallel();
        }
    }

    [BurstCompile]
    public partial struct EnemySpawnerJob : IJobEntity
    {
        public float DeltaTime;
        public EntityCommandBuffer.ParallelWriter Ecb;

        [BurstCompile]
        private void Execute(Entity entity, ref EnemySpawnData enemySpawnData,
            in EnemySpawnPositionReference enemySpawnPositionReference,
            DynamicBuffer<EnemySpawnEntityBuffer> enemySpawnEntityBuffers, ref RandomData randomData,
            [ChunkIndexInQuery] int sortKey)
        {
            enemySpawnData.CurrentTime += DeltaTime;
            if (enemySpawnData.CurrentTime < enemySpawnData.MaxTime) return;
            enemySpawnData.CurrentTime = 0f;

            var positionLength = enemySpawnPositionReference.BlobAssetReference.Value.Values.Length;
            var randomPositionIndex = randomData.RandomValue.NextInt(0, positionLength);
            var randomPosition =
                enemySpawnPositionReference.BlobAssetReference.Value.Values[randomPositionIndex];

            var entityLength = enemySpawnEntityBuffers.Length;
            var randomEntityIndex = randomData.RandomValue.NextInt(0, entityLength);
            var newEntity = Ecb.Instantiate(sortKey, enemySpawnEntityBuffers[randomEntityIndex].EnemyEntity);

            Ecb.SetComponent(sortKey, newEntity, new LocalTransform()
            {
                Position = randomPosition,
                Rotation = quaternion.identity,
                Scale = 1f
            });

            enemySpawnData.CurrentCount++;

            if (enemySpawnData.CurrentCount >= enemySpawnData.MaxCount)
            {
                Ecb.RemoveComponent<EnemySpawnData>(sortKey, entity);
            }
        }
    }
}