using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Sample2
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateBefore(typeof(TransformSystemGroup))]
    public partial struct CapsuleMoveSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<MoveTag>();
        }

        // [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;
            var entityCommandBuffer = new EntityCommandBuffer(Allocator.Temp);
            foreach (var (moveTag, moveData, localTransform, entity) in SystemAPI.Query<RefRO<MoveTag>, RefRW<MoveData>, RefRW<LocalTransform>>().WithDisabled<AttackTag>().WithEntityAccess())
            {
                Debug.Log("Move State");
                float3 direction = new float3(0f, 0f, 1f);
                localTransform.ValueRW.Position += deltaTime * moveData.ValueRO.MoveSpeed * direction;

                moveData.ValueRW.Timer += deltaTime;

                if (moveData.ValueRO.Timer < 3f) return;

                moveData.ValueRW.Timer = 0f;
                
                entityCommandBuffer.SetComponentEnabled<MoveTag>(entity, false);
                entityCommandBuffer.SetComponentEnabled<AttackTag>(entity, true);
            }
            
            entityCommandBuffer.Playback(state.EntityManager);
            entityCommandBuffer.Dispose();
        }
    }
}