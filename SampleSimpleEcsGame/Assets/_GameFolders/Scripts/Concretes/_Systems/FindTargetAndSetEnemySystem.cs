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

            foreach (var (enemyTag,inputData, localTransform) in SystemAPI.Query<RefRO<EnemyTag>, RefRW<InputData>, RefRO<LocalTransform>>())
            {
                if(math.distance(targetPosition, localTransform.ValueRO.Position) < 0.1f) continue;

                var targetDirection = targetPosition - localTransform.ValueRO.Position;
                targetDirection = math.normalize(targetDirection);
                inputData.ValueRW.Direction = targetDirection;
            }
        }
    }
}