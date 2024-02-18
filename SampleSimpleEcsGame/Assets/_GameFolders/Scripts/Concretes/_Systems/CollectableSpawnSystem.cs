using EcsGame.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace EcsGame.Systems
{
    [BurstCompile]
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct CollectableSpawnSystem : ISystem
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
            var entityCommandBufferSystem =
                SystemAPI.GetSingleton<EndInitializationEntityCommandBufferSystem.Singleton>();
            var entityCommandBuffer = entityCommandBufferSystem.CreateCommandBuffer(state.WorldUnmanaged);
            
            new CollectableSpawnJob()
            {
                Ecb = entityCommandBuffer.AsParallelWriter(),
                DeltaTime = deltaTime
            }.ScheduleParallel();
        }
    }

    [BurstCompile]
    public partial struct CollectableSpawnJob : IJobEntity
    {
        public EntityCommandBuffer.ParallelWriter Ecb;
        public float DeltaTime;
        
        [BurstCompile]
        private void Execute(Entity entity, ref SpawnData spawnData, in SpawnPositionReference spawnPositionReference, ref RandomData randomData, [ChunkIndexInQuery]int sortKey)
        {
            spawnData.CurrentSpawnTime += DeltaTime;

            if (spawnData.CurrentSpawnTime < spawnData.MaxSpawnTime) return;

            spawnData.CurrentSpawnTime = 0f;

            int randomIndex =
                randomData.RandomValue.NextInt(0, spawnPositionReference.BlobAssetReference.Value.Values.Length);
            var randomSpawnPosition = spawnPositionReference.BlobAssetReference.Value.Values[randomIndex];

            var collectableEntity = Ecb.Instantiate(sortKey, spawnData.Entity);
            
            Ecb.SetComponent(sortKey,collectableEntity, new LocalTransform()
            {
                Position = randomSpawnPosition,
                Rotation = randomData.RandomValue.NextFloat4(),
                Scale = 1f
            });
        }
    }
}