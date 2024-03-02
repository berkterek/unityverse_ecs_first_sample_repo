using EcsGame.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace EcsGame.Systems
{
    [BurstCompile]
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateBefore(typeof(TransformSystemGroup))]
    public partial struct FindTargetAndSetEnemySystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EnemyTargetTag>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var enemyTargetEntity = SystemAPI.GetSingletonEntity<EnemyTargetTag>();
            var targetLocalTransform = SystemAPI.GetComponent<LocalTransform>(enemyTargetEntity);
            var targetPosition = targetLocalTransform.Position;

            new FindTargetAndSetEnemyJob()
            {
                TargetPosition = targetPosition
            }.ScheduleParallel();
        }
    }

    [BurstCompile]
    public partial struct FindTargetAndSetEnemyJob : IJobEntity
    {
        public float3 TargetPosition;
        
        [BurstCompile]
        private void Execute(in EnemyTag enemyTag, in LocalTransform localTransform, ref InputData inputData)
        {
            if(math.distance(TargetPosition, localTransform.Position) < 0.1f) return;

            var targetDirection = TargetPosition - localTransform.Position;
            targetDirection = math.normalize(targetDirection);
            inputData.Direction = targetDirection;
        }
    }
}