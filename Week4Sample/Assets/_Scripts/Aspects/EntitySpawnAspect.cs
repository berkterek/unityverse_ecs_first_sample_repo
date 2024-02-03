using Sample1;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace _Scripts.Aspects
{
    public readonly partial struct EntitySpawnAspect : IAspect
    {
        public readonly Entity Entity;
        readonly RefRW<RandomData> _randomDataRW;
        readonly RefRW<SpawnData> _spawnDataRW;
        readonly DynamicBuffer<SpawnEntityBufferData> _spawnEntityBufferDatas;

        public bool CanSpawn(float deltaTime)
        {
            _spawnDataRW.ValueRW.CurrentTime += deltaTime;

            return _spawnDataRW.ValueRO.CurrentTime > _spawnDataRW.ValueRO.MaxSpawnTime;
        }

        public void SpawnProcess(EntityCommandBuffer.ParallelWriter ecb, int sortKey)
        {
            _spawnDataRW.ValueRW.CurrentTime = 0f;

            int randomIndex = _randomDataRW.ValueRW.Random.NextInt(0, _spawnEntityBufferDatas.Length);
            var newEntity = ecb.Instantiate(sortKey, _spawnEntityBufferDatas[randomIndex].Entity);
            
            ecb.SetComponent(sortKey, newEntity,new LocalTransform()
            {
                Position = _spawnDataRW.ValueRO.SpawnPosition,
                Rotation = quaternion.identity,
                Scale = 1f
            });
        }
    }
}