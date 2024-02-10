using Unity.Burst;
using Unity.Entities;
using Unity.Physics;

namespace SampleScripts
{
    public partial struct PlayerOnColliderSystem : ISystem
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
            state.RequireForUpdate<EndSimulationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<SimulationSingleton>();
            _componentDataHandler = new ComponentDataHandler(ref state);
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            var entityCommandBufferSystem = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
            var entityCommandBuffer = entityCommandBufferSystem.CreateCommandBuffer(state.WorldUnmanaged);
            
            _componentDataHandler.Update(ref state);
            var job = new PlayerOnColliderJob()
            {
                HealthDataLookup = _componentDataHandler.HealthDataLookup,
                DamageDataLookup = _componentDataHandler.DamageDataLookup,
                DeltaTime = deltaTime,
                Ecb = entityCommandBuffer
            };
            var jobHandler = job.Schedule(SystemAPI.GetSingleton<SimulationSingleton>(), state.Dependency);
            
            state.Dependency = jobHandler;
        }
    }
    
    [BurstCompile]
    public partial struct PlayerOnColliderJob : ICollisionEventsJob
    {
        public float DeltaTime;
        public EntityCommandBuffer Ecb;
        public ComponentLookup<HealthData> HealthDataLookup;
        public ComponentLookup<DamageData> DamageDataLookup;
        
        [BurstCompile]
        public void Execute(CollisionEvent collisionEvent)
        {
            var entityA = collisionEvent.EntityA;
            var entityB = collisionEvent.EntityB;

            if (HealthDataLookup.HasComponent(entityA))
            {
                if (DamageDataLookup.HasComponent(entityB))
                {
                    var damage = DamageDataLookup.GetRefRO(entityB).ValueRO.Damage;
                    var damageBufferValue = damage * DeltaTime;
                    Ecb.AppendToBuffer(entityA, new DamageBufferData()
                    {
                        Value = damageBufferValue
                    });
                }
            }
            else if (HealthDataLookup.HasComponent(entityB))
            {
                if (DamageDataLookup.HasComponent(entityA))
                {
                    var damage = DamageDataLookup.GetRefRO(entityA).ValueRO.Damage;
                    var damageBufferValue = damage * DeltaTime;
                    Ecb.AppendToBuffer(entityA, new DamageBufferData()
                    {
                        Value = damageBufferValue
                    });
                }
            }
        }
    }
}