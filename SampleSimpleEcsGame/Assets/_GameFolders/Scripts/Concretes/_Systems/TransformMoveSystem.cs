using EcsGame.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace EcsGame.Systems
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateBefore(typeof(TransformSystemGroup))]
    [BurstCompile]
    public partial struct TransformMoveSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;

            new TransformMoveJob()
            {
                DeltaTime = deltaTime
            }.ScheduleParallel();
        }
    }

    [BurstCompile]
    public partial struct TransformMoveJob : IJobEntity
    {
        public float DeltaTime;
        
        [BurstCompile]
        private void Execute(in TransformMoverTag transformMoverTag, in InputData inputData, in MoveData moveData, ref LocalTransform localTransform)
        {
            localTransform.Position += DeltaTime * moveData.MoveSpeed * inputData.Direction;
        }
    }
}