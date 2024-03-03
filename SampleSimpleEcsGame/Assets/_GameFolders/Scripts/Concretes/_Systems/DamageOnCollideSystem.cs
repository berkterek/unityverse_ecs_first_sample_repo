using EcsGame.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Physics;

namespace EcsGame.Systems
{
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    public partial struct DamageOnCollideSystem : ISystem
    {
        struct ComponentDataHandler
        {
            public ComponentLookup<HealthData> HealthDataLookup;
            public ComponentLookup<DamageData> DamageDataLookup;

            public ComponentDataHandler(ref SystemState state)
            {
                HealthDataLookup = state.GetComponentLookup<HealthData>();
                DamageDataLookup = state.GetComponentLookup<DamageData>();
            }

            public void Update(ref SystemState state)
            {
                HealthDataLookup.Update(ref state);
                DamageDataLookup.Update(ref state);
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
            var deltaTime = SystemAPI.Time.DeltaTime;
            _componentDataHandler.Update(ref state);

            var job = new DamageOnCollideJob()
            {
                HealthDataLookup = _componentDataHandler.HealthDataLookup,
                DamageDataLookup = _componentDataHandler.DamageDataLookup,
                Ecb = entityCommandBuffer,
                DeltaTime = deltaTime
            };
            
            var jobHandler = job.Schedule(SystemAPI.GetSingleton<SimulationSingleton>(), state.Dependency);
            state.Dependency = jobHandler;
        }
    }
    
    [BurstCompile]
    public partial struct DamageOnCollideJob : ICollisionEventsJob
    {
        public EntityCommandBuffer Ecb;
        public ComponentLookup<HealthData> HealthDataLookup;
        public ComponentLookup<DamageData> DamageDataLookup;
        public float DeltaTime;

        public void Execute(CollisionEvent collisionEvent)
        {
            var entityA = collisionEvent.EntityA;
            var entityB = collisionEvent.EntityB;

            if (HealthDataLookup.HasComponent(entityA))
            {
                if (DamageDataLookup.HasComponent(entityB))
                {
                    var damageData = DamageDataLookup.GetRefRW(entityB);
                    damageData.ValueRW.CurrentTime += DeltaTime;
                    if (damageData.ValueRO.CurrentTime < damageData.ValueRO.MaxTime) return;
                    damageData.ValueRW.CurrentTime = 0f;
                    var damage = damageData.ValueRO.Damage;
                    Ecb.AppendToBuffer(entityA, new DamageBuffer()
                    {
                        DamageValue = damage
                    });
                }
            }
            else if (HealthDataLookup.HasComponent(entityB))
            {
                if (DamageDataLookup.HasComponent(entityA))
                {
                    var damageData = DamageDataLookup.GetRefRW(entityA);
                    damageData.ValueRW.CurrentTime += DeltaTime;
                    if (damageData.ValueRO.CurrentTime > damageData.ValueRO.MaxTime) return;
                    damageData.ValueRW.CurrentTime = 0f;
                    var damage = damageData.ValueRO.Damage;
                    Ecb.AppendToBuffer(entityB, new DamageBuffer()
                    {
                        DamageValue = damage
                    });
                }
            }
        }
    }
}