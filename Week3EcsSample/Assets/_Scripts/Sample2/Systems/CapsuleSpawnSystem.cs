using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace Sample2
{
    [BurstCompile]
    [UpdateInGroup(typeof(InitializationSystemGroup),OrderLast = true)]
    public partial struct CapsuleSpawnSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var entityCommandBuffer = new EntityCommandBuffer(Allocator.Temp);
            foreach (var (spawnData,entity) in SystemAPI.Query<RefRW<SpawnData>>().WithEntityAccess())
            {
                for (int i = 0; i < spawnData.ValueRO.SpawnCount; i++)
                {
                    var newEntity = entityCommandBuffer.Instantiate(spawnData.ValueRW.Entity);
                    entityCommandBuffer.SetComponentEnabled<AttackTag>(newEntity, false);
                }
                
                //entity destroy dedigmiz bizim spawn yapan entity'dir bunu yok etmis oluyoruz tekrar spanw islemi gerceklestirmesin diye cunku entity yoksa system calismaz zaten
                entityCommandBuffer.DestroyEntity(entity);
            }
            
            entityCommandBuffer.Playback(state.EntityManager);
            entityCommandBuffer.Dispose();
            //state.Enabled = false; //state.Enable = false bu sistemi kapatir
        }
    }
}