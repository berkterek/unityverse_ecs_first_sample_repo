using EcsGame.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Physics;

namespace EcsGame.Systems
{
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    public partial struct CollectableOnTriggerSystem : ISystem
    {
        struct ComponentDataHandler
        {
            public ComponentLookup<CollectData> CollectDataLookup;
            public ComponentLookup<PlayerScoreData> PlayerScoreDataLookup;

            public ComponentDataHandler(ref SystemState state)
            {
                CollectDataLookup = state.GetComponentLookup<CollectData>();
                PlayerScoreDataLookup = state.GetComponentLookup<PlayerScoreData>();
            }

            public void Update(ref SystemState state)
            {
                CollectDataLookup.Update(ref state);
                PlayerScoreDataLookup.Update(ref state);
            }
        }

        ComponentDataHandler _componentDataHandler;

        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EndFixedStepSimulationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<SimulationSingleton>();
            _componentDataHandler = new ComponentDataHandler(ref state);
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var entityCommandBufferSystem =
                SystemAPI.GetSingleton<EndFixedStepSimulationEntityCommandBufferSystem.Singleton>();
            var entityCommandBuffer = entityCommandBufferSystem.CreateCommandBuffer(state.WorldUnmanaged);
            _componentDataHandler.Update(ref state);

            var job = new CollectableOnTriggerJob()
            {
                CollectDataLookup = _componentDataHandler.CollectDataLookup,
                PlayerScoreDataLookup = _componentDataHandler.PlayerScoreDataLookup,
                Ecb = entityCommandBuffer
            };

            var jobHandler = job.Schedule(SystemAPI.GetSingleton<SimulationSingleton>(), state.Dependency);
            state.Dependency = jobHandler;
        }
    }

    [BurstCompile]
    public partial struct CollectableOnTriggerJob : ITriggerEventsJob
    {
        public EntityCommandBuffer Ecb;
        public ComponentLookup<CollectData> CollectDataLookup;
        public ComponentLookup<PlayerScoreData> PlayerScoreDataLookup;

        [BurstCompile]
        public void Execute(TriggerEvent triggerEvent)
        {
            var entityA = triggerEvent.EntityA;
            var entityB = triggerEvent.EntityB;

            if (CollectDataLookup.HasComponent(entityA))
            {
                if (PlayerScoreDataLookup.HasComponent(entityB))
                {
                     var collectData= CollectDataLookup.GetRefRW(entityA);
                     int score = collectData.ValueRO.Score;
                     collectData.ValueRW.IsCollected = true;
                     
                     Ecb.AppendToBuffer(entityB, new PlayerScoreBuffer()
                     {
                         Value = score
                     });
                }
            }
            else if (CollectDataLookup.HasComponent(entityB))
            {
                if (PlayerScoreDataLookup.HasComponent(entityA))
                {
                    var collectData= CollectDataLookup.GetRefRW(entityB);
                    int score = collectData.ValueRO.Score;
                    collectData.ValueRW.IsCollected = true;
                     
                    Ecb.AppendToBuffer(entityA, new PlayerScoreBuffer()
                    {
                        Value = score
                    });
                }
            }
        }
    }
}