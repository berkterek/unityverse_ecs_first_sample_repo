using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Sample1
{
    [BurstCompile]
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateBefore(typeof(TransformSystemGroup))]
    [UpdateAfter(typeof(InputReaderSystem))]
    public partial struct SoldierMoveSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;
            
            new SoldierMoveJob()
            {
                DeltaTime = deltaTime
            }.ScheduleParallel();
        }
    }

    [BurstCompile]
    public partial struct SoldierMoveJob : IJobEntity
    {
        public float DeltaTime;
        
        [BurstCompile]
        private void Execute(Entity entity, ref LocalTransform localTransform, ref MoveData moveData,
            in InputData inputData)
        {
            moveData.Velocity = math.length(inputData.Direction);
            localTransform.Position += moveData.MoveSpeed * DeltaTime * inputData.Direction;
        }
    }
}