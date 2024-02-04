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
                Ecb = ecb,
                DeltaTime = deltaTime
            }.Schedule();
        }
    }

    [BurstCompile]
    public partial struct CubeSpawnJob : IJobEntity
    {
        public float DeltaTime;
        public EntityCommandBuffer Ecb;
        
        [BurstCompile]
        private void Execute(Entity entity, ref SpawnTimeData spawnTimeData, ref SpawnEntityData spawnEntityData, ref RandomData randomData, in SpawnPositionsReference spawnPositionsReference)
        {
            spawnTimeData.CurrentSpawnTime += DeltaTime;
            if (spawnTimeData.CurrentSpawnTime < spawnTimeData.MaxSpawnTime) return;

            spawnTimeData.CurrentSpawnTime = 0f;
            
            var newEntity = Ecb.Instantiate(spawnEntityData.Entity);
            var randomIndex = randomData.Random.NextInt(0,
                spawnPositionsReference.BlobValueReference.Value.Values.Length);
            var position = spawnPositionsReference.BlobValueReference.Value.Values[randomIndex];
            Ecb.SetComponent(newEntity, new LocalTransform()
            {
                Position = position,
                Rotation = quaternion.identity,
                Scale = 1f
            });
        }
    } 
}