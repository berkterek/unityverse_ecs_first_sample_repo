using Unity.Burst;
using Unity.Entities;
using EcsGame.Components;
using Unity.Mathematics;
using Unity.Transforms;

namespace EcsGame.Systems
{
    [BurstCompile]
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateBefore(typeof(TransformSystemGroup))]
    public partial struct RotationSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;

            new RotationJob()
            {
                DeltaTime = deltaTime
            }.ScheduleParallel();
        }
    }

    [BurstCompile]
    public partial struct RotationJob : IJobEntity
    {
        public float DeltaTime;
        
        [BurstCompile]
        private void Execute(in InputData inputData, in RotationTag rotationTag, in MoveData moveData, ref LocalTransform localTransform)
        {
            var direction = inputData.Direction;
            if (math.lengthsq(direction) > 0f)
            {
                direction = math.normalize(direction);
                var targetRotation = quaternion.LookRotation(direction, new float3(0f, 1f, 0f));
                    
                if (!math.any(math.isnan(targetRotation.value)))
                {
                    float rotationSpeed = 5f;
                    localTransform.Rotation = math.slerp(localTransform.Rotation, targetRotation, rotationSpeed * DeltaTime);
                }
            }
        }
    }
}