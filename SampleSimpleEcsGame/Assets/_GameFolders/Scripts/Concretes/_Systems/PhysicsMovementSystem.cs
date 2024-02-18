using EcsGame.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Physics;

namespace EcsGame.Systems
{
    [BurstCompile]
    public partial struct PhysicsMovementSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;
            
            new PhysicsMovementJob()
            {
                DeltaTime = deltaTime
            }.ScheduleParallel();
        }
    }

    [BurstCompile]
    public partial struct PhysicsMovementJob : IJobEntity
    {
        public float DeltaTime;
        
        [BurstCompile]
        private void Execute(ref PhysicsVelocity physicsVelocity, in InputData inputData, in MoveData moveData)
        {
            physicsVelocity.Linear += DeltaTime * moveData.MoveSpeed * inputData.Direction;
        }
    }
}