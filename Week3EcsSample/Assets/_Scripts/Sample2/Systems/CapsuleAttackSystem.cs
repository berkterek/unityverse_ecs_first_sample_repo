using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace Sample2
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    public partial struct CapsuleAttackSystem : ISystem
    {
        //[BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<AttackTag>();
        }

        //[BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;
            var entityCommandBuffer = new EntityCommandBuffer(Allocator.Temp);
            foreach (var (attackTag, attackData, entity) in SystemAPI.Query<RefRO<AttackTag>,RefRW<AttackData>>().WithDisabled<MoveTag>().WithEntityAccess())
            {
                Debug.Log("Attack State");
                attackData.ValueRW.Timer += deltaTime;

                if (attackData.ValueRO.Timer < 3f) return;

                attackData.ValueRW.Timer = 0f;
                
                entityCommandBuffer.SetComponentEnabled<AttackTag>(entity, false);
                entityCommandBuffer.SetComponentEnabled<MoveTag>(entity, true);
            }
            
            entityCommandBuffer.Playback(state.EntityManager);
            entityCommandBuffer.Dispose();
        }
    }
}