using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Sample_1_Scripts
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateBefore(typeof(TransformSystemGroup))]
    public partial struct MoveSphereSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;
            // float3 direction = new float3(1f, 0f, 0f);
            // foreach (var (moveData, localTransform, cubeTag) in SystemAPI.Query<RefRO<MoveData>, RefRW<LocalTransform>, RefRO<SphereTag>>())
            // {
            //     localTransform.ValueRW.Position += deltaTime * moveData.ValueRO.Speed * direction;
            // }
            foreach (var sphereMovementAspect in SystemAPI.Query<SphereMovementAspect>())
            {
                sphereMovementAspect.MoveProcess(deltaTime);
            }
        }
    }
}