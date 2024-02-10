using Unity.Burst;
using Unity.Entities;
using Unity.Physics;

namespace SampleScripts
{
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    public partial struct MoveSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;

            new MoveJob
            {
                DeltaTime = deltaTime
            }.ScheduleParallel();
        }
    }

    [BurstCompile]
    public partial struct MoveJob : IJobEntity
    {
        public float DeltaTime;
        
        [BurstCompile]
        void Execute(Entity entity, in InputData inputData, in MoveData moveData, ref PhysicsVelocity physicsVelocity)
        {
            //localTransform.Position += DeltaTime * moveData.MoveSpeed * inputData.MoveInput;
            physicsVelocity.Linear += DeltaTime * moveData.MoveSpeed * inputData.MoveInput;
        }
    }
}