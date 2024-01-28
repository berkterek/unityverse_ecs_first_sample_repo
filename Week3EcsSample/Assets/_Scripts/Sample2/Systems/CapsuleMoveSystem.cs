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
            state.RequireForUpdate<EndSimulationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<MoveTag>();
        }

        // [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;
            var entityCommandBufferSystem = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
            var entityCommandBuffer = entityCommandBufferSystem.CreateCommandBuffer(state.WorldUnmanaged);
            // var entityCommandBuffer = new EntityCommandBuffer(Allocator.Temp);
            // foreach (var (moveTag, moveData, localTransform, entity) in SystemAPI.Query<RefRO<MoveTag>, RefRW<MoveData>, RefRW<LocalTransform>>().WithEntityAccess())
            // {
            //     Debug.Log("Move State");
            //     float3 direction = new float3(0f, 0f, 1f);
            //     localTransform.ValueRW.Position += deltaTime * moveData.ValueRO.MoveSpeed * direction;
            //
            //     moveData.ValueRW.Timer += deltaTime;
            //
            //     if (moveData.ValueRO.Timer < 3f) return;
            //
            //     moveData.ValueRW.Timer = 0f;
            //     
            //     entityCommandBuffer.SetComponentEnabled<MoveTag>(entity, false);
            //     entityCommandBuffer.SetComponentEnabled<AttackTag>(entity, true);
            // }
            //
            // entityCommandBuffer.Playback(state.EntityManager);
            // entityCommandBuffer.Dispose();

            var job = new CapsuleMoveJob()
            {
                // Ecb = entityCommandBuffer, // run ve schedule uzerinde calisir
                Ecb = entityCommandBuffer.AsParallelWriter(),
                DeltaTime = deltaTime
            };
            
            //ikisi standart calisir
            //job.Run();
            //job.Schedule();
          
            //ustteki gibi run ve schedule gibi calismaz bunun nedeni ecb yani entitycommandbufferdan kaynaklidir burda daha ozel bir ecb bizden ister
            job.ScheduleParallel();
        }
    }

    public partial struct CapsuleMoveJob : IJobEntity
    {
        public float DeltaTime;
        // public EntityCommandBuffer Ecb; //run ve schedule icin normal calisir
        public EntityCommandBuffer.ParallelWriter Ecb; // ScheduleParallel icin bu paralle writer tipinde gondermemiz gerekir
        
        private void Execute(Entity entity, in MoveTag moveTag, ref MoveData moveData, ref LocalTransform localTransform, [ChunkIndexInQuery]int sortKey)
        {
            float3 direction = new float3(0f, 0f, 1f);
            localTransform.Position += moveData.MoveSpeed * DeltaTime * direction;

            moveData.Timer += DeltaTime;

            if (moveData.Timer < 3f) return;

            moveData.Timer = 0f;
            
            // Ecb.SetComponentEnabled<MoveTag>(entity, false); //run, schedule
            // Ecb.SetComponentEnabled<AttackTag>(entity, true);//run, schedule
            Ecb.SetComponentEnabled<MoveTag>(sortKey,entity, false); //parallel schedule
            Ecb.SetComponentEnabled<AttackTag>(sortKey,entity, true); //parallel schedule
        }
    }
}