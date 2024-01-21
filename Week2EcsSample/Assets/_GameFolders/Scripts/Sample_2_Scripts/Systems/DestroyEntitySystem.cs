using Unity.Entities;
using Unity.Transforms;

namespace Sample_2_Scripts
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateBefore(typeof(TransformSystemGroup))]
    public partial struct DestroyEntitySystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EndSimulationEntityCommandBufferSystem.Singleton>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var entityCommandBufferSystem = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
            var entityCommandBuffer = entityCommandBufferSystem.CreateCommandBuffer(state.WorldUnmanaged);
            //WithEntityAccess() ile biz query'lerimize entity'de ekle demis oluyoruz
            foreach (var (localTransform, moveData, entity) in SystemAPI.Query<RefRO<LocalTransform>, RefRO<MoveData>>().WithEntityAccess())
            {
                if(localTransform.ValueRO.Position.z < 10f) continue;
                
                entityCommandBuffer.DestroyEntity(entity);
            }
        }
    }
}