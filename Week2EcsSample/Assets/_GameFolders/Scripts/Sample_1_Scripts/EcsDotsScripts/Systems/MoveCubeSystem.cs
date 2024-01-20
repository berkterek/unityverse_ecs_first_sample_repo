using Unity.Entities;
using Unity.Transforms;

namespace Sample_1_Scripts
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateBefore(typeof(TransformSystemGroup))]
    public partial struct MoveCubeSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;

            foreach (var cubeMovementAspect in SystemAPI.Query<CubeMovementAspect>())
            {
                cubeMovementAspect.MoveProcess(deltaTime);
            }
        }

        private void Sample(float deltaTime)
        {
            // float3 direction = new float3(0f, 0f, 1f);
            // foreach (var (moveData, localTransform, cubeTag) in SystemAPI.Query<RefRO<MoveData>, RefRW<LocalTransform>, RefRO<CubeTag>>())
            // {
            //     localTransform.ValueRW.Position += deltaTime * moveData.ValueRO.Speed * direction;
            //
            //     quaternion currentRotation = localTransform.ValueRO.Rotation;
            //     quaternion increaseRotation = quaternion.Euler(0f, moveData.ValueRO.Speed * deltaTime, 0f);
            //     localTransform.ValueRW.Rotation = math.mul(currentRotation, increaseRotation);
            // }

            // foreach (var localTransform in SystemAPI.Query<RefRW<LocalTransform>>())
            // {
            //     localTransform.ValueRW.Position += deltaTime * direction;
            // }
        }
    }
}