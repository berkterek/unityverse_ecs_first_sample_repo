using Unity.Burst;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;
using UnityEngine;

namespace Sample3
{
    //[BurstCompile]
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    public partial struct OnPhysicsCubeSystem : ISystem
    {
        //[BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<SimulationSingleton>();
        }

        //[BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            //OnCollision
            // var job = new OnCollisionCubeJob();
            // var handleJob = job.Schedule(SystemAPI.GetSingleton<SimulationSingleton>(), state.Dependency);
            // state.Dependency = handleJob;
            
            //OnTrigger
            //ikiside calisiyor
            var job = new OnTriggerCubeJob();
            // var jobHandle = job.Schedule(SystemAPI.GetSingleton<SimulationSingleton>(), state.Dependency);
            // state.Dependency = jobHandle;
            
            job.Schedule(SystemAPI.GetSingleton<SimulationSingleton>(), state.Dependency);
        }
    }
    
    //[BurstCompile]
    public partial struct OnCollisionCubeJob : ICollisionEventsJob
    {
        //[BurstCompile]
        public void Execute(CollisionEvent collisionEvent)
        {
            Debug.Log("Collision Event Triggered");
        }
    }
    
	public partial struct OnTriggerCubeJob : ITriggerEventsJob
    {
        public void Execute(TriggerEvent triggerEvent)
        {
            Debug.Log("Trigger event triggered");
        }
    }
}