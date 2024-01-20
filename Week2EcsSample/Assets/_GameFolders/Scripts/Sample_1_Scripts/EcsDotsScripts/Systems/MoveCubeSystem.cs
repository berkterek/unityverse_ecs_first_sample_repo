using Unity.Entities;
using Unity.Mathematics;
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
            float3 direction = new float3(0f, 0f, 1f);
            foreach (var (moveData, localTransform, cubeTag) in SystemAPI.Query<RefRO<MoveData>, RefRW<LocalTransform>, RefRO<CubeTag>>())
            {
                localTransform.ValueRW.Position += deltaTime * moveData.ValueRO.Speed * direction;
            }

            // foreach (var localTransform in SystemAPI.Query<RefRW<LocalTransform>>())
            // {
            //     localTransform.ValueRW.Position += deltaTime * direction;
            // }
        }
    }
}