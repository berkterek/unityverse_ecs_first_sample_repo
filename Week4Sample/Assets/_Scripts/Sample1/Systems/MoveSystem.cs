using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace Sample1
{
    [BurstCompile]
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateBefore(typeof(TransformSystemGroup))]
    public partial struct MoveSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;

            new MoveJob()
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
        private void Execute(Entity entity, in MoveData moveData, ref LocalTransform localTransform)
        {
            localTransform.Position += moveData.Speed * DeltaTime * moveData.Direction;
        }
    }
}