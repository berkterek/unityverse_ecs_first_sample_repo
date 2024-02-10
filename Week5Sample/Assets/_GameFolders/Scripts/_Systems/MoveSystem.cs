using Unity.Burst;
using Unity.Entities;
using Unity.Physics;
using Unity.Transforms;

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

    public partial struct MoveJob : IJobEntity
    {
        public float DeltaTime;
        
        private void Execute(Entity entity, in InputData inputData, in MoveData moveData, ref PhysicsVelocity physicsVelocity)
        {
            //localTransform.Position += DeltaTime * moveData.MoveSpeed * inputData.MoveInput;
            physicsVelocity.Linear += DeltaTime * moveData.MoveSpeed * inputData.MoveInput;
        }
    }
}