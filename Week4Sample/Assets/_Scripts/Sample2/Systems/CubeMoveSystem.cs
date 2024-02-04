using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace Sample2
{
    [BurstCompile]
    public partial struct CubeMoveSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;
            new CubeMoveJob()
            {
                DeltaTime = deltaTime
            }.ScheduleParallel();
        }
    }

    [BurstCompile]
    public partial struct CubeMoveJob : IJobEntity
    {
        public float DeltaTime;

        [BurstCompile]
        private void Execute(Entity entity, in MoveData moveData, ref LocalTransform localTransform)
        {
            localTransform.Position += moveData.Speed * DeltaTime * moveData.Direction;
        }
    }
}