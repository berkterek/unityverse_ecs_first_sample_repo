using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Sample1
{
    [BurstCompile]
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateBefore(typeof(TransformSystemGroup))]
    public partial struct CubeRotateSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;
            // foreach (var (cubeEntityTag, moveData, localTransform) in SystemAPI.Query<RefRO<CubeEntityTag>, RefRO<MoveData>, RefRW<LocalTransform>>())
            // {
            //     var currentRotation = localTransform.ValueRO.Rotation;
            //     var increaseRotationValue = quaternion.Euler(0, moveData.ValueRO.RotateSpeed * deltaTime, 0f);
            //     localTransform.ValueRW.Rotation = math.mul(currentRotation, increaseRotationValue);
            // }

            // new CubeRotateJob()
            // {
            //     DeltaTime = deltaTime
            // }.ScheduleParallel();

            new CubeRotateWithAspectJob()
            {
                DeltaTime = deltaTime
            }.ScheduleParallel();
        }
    }

    [BurstCompile]
    public partial struct CubeRotateJob : IJobEntity
    {
        public float DeltaTime;
        
        [BurstCompile]
        private void Execute(in CubeEntityTag entityTag, in MoveData moveData, ref LocalTransform localTransform)
        {
            var currentRotation = localTransform.Rotation;
            var increaseRotationValue = quaternion.Euler(0f, moveData.RotateSpeed * DeltaTime, 0f);
            localTransform.Rotation = math.mul(currentRotation, increaseRotationValue);
        }
    }

    [BurstCompile]
    public partial struct CubeRotateWithAspectJob : IJobEntity
    {
        public float DeltaTime;

        [BurstCompile]
        private void Execute(CubeEntityAspect cubeEntityAspect)
        {
            cubeEntityAspect.RotateProcess(DeltaTime);
        }
    }
}