using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace Sample1
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateAfter(typeof(TransformSystemGroup))]
    public partial struct DamageSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EndSimulationEntityCommandBufferSystem.Singleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;
            var ecbSystem = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSystem.CreateCommandBuffer(state.WorldUnmanaged);
            foreach (var (localTransform, damageBufferDatas, entity) in SystemAPI.Query<RefRO<LocalTransform>, DynamicBuffer<DamageBufferData>>().WithEntityAccess())
            {
                if(localTransform.ValueRO.Position.z < 5f) continue;

                ecb.AppendToBuffer<DamageBufferData>(entity, new DamageBufferData()
                {
                    Damage = deltaTime * 4f
                });
            }
        }
    }
}