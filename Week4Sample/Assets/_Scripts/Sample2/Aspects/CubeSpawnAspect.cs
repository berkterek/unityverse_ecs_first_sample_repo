using Sample2;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace _Scripts.Sample2.Aspects
{
    public readonly partial struct CubeSpawnAspect : IAspect
    {
        public readonly Entity Entity;
        readonly RefRW<SpawnTimeData> _spawnTimeDataRW;
        readonly RefRW<SpawnEntityData> _spawnEntityDataRW;
        readonly RefRW<RandomData> _randomDataRW;
        readonly RefRO<SpawnPositionsReference> _spawnPositionsReferenceRO;

        public bool CanSpawn(float deltaTime)
        {
            _spawnTimeDataRW.ValueRW.CurrentSpawnTime += deltaTime;
            return _spawnTimeDataRW.ValueRO.CurrentSpawnTime > _spawnTimeDataRW.ValueRO.MaxSpawnTime;
        }

        public void SpawnProcess(EntityCommandBuffer.ParallelWriter ecb, int sortKey)
        {
            _spawnTimeDataRW.ValueRW.CurrentSpawnTime = 0f;
            
            var newEntity = ecb.Instantiate(sortKey,_spawnEntityDataRW.ValueRW.Entity);
            var randomIndex = _randomDataRW.ValueRW.Random.NextInt(0,
                _spawnPositionsReferenceRO.ValueRO.BlobValueReference.Value.Values.Length);
            var position = _spawnPositionsReferenceRO.ValueRO.BlobValueReference.Value.Values[randomIndex];
            ecb.SetComponent(sortKey,newEntity, new LocalTransform()
            {
                Position = position,
                Rotation = quaternion.identity,
                Scale = 1f
            });
        }
    }
}