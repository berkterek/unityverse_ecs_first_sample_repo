using Unity.Entities;
using Unity.Transforms;

namespace Sample_1_Scripts
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateBefore(typeof(LocalTransform))]
    public partial struct CubeRotationSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;
            foreach (var cubeMovementAspect in SystemAPI.Query<CubeMovementAspect>())
            {
                cubeMovementAspect.RotateProcess(deltaTime);
            }
        }
    }
}